using UnityEngine;
using System.Collections;

public class ThrowPokeBall : MonoBehaviour
{
	public GameObject pokeball;
	public GameObject empty_ball;
	public AudioClip poke_ball_grow;
	public AudioClip pokemon_out;
	public GameObject poke_ball_prefab;
	public GameObject empty_poke_ball_prefab;
	public GameObject grip;
	public float throw_power;
	public float max_angle_velocity;
	public PlayerCharacter this_player;
	public PlayerInput input;

	public GameObject target;
	private Animator anim;
	private Transform my_transform;

	void Start()
	{
		my_transform = transform;
		anim = GetComponent<Animator>();
		this_player = GetComponent<PlayerCharacter>();
		input = GetComponent<PlayerInput>();
	}

	void Update()
	{

	}

	public void CreateEmptyBall()
	{
		audio.PlayOneShot(poke_ball_grow);
		empty_ball = Instantiate(empty_poke_ball_prefab, grip.transform.position, grip.transform.rotation) as GameObject;
		empty_ball.GetComponent<EmptyPokeBall>().this_player = this_player;
		empty_ball.transform.parent = grip.transform;
		empty_ball.collider.enabled = false;
		empty_ball.rigidbody.useGravity = false;
	}
	public void CreatePokemonBall()
	{
		audio.PlayOneShot(poke_ball_grow);
		pokeball = Instantiate(poke_ball_prefab, grip.transform.position, grip.transform.rotation) as GameObject;
		pokeball.transform.parent = grip.transform;
		pokeball.collider.enabled = false;
		pokeball.rigidbody.useGravity = false;
	}
	public void ThrowEmptyBall()
	{
		empty_ball.transform.parent = null;
		empty_ball.rigidbody.useGravity = true;
		Vector3 target_pos = target.GetComponentInChildren<Renderer>().renderer.bounds.center;
		Vector3 throw_speed = calculateBestThrowSpeed(empty_ball.transform.position, target_pos, 1.0f);
		empty_ball.rigidbody.AddForce(throw_speed, ForceMode.VelocityChange);
		empty_ball.collider.enabled = true;
	}
	public void ThrowPokemonBall()
	{
		pokeball.transform.parent = null;
		pokeball.rigidbody.useGravity = true;
		pokeball.rigidbody.AddForce(transform.forward * 1000);
		pokeball.collider.enabled = true;
		pokeball.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * throw_power);
		pokeball.GetComponent<Rigidbody>().AddTorque(10, 0, 0);
		pokeball.GetComponent<Rigidbody>().maxAngularVelocity = max_angle_velocity;
	}

	private void OpenBall()
	{
		pokeball.animation["Open"].speed = 2;
		pokeball.animation.Play("Open");
	}
	private void CloseBall()
	{
		pokeball.animation["Close"].speed = -5f;
		pokeball.animation["Close"].time = pokeball.animation["Close"].length;
		pokeball.animation.Play("Close");
	}
	private void HandOverStats(Pokemon pokemon, PlayerPokemonData data)
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
		pokemon.moves_to_learn_names = data.moves_to_learn;
		pokemon.known_moves_names = data.known_moves;
		pokemon.last_move_used = data.last_move_used;
		pokemon.equipped_item = data.equipped_item;
		pokemon.origin = data.origin;
		pokemon.is_shiny = data.is_shiny;
	}
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;
		
		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;
		
		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;
		
		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)
		
		return result;
	}

	public IEnumerator PokeBallGo()
	{
		target = GetComponent<PlayerInput>().target;
		anim.SetTrigger("EmptyPokeBallThrow");
		yield return null;
	}
	public IEnumerator PokemonGo(PlayerPokemonData data)
	{
		anim.SetTrigger("PokemonPokeBallThrow");
		yield return new WaitForSeconds(1.0f);
		while(Vector3.Distance(my_transform.position, pokeball.transform.position) < 10.0f)
		{
			yield return null;
		}
		pokeball.collider.enabled = false;
		pokeball.rigidbody.Sleep();
		pokeball.transform.LookAt(transform.forward);
		OpenBall();
		pokeball.audio.PlayOneShot(pokemon_out);
		yield return new WaitForSeconds(pokemon_out.length);
		GameObject pokemon_to_release = (GameObject)Resources.Load("Prefabs/" + data.pokemon_name.ToString());
		float current_terrain_height = Terrain.activeTerrain.SampleHeight (pokeball.transform.position);
		Vector3 here = new Vector3(pokeball.transform.position.x, current_terrain_height, pokeball.transform.position.z);
		GameObject pokemon = Instantiate(pokemon_to_release, here, Quaternion.identity)as GameObject;
		CloseBall();
		Vector3 move_to = new Vector3(my_transform.position.x, my_transform.position.y, my_transform.position.z);
		while(Vector3.Distance(pokeball.transform.position, move_to) > 1f){
			pokeball.transform.position = Vector3.Lerp(pokeball.transform.position, move_to, 5f * Time.deltaTime);
			yield return null;
		}
		HandOverStats(pokemon.GetComponent<Pokemon>(), data);
		pokemon.GetComponent<Pokemon>().SetupSetupPokemon();
		this_player.SetActivePokemon(pokemon);
		Destroy(pokeball);
		input.throw_coroutine_started = false;
		yield return null;
	}
}
