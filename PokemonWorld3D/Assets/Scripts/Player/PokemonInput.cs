using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonInput : MonoBehaviour 
{
	
	public float walk_speed = 1.0f;
	public float run_multiplier = 2.0f;
	public float gravity = 10.0f;
	public float air_time;							//Make private later.
	public float fall_time = 0.5f;
	public float max_velocity_change = 10.0f;		//Make private later.
	public bool can_jump = true;					//Make private later.
	public bool jumping = false;					//Make private later.
	public float jump_height = 2.0f;
	public bool using_a_move;						//Make private later.
	public bool grounded = false;					//Make private later.
	public GameObject target;						//Make private later.
	public Pokemon target_pokemon;					//Make private later.
	public List<GameObject> targets;				//Make private later.
	public bool moving;
	public bool fainting;

	public Transform my_transform;
	private Rigidbody body;
	private Vector3 target_velocity;
	private Vector3 velocity;
	public Pokemon this_pokemon;
	private FINALGUISCRIPT hud;
	private Flight flight;

	void Start()
	{
		my_transform = transform;
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		body.useGravity = false;
		target_velocity = Vector3.zero;
		this_pokemon = GetComponent<Pokemon>();
		if(this_pokemon.trainer != null)
			hud = this_pokemon.trainer.GetComponent<PlayerCharacter>().players_hud;
		targets = new List<GameObject>();
		target = null;
		flight = GetComponent<Flight>();
	}
	void Update()
	{
		if(Input.GetButton("Horizontal") && grounded  && !using_a_move && !jumping && !fainting ||
		   Input.GetButton("Vertical") && grounded && !using_a_move && !jumping && !fainting)
		{
			if(Input.GetButton("Walk"))
			{
				Walk();
			}
			else
			{
				Run();
			}
		}
		else
		{
			if(grounded  && !using_a_move && !jumping && !fainting)
				Idle();
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
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0f, -forward.x);
		if (grounded)
		{
			air_time = 0.0f;
			if(Input.GetButtonDown("TakeFlight"))
			{
				flight.enabled = true;
				StartCoroutine(flight.TakeOff());
			}
			// Calculate how fast we should be moving
			target_velocity = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward);
			if (target_velocity != Vector3.zero)
			{
				my_transform.rotation = Quaternion.Slerp(my_transform.rotation, Quaternion.LookRotation(target_velocity), 10f * Time.smoothDeltaTime);
				my_transform.eulerAngles = new Vector3(0f, my_transform.eulerAngles.y, 0f);
			}
			if(Input.GetButton("Walk"))
			{
				target_velocity *= walk_speed;
			}
			else
			{
				target_velocity *= walk_speed * run_multiplier;
			}
			
			// Apply a force that attempts to reach our target velocity
			velocity = body.velocity;
			Vector3 velocityChange = (target_velocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -max_velocity_change, max_velocity_change);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -max_velocity_change, max_velocity_change);
			velocityChange.y = 0;
			body.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		// Jump
		if (can_jump && Input.GetButton("Jump"))
		{
			Jump();
		}
		// We apply gravity manually for more tuning control
		body.AddForce(new Vector3 (0, -gravity * body.mass, 0));
		
		grounded = false;
		air_time += Time.deltaTime;
		if(air_time > fall_time)
			Fall();
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Terrain"))
		   animation.CrossFade("Landing");
	}
	void OnCollisionStay()
	{
		grounded = true;
		can_jump = true;
	}

	public void Faint()
	{
		fainting = true;
		animation.Play("Faint");
	}
	public void Jumping()
	{
		body.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
		can_jump = false;
	}
	public void NotJumping()
	{
		jumping = false;
	}
	public void UsingMove()
	{
		using_a_move = true;
	}
	public void NotUsingMove()
	{
		using_a_move = false;
	}
	private void Jump()
	{
		jumping = true;
		animation.CrossFade("Jump");
	}
	private void Idle()
	{
		if(this_pokemon.is_in_battle)
		{
			animation.CrossFade("Idle_Battle");
		}
		else
		{
			animation.CrossFade("Idle_World");
		}
	}
	private void Walk()
	{
		animation.CrossFade("Walk");
	}
	private void Run()
	{
		animation.CrossFade("Run");
	}
	private void Fall()
	{
		animation.CrossFade("Falling");
	}
	private float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jump_height * gravity);
	}
	private void SwapToPlayer()
	{
		if(!this_pokemon.is_in_battle)
		{
			//DISABLE THE PLAYER AI.
			//ENABLE THE POKEMON AI.
			this_pokemon.trainer.GetComponent<PlayerInput>().enabled = true;
			Camera.main.GetComponent<CameraController>().SetTarget(this_pokemon.trainer);
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
		if(target.gameObject != this.gameObject)
			targets.Add(target);
	}
	private void RemoveTarget(GameObject target)
	{
		targets.Remove(target);
		target = null;
		hud.NoTarget();
	}
	private void KeepTrackOfTargets()
	{
		if(target_pokemon != null && target_pokemon.cur_hp == 0)
		{
			RemoveTarget(target);
		}
		for(int i = 0; i < targets.Count; i++)
		{
			if(targets[i].GetComponent<Pokemon>().cur_hp == 0)
			{
				targets.Remove(targets[i]);
			}
		}
	}
	private void SortTargetsByDistance()
	{
		targets.Sort(delegate(GameObject c1, GameObject c2){
			return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
				((Vector3.Distance(this.transform.position, c2.transform.position)));   
		});
	}
	private void TargetPokemon()
	{
		if(target == null)
		{
			SortTargetsByDistance();
			target = targets[0];
			target_pokemon = target.GetComponent<Pokemon>();
			hud.SetTarget(target_pokemon);
		}
		else
		{
			int index = targets.IndexOf(target);
			
			if(index < targets.Count - 1)
			{
				index++;
			}
			else
			{
				index = 0;
			}
			target = targets[index];
			target_pokemon = targets[index].GetComponent<Pokemon>();
			hud.SetTarget(target_pokemon);
		}
	}
	private void Attack()
	{
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha1) && this_pokemon.known_moves.Count >= 1
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[0].range
		   && this_pokemon.known_moves[0].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[0].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[0].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha2) && this_pokemon.known_moves.Count >= 2
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[1].range
		   && this_pokemon.known_moves[1].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[1].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[1].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha3) && this_pokemon.known_moves.Count >= 3
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[2].range
		   && this_pokemon.known_moves[2].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[2].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[2].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha4) && this_pokemon.known_moves.Count >= 4
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[3].range
		   && this_pokemon.known_moves[3].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[3].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[3].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha5) && this_pokemon.known_moves.Count >= 5
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[4].range
		   && this_pokemon.known_moves[4].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[4].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[4].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha6) && this_pokemon.known_moves.Count >= 6
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[5].range
		   && this_pokemon.known_moves[5].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[5].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[5].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha7) && this_pokemon.known_moves.Count >= 7
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[6].range
		   && this_pokemon.known_moves[6].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[6].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[6].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha8) && this_pokemon.known_moves.Count >= 8
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[7].range
		   && this_pokemon.known_moves[7].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[7].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[7].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha9) && this_pokemon.known_moves.Count >= 9
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[8].range
		   && this_pokemon.known_moves[8].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[8].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[8].UseMove(gameObject, target, targets);
		}
		if(target != null && !using_a_move && !fainting && Input.GetKeyDown(KeyCode.Alpha0) && this_pokemon.known_moves.Count >= 10
		   && Vector3.Distance(transform.position, target.transform.position) < this_pokemon.known_moves[9].range
		   && this_pokemon.known_moves[9].cooling_down == 0 && this_pokemon.cur_pp >= this_pokemon.known_moves[9].pp_cost)
		{
			using_a_move = true;
			this_pokemon.known_moves[9].UseMove(gameObject, target, targets);
		}
	}
}
