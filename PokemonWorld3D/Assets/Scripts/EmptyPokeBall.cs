using UnityEngine;
using System.Collections;

public class EmptyPokeBall : MonoBehaviour
{
	public PokeBall.PokeBallTypes this_poke_ball_type;
	public PlayerCharacter this_player;
	public Animator anim;
	public AudioClip capture_attempt;
	public AudioClip attempting_capture;
	public AudioClip capture_success;
	public AudioClip capture_fail;
	public GameObject capture_orb_prefab;

	private RaycastHit hit;
	private float distance_to_ground;
	private CalculateCapture calculate_capture_script = new CalculateCapture();

	void Start()
	{
		anim = GetComponent<Animator>();
		this_player = transform.GetComponentInParent<PlayerCharacter>();
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
			NEWPokemon this_pokemon = col.gameObject.GetComponent<NEWPokemon>();
			if(!this_pokemon.is_captured)
			{
				audio.PlayOneShot(capture_attempt);
				col.gameObject.GetComponent<Animator>().enabled = false;
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Code goes here to change the Pokemon to red.
			}
			else
			{

			}
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private IEnumerator Capture(Collision col)
	{
		yield return StartCoroutine(MovePokeBall(col));
		rigidbody.WakeUp();
		while(distance_to_ground > 0.2f)
		{
			yield return null;
		}
		rigidbody.isKinematic = true;
		yield return StartCoroutine(TryToCatch(col));
	}
	private IEnumerator MovePokeBall(Collision col){
		Vector3 move_to = new Vector3(transform.position.x-1.5f, col.contacts[0].point.y+1.5f, transform.position.z-1.5f);
		while(Vector3.Distance(transform.position, move_to) > 0.01f)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.Sleep();
			transform.LookAt(col.transform.position);
			transform.position = Vector3.Lerp(transform.position, move_to, 5f * Time.deltaTime);
			yield return null;
		}
		anim.SetTrigger("Open");
		GameObject orb = Instantiate(capture_orb_prefab, col.gameObject.renderer.bounds.center, Quaternion.identity) as GameObject;
		yield return new WaitForSeconds(.5f);
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Need to include some code here that makes the Pokemon shrink and until it's smaller than the capture orb and then disappear.
		col.gameObject.SetActive(false);
		while(Vector3.Distance(transform.position, orb.transform.position) > 0.01f)
		{
			Vector3 orb_target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			orb.transform.position = Vector3.Lerp(orb.transform.position, orb_target, 2.7f * Time.deltaTime);
			yield return null;
		}
		anim.SetTrigger("Close");
		Destroy(orb);
	}
	private IEnumerator TryToCatch(Collision col)
	{
		NEWPokemon this_pokemon = col.gameObject.GetComponent<NEWPokemon>();
		audio.PlayOneShot(attempting_capture);
		yield return new WaitForSeconds(attempting_capture.length+1);
		bool try_to_capture = calculate_capture_script.AttemptCapture(this_pokemon.status_condition, this_poke_ball_type, this_pokemon.cur_hp,
		                                                              this_pokemon.cur_max_hp, this_pokemon.capture_rate);
		if(try_to_capture){
			this_pokemon.GetComponent<Pokemon>().isCaptured = true;
			NEWPokemon temp = this_pokemon;
			NEWPlayerPokemonData data_holder_pokemon = new NEWPlayerPokemonData(temp.is_setup, temp.is_captured, temp.trainers_name, temp.pokemon_name,
			                                                                    temp.nick_name,
			                                                                    temp.is_from_trade, temp.level, temp.gender, temp.nature, temp.max_hp,
			                                                                    temp.cur_max_hp, temp.max_atk, temp.max_def, temp.max_spatk, temp.max_spdef,
			                                                                    temp.max_spd, temp.cur_hp, temp.cur_atk, temp.cur_def, temp.cur_spatk,
			                                                                    temp.cur_spdef, temp.cur_spd, temp.hp_ev, temp.atk_ev, temp.def_ev, temp.spatk_ev,
			                                                                    temp.spdef_ev, temp.spd_ev, temp.hp_iv, temp.atk_iv, temp.def_iv, temp.spatk_iv,
			                                                                    temp.spdef_iv, temp.spd_iv, temp.last_required_exp, temp.current_exp,
			                                                                    temp.next_required_exp, temp.status_condition, temp.moves_to_learn, temp.known_moves,
			                                                                    temp.last_move_used, temp.equipped_item, temp.origin, temp.is_shiny);
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
		}
		else
		{
// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Code goes here to change the Pokemon back from red.
			col.gameObject.SetActive(true);
			col.gameObject.GetComponent<Animator>().enabled = true;
			yield return new WaitForSeconds(capture_fail.length);
		}
		Destroy(gameObject);
		yield return null;
	}
}
