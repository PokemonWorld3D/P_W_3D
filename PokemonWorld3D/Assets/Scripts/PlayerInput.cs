using UnityEngine;
using System.Collections;

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
	private GameObject pokeball;
	public AudioClip poke_ball_grow;
	public AudioClip pokemon_out;
	public GameObject empty_ball_prefab;
	public GameObject pokemon_poke_ball_prefab;
	public GameObject grip;
	public float throw_power;
	public float max_angle_velocity;

	private Transform my_transform;
	private Collider terrain_collider;
	private float terrain_height;
	private Animator anim;
	private float horizontal;
	private float vertical;
	private float rotation;
	private Vector3 velocity;
	private float speed;
	private bool throw_coroutine_started;

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
		throw_coroutine_started = false;
	}
	void Update()
	{
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
		if(Input.GetKeyDown(KeyCode.LeftControl))
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
	private void HandOverStats(NEWPokemon pokemon, NEWPlayerPokemonData data)
	{
		pokemon.is_setup = data.is_setup;
		pokemon.is_captured = data.is_captured;
		pokemon.trainer = this.gameObject;
		pokemon.trainers_name = data.trainers_name;
		pokemon.nick_name = data.nick_name;
		pokemon.is_from_trade = data.is_from_trade;
		pokemon.level = data.level;
		pokemon.gender = data.gender;
		pokemon.nature = data.nature;
		pokemon.cur_hp = data.cur_hp;
		pokemon.hp_ev = data.hp_ev;
		pokemon.atk_ev = data.atk_ev;
		pokemon.def_ev = data.def_ev;
		pokemon.spatk_ev = data.spatk_ev;
		pokemon.spdef_ev = data.spdef_ev;
		pokemon.spd_ev = data.spd_ev;
		pokemon.hp_iv = data.hp_iv;
		pokemon.atk_iv = data.atk_iv;
		pokemon.def_iv = data.def_iv;
		pokemon.spatk_iv = data.spatk_iv;
		pokemon.spdef_iv = data.spdef_iv;
		pokemon.spd_iv = data.spd_iv;
		pokemon.last_required_exp = data.last_required_exp;
		pokemon.current_exp = data.current_exp;
		pokemon.next_required_exp = data.next_required_exp;
		pokemon.status_condition = data.status_condition;
		pokemon.moves_to_learn = data.moves_to_learn;
		pokemon.known_moves = data.known_moves;
		pokemon.last_move_used = data.last_move_used;
		pokemon.equipped_item = data.equipped_item;
		pokemon.origin = data.origin;
		pokemon.is_shiny = data.is_shiny;
	}
	public void CreatePokeBall()
	{
		audio.PlayOneShot(poke_ball_grow);
		pokeball = Instantiate(pokemon_poke_ball_prefab, grip.transform.position, grip.transform.rotation) as GameObject;
		pokeball.transform.parent = grip.transform;
		pokeball.collider.enabled = false;
		pokeball.rigidbody.useGravity = false;
	}
	public void ThrowBall()
	{
		pokeball.transform.parent = null;
		pokeball.rigidbody.useGravity = true;
		pokeball.rigidbody.AddForce(transform.forward * 1000);
		pokeball.collider.enabled = true;
		pokeball.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throw_power);
		pokeball.GetComponent<Rigidbody>().AddTorque(10, 0, 0);
		pokeball.GetComponent<Rigidbody>().maxAngularVelocity = max_angle_velocity;
	}
	private void SummonPokemon(){
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha1) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 1){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[0]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha2) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 2){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[1]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha3) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 3){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[2]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha4) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 4){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[3]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha5) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count >= 5){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[4]));
		}
		if(Input.GetMouseButton(1) && Input.GetKey(KeyCode.Alpha6) && this_player.players_active_pokemon == null
		   && !throw_coroutine_started && this_player.players_pokemon_roster.Count == 6){
			throw_coroutine_started = true;
			StartCoroutine(PokemonGo(this_player.players_pokemon_roster[5]));
		}
	}

	private IEnumerator PokemonGo(NEWPlayerPokemonData data)
	{
		anim.SetTrigger("PokeBallThrow");
		yield return new WaitForSeconds(1.0f);
		while(Vector3.Distance(my_transform.position, pokeball.transform.position) < 10.0f)
		{
			yield return null;
		}
		pokeball.collider.enabled = false;
		pokeball.rigidbody.Sleep();
		pokeball.transform.LookAt(transform.forward);
		pokeball.audio.PlayOneShot(pokemon_out);
		yield return new WaitForSeconds(pokemon_out.length);
		GameObject pokemon_to_release = (GameObject)Resources.Load("Prefabs/" + data.pokemon_name.ToString());
		float current_terrain_height = Terrain.activeTerrain.SampleHeight (pokeball.transform.position);
		Vector3 here = new Vector3(pokeball.transform.position.x, current_terrain_height, pokeball.transform.position.z);
		GameObject pokemon = Instantiate(pokemon_to_release, here, Quaternion.identity)as GameObject;
		Vector3 move_to = new Vector3(my_transform.position.x, my_transform.position.y, my_transform.position.z);
		while(Vector3.Distance(pokeball.transform.position, move_to) > 1f){
			pokeball.transform.position = Vector3.Lerp(pokeball.transform.position, move_to, 5f * Time.deltaTime);
			yield return null;
		}
		HandOverStats(pokemon.GetComponent<NEWPokemon>(), data);
		this_player.SetActivePokemon(pokemon);
		Destroy(pokeball);
		throw_coroutine_started = false;
		yield return null;
	}
}
