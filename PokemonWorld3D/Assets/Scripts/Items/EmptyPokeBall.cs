using UnityEngine;
using System.Collections;

public class EmptyPokeBall : MonoBehaviour
{
	public PokeBall.PokeBallTypes this_poke_ball_type;
	public PlayerCharacter this_player;
	public AudioClip capture_attempt;
	public AudioClip attempting_capture;
	public AudioClip capture_success;
	public AudioClip capture_fail;
	public GameObject capture_orb_prefab;
	public Collider col;

	private Transform my_transform;
	private RaycastHit hit;
	private float distance_to_ground;
	private CalculateCapture calculate_capture_script = new CalculateCapture();

	void Start()
	{
		my_transform = transform;
	}
	void Update()
	{
		if(Physics.Raycast(transform.position, -Vector3.up, out hit)){
			distance_to_ground = hit.distance;
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Pokemon")
		{
			Pokemon this_pokemon = col.gameObject.GetComponent<Pokemon>();
			if(!this_pokemon.is_captured)
			{
				audio.PlayOneShot(capture_attempt);
				col.gameObject.GetComponent<Animation>().enabled = false;
				//col.collider.enabled = false;
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                               // Code goes here to change the Pokemon to red.
				StartCoroutine(Capture(col));
			}
			else
			{

			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator Capture(Collision col)
	{
		rigidbody.useGravity = false;
		yield return StartCoroutine(MovePokeBall(col));
		rigidbody.WakeUp();
		rigidbody.useGravity = true;
		while(distance_to_ground > 0.2f)
		{
			yield return null;
		}
		rigidbody.isKinematic = true;
		yield return StartCoroutine(TryToCatch(col));
	}
	private IEnumerator MovePokeBall(Collision col)
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.Sleep();
		Vector3 move_to = new Vector3(transform.position.x-1.5f, col.contacts[0].point.y+1.5f, transform.position.z-1.5f);
		while(Vector3.Distance(transform.position, move_to) > 0.01f)
		{
			transform.LookAt(col.transform.position);
			transform.position = Vector3.Lerp(transform.position, move_to, 5f * Time.deltaTime);
			yield return null;
		}
		animation["Open"].speed = 5;
		animation.Play("Open");
		GameObject orb = Instantiate(capture_orb_prefab, col.gameObject.GetComponentInChildren<Renderer>().renderer.bounds.center, Quaternion.identity) as GameObject;
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Need to include some code here that makes the Pokemon shrink and until it's smaller than the capture orb and then disappear.
		col.gameObject.SetActive(false);
		while(Vector3.Distance(my_transform.position, orb.transform.position) > 0.01f)
		{
			Vector3 orb_target = new Vector3(my_transform.position.x, my_transform.position.y, my_transform.position.z);
			orb.transform.position = Vector3.Lerp(orb.transform.position, orb_target, 2.7f * Time.deltaTime);
			yield return null;
		}
		//orb.transform.parent = my_transform;
		animation["Close"].speed = -5f;
		animation["Close"].time = animation["Close"].length;
		animation.Play("Close");
		Vector3 flatFwd = new Vector3(my_transform.forward.x, 0, my_transform.forward.z);
		Quaternion fwdRotation = Quaternion.LookRotation(flatFwd, Vector3.up);
		float angle = Quaternion.Angle(my_transform.rotation, fwdRotation);
		while(angle > 1.0f)
		{
			my_transform.rotation = Quaternion.Slerp(my_transform.rotation, fwdRotation, Time.deltaTime * 5.0f);
			angle = Quaternion.Angle(my_transform.rotation, fwdRotation);
			yield return null;
		}
		Destroy(orb);
		yield return null;
	}
	private IEnumerator TryToCatch(Collision col)
	{
		Pokemon this_pokemon = col.gameObject.GetComponent<Pokemon>();
		audio.PlayOneShot(attempting_capture);
		animation["Trying_To_Catch"].speed = 2;
		animation.Play("Trying_To_Catch");
		yield return new WaitForSeconds(attempting_capture.length+1);
		bool try_to_capture = calculate_capture_script.AttemptCapture(this_pokemon.status_condition, this_poke_ball_type, this_pokemon.cur_hp,
		                                                              this_pokemon.cur_max_hp, this_pokemon.capture_rate);
		if(try_to_capture){
			this_pokemon.is_captured = true;
			this_pokemon.trainers_name = this_player.players_name;
			Pokemon temp = this_pokemon;
			PlayerPokemonData data_holder_pokemon = new PlayerPokemonData(temp.is_setup, temp.is_captured, temp.trainers_name, temp.pokemon_name,
			                                                                    temp.nick_name,
			                                                                    temp.is_from_trade, temp.level, temp.gender, temp.nature, temp.max_hp,
			                                                                    temp.cur_max_hp, temp.max_atk, temp.max_def, temp.max_spatk, temp.max_spdef,
			                                                                    temp.max_spd, temp.cur_hp, temp.cur_atk, temp.cur_def, temp.cur_spatk,
			                                                                    temp.cur_spdef, temp.cur_spd, temp.hp_ev, temp.atk_ev, temp.def_ev, temp.spatk_ev,
			                                                                    temp.spdef_ev, temp.spd_ev, temp.hp_iv, temp.atk_iv, temp.def_iv, temp.spatk_iv,
			                                                                    temp.spdef_iv, temp.spd_iv, temp.last_required_exp, temp.current_exp,
			                                                                    temp.next_required_exp, temp.status_condition, temp.moves_to_learn_names,
			                                                                    temp.known_moves_names, temp.last_move_used, temp.equipped_item, temp.origin,
			                                                                    temp.is_shiny);
			if(this_player.players_pokemon_roster.Count < 6)
			{
				this_player.players_pokemon_roster.Add(data_holder_pokemon);
			}
			else
			{
				this_player.players_pokemon_inventory.Add(data_holder_pokemon);
			}
			this_pokemon.is_captured = false;
			this_pokemon.SetDead();
			audio.PlayOneShot(capture_success);
			yield return new WaitForSeconds(capture_success.length);
		}
		else
		{
			animation["Open_Top"].speed = 5;
			animation.Play("Open_Top");
			audio.PlayOneShot(capture_fail);
			yield return new WaitForSeconds(animation["Open_Top"].length);
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Code goes here to change the Pokemon back from red.
			col.gameObject.SetActive(true);
			col.gameObject.GetComponent<Animation>().enabled = true;
			animation["Close_Top"].speed = -5f;
			animation["Close_Top"].time = animation["Close_Top"].length;
			animation.Play("Close_Top");
			yield return new WaitForSeconds(capture_fail.length);
		}
		Destroy(gameObject);
		yield return null;
	}
}
