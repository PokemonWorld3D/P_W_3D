using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
	public PlayerCharacter this_player;
	public float speed_limit = 10.0f;
	public float rotate_speed = 10.0f;
	public float gravity = 10.0f;
	public float max_velocity_change = 10.0f;
	public bool can_jump = true;
	public float jump_height = 2.0f;
	public bool grounded = false;
	public ThrowPokeBall throw_poke_ball;
	public GameObject target;
	public List<GameObject> targets;

	private Transform my_transform;
	private Collider terrain_collider;
	private float terrain_height;
	private Animator anim;
	private float horizontal;
	private float vertical;
	private float rotation;
	private Vector3 velocity;
	private float speed;
	private FINALGUISCRIPT hud;
	public bool throw_coroutine_started;

	void Awake()
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	void Start()
	{
		this_player = GetComponent<PlayerCharacter>();
		anim = GetComponent<Animator>();
		my_transform = transform;
		terrain_collider = GameObject.FindGameObjectWithTag("Terrain").GetComponent<TerrainCollider>();
		velocity = rigidbody.velocity;
		hud = this_player.players_hud;
		AddAllTargets();
		throw_coroutine_started = false;
	}
	void Update()
	{
		if(Input.GetButtonUp("Targeting"))
		{
			TargetPokemon();
		}
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		rotation = Input.GetAxis("Rotation");
		velocity = rigidbody.velocity;
		if(Input.GetKey(KeyCode.LeftShift))
		{
			speed_limit = 1.0f;
			
		}
		else
		{
			speed_limit = 10.0f;
		}
		if (can_jump && Input.GetButtonDown("Jump"))
		{
			Jump();
		}
		if(Input.GetButtonDown("Swap"))
		{
			SwapToPokemon();
		}
		if(!this_player.player_is_in_battle)
		{

		}
		if(Input.GetKey(KeyCode.M))
		{
			anim.SetTrigger("Mount");
		}
		if(Input.GetKeyDown(KeyCode.C) && target != null)
		{
			StartCoroutine(GetComponent<ThrowPokeBall>().PokeBallGo());
		}
		SummonPokemon();
		anim.SetFloat("Speed", speed);
	}
	void FixedUpdate ()
	{
		if (grounded)
		{
			// Calculates the slope of the ground beneath the object and matches the "lean" of the object to the angle of the slope.
			RaycastHit hit;
			Ray ray = new Ray(my_transform.position, Vector3.down);
			if (terrain_collider.Raycast(ray, out hit, 1000.0f))
			{
				my_transform.rotation = Quaternion.FromToRotation(my_transform.up, hit.normal) * my_transform.rotation;
			}

			// Calculate how fast we should be moving
			Vector3 target_velocity = new Vector3(horizontal, 0.0f, vertical);
			target_velocity = transform.TransformDirection(target_velocity);
			target_velocity *= speed_limit;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity_change = (target_velocity - velocity);
			velocity_change.x = Mathf.Clamp(velocity_change.x, -max_velocity_change, max_velocity_change);
			velocity_change.z = Mathf.Clamp(velocity_change.z, -max_velocity_change, max_velocity_change);
			velocity_change.y = 0;
			rigidbody.AddForce(velocity_change, ForceMode.VelocityChange);

			// Rotates on the Y axis.
			transform.Rotate(0.0f, rotation * rotate_speed, 0.0f);

			speed = vertical * speed_limit;
		}
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	void OnCollisionStay()
	{
		grounded = true;    
	}

	private void Jump()
	{
		rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
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
			hud.SetTarget(target.GetComponent<Pokemon>());
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
