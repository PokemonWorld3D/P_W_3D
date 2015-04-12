using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class WildPokemonAI : MonoBehaviour
{
	public List<HateHolder> HateList;
	public GameObject target;
	public bool canAttackTarget = false;
	public bool usingMove = false;
	public float idleTime = 15.0f;
	public float idleTimer = 0.0f;
	public float deadTime = 10.0f;
	public float deadTimer = 0.0f;
	public float battleTime = 10.0f;
	public float battleTimer = 0.0f;
	public float mapSize = 0.0f;
	public bool movingToDestination;
	public bool respawning;
	public bool posOrNeg;
	public List<Component> ThingsToDisable;

	private Pokemon thisPokemon;
	private Vector3 forward;
	private Vector3 right;
	private Animator anim;
	public Vector3 destination;
	public Vector3 lastDestination;
	public State state;
	private Vector3 respawnPoint;

	public enum State{ Setup, Init, Idle, Battle, Dead, DontMove }

	void Awake()
	{
		state = State.Setup;
	}
	private IEnumerator Start()
	{
		while(true)
		{
			switch(state)
			{
			case State.Setup:
				Setup();
				break;
			case State.Init:
				Init();
				break;
			case State.Idle:
				Idle();
				break;
			case State.Battle:
				Battle();
				break;
			case State.Dead:
				Dead();
				break;
			case State.DontMove:
				DontMove();
				break;
			}
			yield return null;
		}
	}

	[RPC]
	public void IncreaseHate(int attackingPokemon, int hateIncrease)
	{
		GameObject thePokemon = PhotonView.Find(attackingPokemon).gameObject;
		if(HateList.Count > 0)
		{
			if(HateList.Any(h => h.pokemon == thePokemon))
			{
				for(int i = 0; i < HateList.Count; i++)
				{
					if(HateList[i].pokemon == thePokemon)
					{
						HateList[i].amountOfHate += hateIncrease;
						return;
					}
				}
			}
			else
			{
				HateList.Add(new HateHolder(thePokemon, GetComponent<Pokemon>(), hateIncrease));
			}
			if(HateList.Count > 1)
			{
				HateList.Sort(delegate(HateHolder x, HateHolder y) { return x.amountOfHate.CompareTo(y.amountOfHate); });
			}
		}
		else
		{
			HateList.Add(new HateHolder(thePokemon, GetComponent<Pokemon>(), hateIncrease));
		}
		target = HateList[0].pokemon;
	}

	private void Setup()
	{
		thisPokemon = GetComponent<Pokemon>();
		anim = GetComponent<Animator>();
		state = State.Init;
	}
	private void Init()
	{
		target = null;
		canAttackTarget = false;
		movingToDestination = false;
		respawning = false;
		deadTimer = 0.0f;
		idleTimer = 0.0f;
		HateList.Clear();
		GetComponent<PhotonView>().RPC("SetupPokemonFirstTime", PhotonTargets.AllBuffered);
		state = State.Idle;
	}
	private void Idle()
	{
		forward = transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		right = new Vector3(forward.z, 0f, -forward.x);
		idleTimer += Time.deltaTime;
		if(idleTimer >= idleTime)
		{
			if(!movingToDestination)
			{
				movingToDestination = true;
				StartCoroutine("MoveToDestination");
			}
		}
		float speed = rigidbody.velocity.normalized.magnitude;
		anim.SetFloat("Speed", speed);
	}
	private void Battle()
	{
		if(movingToDestination)
		{
			movingToDestination = false;
			StopCoroutine("MoveToDestination");
			return;
		}
		forward = transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		right = new Vector3(forward.z, 0f, -forward.x);
		if(!usingMove)
		{
			if(thisPokemon.isInBattle)
			{
				if(target == null || target.Equals(null))
				{
					battleTimer += Time.deltaTime;
					if(battleTimer >= battleTime)
					{
						GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
						battleTimer = 0.0f;
					}
					return;
				}
					if(CheckTarget(target))
					{
						canAttackTarget = false;
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position),
						                                      10f * Time.smoothDeltaTime);
						transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
						Vector3 targetPosition = target.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position);
						targetPosition.y = target.transform.position.y;
						Vector3 myPosition = GetComponent<CapsuleCollider>().ClosestPointOnBounds(target.transform.position);
						myPosition.y = transform.position.y;
						float distance = Vector3.Distance(myPosition, targetPosition);
						for(int m = thisPokemon.KnownMoves.Count; m > 0; m--)
						{
							if(thisPokemon.KnownMoves[m - 1].range >= distance && thisPokemon.curPP >= thisPokemon.KnownMoves[m -1].ppCost
							   && thisPokemon.KnownMoves[m -1].coolingDown == 0.0f)
							{
								canAttackTarget = true;
								thisPokemon.KnownMoves[m - 1].UseMove(gameObject, target);
								usingMove = true;
							}
						}
						if(!canAttackTarget)
						{
							if(distance > 1.0f)
							{
								Vector3 targetVelocity = (0.0f * right + 1.0f * forward) * 2.0f;
								
								// Apply a force that attempts to reach our target velocity
								Vector3 velocity = rigidbody.velocity;
								Vector3 velocityChange = (targetVelocity - velocity);
								velocityChange.x = Mathf.Clamp(velocityChange.x, -10.0f, 10.0f);
								velocityChange.z = Mathf.Clamp(velocityChange.z, -10.0f, 10.0f);
								velocityChange.y = 0;
								rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
							}
						}
					}
					else
					{
						return;
					}
				}
			}
		float speed = rigidbody.velocity.magnitude * 2.0f;
		anim.SetFloat("Speed", speed);
	}
	private void Dead()
	{
		deadTimer += Time.deltaTime;
		if(deadTimer >= deadTime)
		{
			if(!respawning)
			{
				respawning = true;
				StartCoroutine("Respawn");
			}
		}
	}
	private void DontMove()
	{

	}
	private bool IsInvalidSpawnPoint(Vector3 position)
	{
		if(position.y == Mathf.Infinity)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	private float TerrainHeight(Vector3 position)
	{
		Ray rayDown = new Ray(position, Vector3.down);
		RaycastHit hitPoint;
		if(Physics.Raycast(rayDown, out hitPoint, Mathf.Infinity))
		{
			return hitPoint.point.y;
		}
		else
		{
			return Mathf.Infinity;
		}
	}
	private bool CheckTarget(GameObject targetPokemon)
	{
		if(target.GetComponent<Pokemon>().curHP == 0)
		{
			HateHolder theTarget = HateList.Find(h => h.pokemon == targetPokemon);
			HateList.Remove(theTarget);
			if(HateList.Count > 0)
			{
				HateList.Sort(delegate(HateHolder x, HateHolder y) { return x.amountOfHate.CompareTo(y.amountOfHate); });
				target = HateList[0].pokemon;
				return true;
			}
			else
			{
				target = null;
				return false;
			}
		}
		return true;
	}

	private IEnumerator Respawn()
	{
		anim.SetBool("Fainting", false);
		bool invalid = true;
		while(invalid)
		{
			respawnPoint = new Vector3(Random.Range(0, mapSize), 1000.0f, Random.Range(0, mapSize));
			respawnPoint.y = TerrainHeight(respawnPoint);
			invalid = IsInvalidSpawnPoint(respawnPoint);
			yield return null;
		}
		NavMeshHit closestHit;
		if(NavMesh.SamplePosition(respawnPoint, out closestHit, 500, 1))
		{
			respawnPoint = closestHit.position;
		}
		else
		{
			Debug.Log("...");
		}
		transform.position = respawnPoint;
		foreach(Component c in ThingsToDisable)
		{
			c.gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(5.0f);
		state = State.Init;
		yield return null;
	}
	private IEnumerator MoveToDestination()
	{
		bool invalid = true;
		lastDestination = destination;
		while(invalid)
		{
			float x = transform.position.x;
			float z = transform.position.z;
			//int posOrNeg = Random.Range(0, 1);
			if(posOrNeg)
			{
				destination = new Vector3(Random.Range(x + 2.0f, x + 10.0f), 1000.0f, Random.Range(z + 2.0f, z + 10.0f));
			}
			if(!posOrNeg)
			{
				destination = new Vector3(Random.Range(x - 2.0f, x - 10.0f), 1000.0f, Random.Range(z - 2.0f, z - 10.0f));
			}
			float terrain_height = Terrain.activeTerrain.SampleHeight(destination);
			destination.y = terrain_height;
			invalid = IsInvalidSpawnPoint(destination);
			if(!invalid)
			{
				if(destination.x > mapSize || destination.z > mapSize || destination.x < 0.0f || destination.z < 0.0f)
					invalid = true;
			}
			posOrNeg = ! posOrNeg;
			yield return null;
		}
		while(Vector3.Distance(transform.position, destination) > 0.5f)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination - transform.position), 10f * Time.smoothDeltaTime);
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
			Vector3 targetVelocity = (0.0f * right + 1.0f * forward) * 1.0f;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -10.0f, 10.0f);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -10.0f, 10.0f);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			yield return null;
		}
		idleTimer = 0.0f;
		movingToDestination = false;
	}
}
