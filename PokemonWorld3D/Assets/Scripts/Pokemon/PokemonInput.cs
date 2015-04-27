using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonInput : MonoBehaviour
{
	public GameObject myCamera;
	public float walkSpeed = 1.0f;
	public float runMultiplier = 2.0f;
	public float maxVelocityChange = 10.0f;									
	public float jumpPower = 5.0f;
	public float gravity = 10.0f;
	public bool attacking;									//HAS TO BE PUBLIC BECAUSE IT'S ACCESSED BY MOVE SCRIPTS!!!!!

	private bool grounded = false;
	private bool jumping = false;							
	private bool hasJumped = false;
	private bool falling = false;
	private GameObject target;								
	private Pokemon targetPokemon;							
	private float horizontal;
	private float vertical;
	private Vector3 forward;
	private Vector3 right;
	private Pokemon thisPokemon;
	public HUD hud;											//HAS TO BE PUBLIC BECAUSE IT'S ACCESSED BY POKEMON SCRIPT!!!!
	private Animator anim;

	void Start()
	{
		thisPokemon = GetComponent<Pokemon>();
		hud = thisPokemon.trainer.GetComponent<PlayerCharacter>().hud;
		anim = GetComponent<Animator>();
	}
	void Update()
	{
		if(attacking)
			return;
		Attack();
		if(falling || !grounded)
		{
			if(rigidbody.velocity.y > -0.04f && rigidbody.velocity.y < 0.04f)
			{
				falling = false;
				anim.SetBool("Fall", falling);
				grounded = true;
			}
		}
		if(Mathf.Abs(rigidbody.velocity.y) > jumpPower * 0.75f)
		{
			falling = true;
			anim.SetBool("Fall", falling);
		}
		forward = myCamera.camera.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		right = new Vector3(forward.z, 0f, -forward.x);
		float speed = rigidbody.velocity.magnitude;
		if(Input.GetButton("Walk"))
		{
			speed *= 1.0f;
		}
		else
		{
			speed *= 2.0f;
		}
		anim.SetFloat("Speed", speed);
		if (Input.GetButtonDown("Jump") && grounded) {
			jumping = true;
			anim.SetBool("Jump", jumping);
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPlayer();
		}
		if(Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray);
			foreach(RaycastHit hit in hits) //Loop through all the hits
			{
				if(hit.transform.gameObject.CompareTag("Pokemon") && hit.transform.gameObject == gameObject)
				{
					//You hit a target!
					DeselectTarget(); //Deselect the old target
					target = hit.transform.gameObject;
					targetPokemon = target.GetComponent<Pokemon>();
					SelectTarget(); //Select the new target
					break; //Break out because we don't need to check anymore
				}
				if(hit.transform.gameObject.CompareTag("Pokemon") && hit.transform.gameObject != gameObject)
				{
					//You hit a target!
					DeselectTarget(); //Deselect the old target
					target = hit.transform.gameObject;
					targetPokemon = target.GetComponent<Pokemon>();
					SelectTarget(); //Select the new target
					if(!targetPokemon.isCaptured)
					{
						hud.wildPokemonPanel.SetActive(true);
						hud.wildPokemonPanel.transform.position = Input.mousePosition;
						break; //Break out because we don't need to check anymore
					}
					else
					{
						hud.otherTrainerPanel.SetActive(true);
						hud.otherTrainerPanel.transform.position = Input.mousePosition;
						break;
					}
				}
			}
		}
		if(target != null)
			TrackTarget();
	}
	void FixedUpdate()
	{
		if(attacking)
			return;
		if(grounded)
		{
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");
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
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
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
		anim.SetBool("Jump", false);
	}
	public void NotAttacking()
	{
		attacking = false;
	}

	private float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpPower * gravity);
	}
	public void SwapToPlayer()
	{
		//DISABLE THE POKEMON AI.
		//ENABLE THE PLAYER AI.
		thisPokemon.trainer.GetComponent<PlayerInput>().enabled = true;
		myCamera.GetComponent<CameraController>().SetTarget(thisPokemon.trainer.transform);
		GetComponent<AudioListener>().enabled = false;
		thisPokemon.trainer.GetComponent<AudioListener>().enabled = true;
		this.enabled = false;
	}

	private void SelectTarget()
	{
		hud.targetPokemon = target.GetComponent<Pokemon>();
		hud.targetPokemonPortrait.SetTargetPokemon(target);
	}
	private void DeselectTarget()
	{
		target = null;
		targetPokemon = null;
		hud.targetPokemonPortrait.RemoveTargetPokemon();
	}
	private void TrackTarget()
	{
		if(targetPokemon.curHP == 0)
		{
			DeselectTarget();
		}
	}
	private void Attack()
	{
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha1) && thisPokemon.KnownMoves.Count >= 1
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[0].range
		   && thisPokemon.KnownMoves[0].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[0].ppCost)
		{
			thisPokemon.KnownMoves[0].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha2) && thisPokemon.KnownMoves.Count >= 2
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[1].range
		   && thisPokemon.KnownMoves[1].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[1].ppCost)
		{
			thisPokemon.KnownMoves[1].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha3) && thisPokemon.KnownMoves.Count >= 3
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[2].range
		   && thisPokemon.KnownMoves[2].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[2].ppCost)
		{
			thisPokemon.KnownMoves[2].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha4) && thisPokemon.KnownMoves.Count >= 4
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[3].range
		   && thisPokemon.KnownMoves[3].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[3].ppCost)
		{
			thisPokemon.KnownMoves[3].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha5) && thisPokemon.KnownMoves.Count >= 5
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[4].range
		   && thisPokemon.KnownMoves[4].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[4].ppCost)
		{
			thisPokemon.KnownMoves[4].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha6) && thisPokemon.KnownMoves.Count >= 6
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[5].range
		   && thisPokemon.KnownMoves[5].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[5].ppCost)
		{
			thisPokemon.KnownMoves[5].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha7) && thisPokemon.KnownMoves.Count >= 7
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[6].range
		   && thisPokemon.KnownMoves[6].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[6].ppCost)
		{
			thisPokemon.KnownMoves[6].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha8) && thisPokemon.KnownMoves.Count >= 8
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[7].range
		   && thisPokemon.KnownMoves[7].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[7].ppCost)
		{
			thisPokemon.KnownMoves[7].UseMove(gameObject, target);
		}
		if(target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha9) && thisPokemon.KnownMoves.Count >= 9
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[8].range
		   && thisPokemon.KnownMoves[8].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[8].ppCost)
		{
			thisPokemon.KnownMoves[8].UseMove(gameObject, target);
		}
		if(thisPokemon.isInBattle && target != null && !attacking && Input.GetKeyDown(KeyCode.Alpha0) && thisPokemon.KnownMoves.Count >= 10
		   && Vector3.Distance(transform.position, target.transform.position) <= thisPokemon.KnownMoves[9].range
		   && thisPokemon.KnownMoves[9].coolingDown == 0 && thisPokemon.curPP >= thisPokemon.KnownMoves[9].ppCost)
		{
			thisPokemon.KnownMoves[9].UseMove(gameObject, target);
		}
	}
}
