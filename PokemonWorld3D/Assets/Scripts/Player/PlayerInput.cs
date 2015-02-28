using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
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
	
	public Transform my_transform;
	private Rigidbody body;
	private Vector3 target_velocity;
	private Vector3 velocity;
	public PlayerCharacter this_player;
	public ThrowPokeBall throw_poke_ball;
	private FINALGUISCRIPT hud;
	private Animator anim;
	public bool throw_coroutine_started;
	
	void Start()
	{
		my_transform = transform;
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		body.useGravity = false;
		target_velocity = Vector3.zero;
		this_player = GetComponent<PlayerCharacter>();
		targets = new List<GameObject>();
		target = null;
		anim = GetComponent<Animator>();
		hud = this_player.players_hud;
		throw_coroutine_started = false;
		throw_poke_ball = GetComponent<ThrowPokeBall>();
	}
	void Update()
	{
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPokemon();
		}
		if(Input.GetButtonDown("Targeting"))
		{
			AddAllTargets();
			TargetPokemon();
		}
		KeepTrackOfTargets();
		if(Input.GetKeyDown(KeyCode.C) && target != null)
		{
			StartCoroutine(GetComponent<ThrowPokeBall>().PokeBallGo());
		}
		SummonPokemon();
	}
	void FixedUpdate ()
	{
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0f;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0f, -forward.x);
		if (grounded && !jumping && !using_a_move)
		{
			air_time = 0.0f;

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
			
			// Handle animation between idle & locomotion
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
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
				Idle();
			}
		}
		// Jump
		if (grounded && Input.GetButton("Jump"))
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
	}
	
	public void Jumping()
	{
		body.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
		can_jump = false;
	}
	public void NotJumping()
	{
		can_jump = true;
		jumping = false;
	}

	private void Jump()
	{
		jumping = true;
		animation.CrossFade("Jump");
	}
	private void Idle()
	{
		if(this_player.player_is_in_battle)
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
		// From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
		return Mathf.Sqrt(2 * jump_height * gravity);
	}
	private void SwapToPokemon()
	{
		if(this_player.players_active_pokemon != null)
		{
			//DISABLE THE POKEMON AI.
			//ENABLE THE PLAYER AI.
			this_player.players_active_pokemon.GetComponent<PokemonInput>().enabled = true;
			Camera.main.GetComponent<CameraController>().SetTarget(this_player.players_active_pokemon);
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

	private void SummonPokemon(){
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha1) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 1){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[0]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha2) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 2){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[1]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha3) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 3){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[2]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha4) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 4){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[3]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha5) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 5){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[4]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha6) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count == 6){
			throw_coroutine_started = true;
			StartCoroutine(throw_poke_ball.PokemonGo(this_player.players_pokemon_roster[5]));
		}
	}


}
