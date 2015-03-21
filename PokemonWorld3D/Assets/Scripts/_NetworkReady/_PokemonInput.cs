using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _PokemonInput : MonoBehaviour
{
	public GameObject myCamera;
	public bool grounded;
	public float walkSpeed = 1.0f;
	public float runMultiplier = 2.0f;
	public float max_velocity_change = 10.0f;
	public bool jumping;									//make private later
	public bool hasJumped;									//make private later
	public float jumpPower = 5.0f;
	public float gravity = 10.0f;
	public bool falling;
	public bool attacking;
	public GameObject target;								//Make private later.
	public _Pokemon targetPokemon;							//Make private later.
	public List<GameObject> Targets;						//Make private later.

	private _Pokemon thisPokemon;
	private _HUD hud;

	private Animator anim;

	void Start()
	{
		thisPokemon = GetComponent<_Pokemon>();
		hud = thisPokemon.trainer.GetComponent<_PlayerCharacter>().hud;
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if(attacking)
		{
			return;
		}
		if(jumping)
		{
			return;
		}
		if(!grounded && rigidbody.velocity.y > -0.04f && rigidbody.velocity.y < 0.04f)
		{
			falling = false;
			anim.SetBool("Falling", false);
			grounded = true;
		}
		if(Mathf.Abs(rigidbody.velocity.y) > jumpPower * 0.75f)
		{
			falling = true;
			anim.SetBool("Falling", true);
		}
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			if(Input.GetButton("Walk"))
			{
				anim.SetFloat("Speed", 1.0f);
			}
			else
			{
				anim.SetFloat("Speed", 2.0f);
			}
		}
		else
		{
			anim.SetFloat("Speed", 0.0f);
			if(!falling && !jumping)
				Idle();
		}
		if (Input.GetButtonDown("Jump") && grounded) {
			jumping = true;
			anim.SetBool("Jumping", true);
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPlayer();
		}
		if(Input.GetButtonDown("Targeting"))
		{
			AddAllTargets();
			TargetPokemon();
		}
		KeepTrackOfTargets();
		Attack();
	}
	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 forward = myCamera.camera.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0f, -forward.x);
		if(grounded)
		{
			float speed;
			if(Input.GetButton("Walk"))
			{
				speed = walkSpeed;
			}
			else
			{
				speed = walkSpeed * runMultiplier;
			}
			Vector3 targetVelocity = (horizontal * right + vertical * forward) * speed;
			if (targetVelocity != Vector3.zero)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetVelocity), 10f * Time.smoothDeltaTime);
				transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
			}
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -max_velocity_change, max_velocity_change);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -max_velocity_change, max_velocity_change);
			velocityChange.y = 0;
			if(hasJumped){
				velocityChange.y = CalculateJumpVerticalSpeed();
				grounded = false;
				hasJumped = false;
				jumping = false;
			}
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
	}

	public void Jumping()
	{
		hasJumped = true;
		anim.SetBool("Jumping", false);
	}
	public void NotAttacking()
	{
		attacking = false;
	}

	private void Idle()
	{
		if(thisPokemon.isInBattle)
		{
			anim.SetBool("InBattle", true);
		}
		else
		{
			anim.SetBool("InBattle", false);
		}
	}
	private float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpPower * gravity);
	}
	private void SwapToPlayer()
	{
		if(!thisPokemon.isInBattle)
		{
			//DISABLE THE POKEMON AI.
			//ENABLE THE PLAYER AI.
			thisPokemon.trainer.GetComponent<_PlayerInput>().enabled = true;
			myCamera.GetComponent<_CameraController>().SetTarget(thisPokemon.trainer.transform);
			GetComponent<AudioListener>().enabled = false;
			thisPokemon.trainer.GetComponent<AudioListener>().enabled = true;
			this.enabled = false;
		}
	}
	private void AddAllTargets()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Pokemon");
		
		foreach(GameObject g in go)
		{
			AddTarget(g);
		}
	}
	private void AddTarget(GameObject target)
	{
		if(!Targets.Contains(target))
		//Add an if statement here if we want to filter the results from "AddAllTargets".
			Targets.Add(target);
	}
	private void RemoveTarget(GameObject aTarget)
	{
		if(target = aTarget)
		{
			target = null;
			hud.NoTarget();
		}
		Targets.Remove(aTarget);
	}
	private void KeepTrackOfTargets()
	{
		for(int i = 0; i < Targets.Count; i++)
		{
			if(Targets[i].GetComponent<_Pokemon>().curHP == 0)
			{
				Targets.Remove(Targets[i]);
			}
		}
	}
	private void SortTargetsByDistance()
	{
		Targets.Sort(delegate(GameObject c1, GameObject c2){
			return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
				((Vector3.Distance(this.transform.position, c2.transform.position)));   
		});
	}
	private void TargetPokemon()
	{
		if(target == null)
		{
			SortTargetsByDistance();
			target = Targets[0];
			targetPokemon = target.GetComponent<_Pokemon>();
			hud.SetTarget(targetPokemon);
		}
		else
		{
			int index = Targets.IndexOf(target);
			
			if(index < Targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			target = Targets[index];
			targetPokemon = Targets[index].GetComponent<_Pokemon>();
			hud.SetTarget(targetPokemon);
		}
	}
	private void Attack()
	{
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha1) && thisPokemon.KnownMoves.Count >= 1
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[0].range
		   && thisPokemon.KnownMoves[0].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[0].ppCost)
		{
			thisPokemon.KnownMoves[0].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha2) && thisPokemon.KnownMoves.Count >= 2
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[1].range
		   && thisPokemon.KnownMoves[1].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[1].ppCost)
		{
			thisPokemon.KnownMoves[1].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha3) && thisPokemon.KnownMoves.Count >= 3
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[2].range
		   && thisPokemon.KnownMoves[2].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[2].ppCost)
		{
			thisPokemon.KnownMoves[2].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha4) && thisPokemon.KnownMoves.Count >= 4
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[3].range
		   && thisPokemon.KnownMoves[3].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[3].ppCost)
		{
			thisPokemon.KnownMoves[3].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha5) && thisPokemon.KnownMoves.Count >= 5
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[4].range
		   && thisPokemon.KnownMoves[4].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[4].ppCost)
		{
			thisPokemon.KnownMoves[4].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha6) && thisPokemon.KnownMoves.Count >= 6
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[5].range
		   && thisPokemon.KnownMoves[5].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[5].ppCost)
		{
			thisPokemon.KnownMoves[5].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha7) && thisPokemon.KnownMoves.Count >= 7
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[6].range
		   && thisPokemon.KnownMoves[6].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[6].ppCost)
		{
			thisPokemon.KnownMoves[6].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha8) && thisPokemon.KnownMoves.Count >= 8
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[7].range
		   && thisPokemon.KnownMoves[7].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[7].ppCost)
		{
			thisPokemon.KnownMoves[7].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha9) && thisPokemon.KnownMoves.Count >= 9
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[8].range
		   && thisPokemon.KnownMoves[8].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[8].ppCost)
		{
			thisPokemon.KnownMoves[8].UseMove(gameObject, target, Targets);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha0) && thisPokemon.KnownMoves.Count >= 10
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[9].range
		   && thisPokemon.KnownMoves[9].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[9].ppCost)
		{
			thisPokemon.KnownMoves[9].UseMove(gameObject, target, Targets);
		}
	}
}
