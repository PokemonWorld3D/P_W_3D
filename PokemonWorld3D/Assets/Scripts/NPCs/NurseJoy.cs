using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NurseJoy : MonoBehaviour {

	private PlayerInput input;
	private GameObject hud;
	private GameObject diagBox;
	private GameObject diag;
	private Text diagText;
	private PlayerCharacter player;

	void Start(){
		hud = GameObject.FindGameObjectWithTag("HUD");
		diagBox = hud.gameObject.transform.GetChild(0).GetChild(2).gameObject;
		diag = diagBox.gameObject.transform.GetChild(0).gameObject;
		diagText = diag.GetComponent<Text>();
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.Space)){
				GameObject player = other.gameObject;
				StartCoroutine(Dialogue(player));
			}
		}
	}

	private IEnumerator Dialogue(GameObject player){
		input = player.GetComponent<PlayerInput>();
		input.enabled = false;
		player.GetComponent<Animator>().SetFloat("Speed", 0f);
		diagBox.SetActive(true);
		diag.SetActive(true);
		diagText.text = "" + "Hello! I'm nurse Joy! I'm here helping new trainers by providing treatment to their wounded Pokémon in the field." + "";
		while(!Input.GetKeyDown(KeyCode.Space)){
			yield return null;
		}
		diagText.text = "" + "Would you like me to treat your Pokémon?" + "";
		while(!Input.GetKeyDown(KeyCode.Y) && !Input.GetKeyDown(KeyCode.N)){
			yield return null;
		}
		if(Input.GetKeyDown(KeyCode.Y)){
			HealPlayersPokemon(player.GetComponent<PlayerCharacter>());
		}
		if(Input.GetKeyDown(KeyCode.N)){
			diag.SetActive(false);
			diagBox.SetActive(false);
			input.enabled = true;
			yield break;
		}
		diagText.text = "" + "There, all better! Your Pokémon have been restored to full health! Good luck on your journey and I'm here if you need me!" + "";
		while(!Input.GetKeyDown(KeyCode.Space)){
			yield return null;
		}
		diag.SetActive(false);
		diagBox.SetActive(false);
		input.enabled = true;
		yield return null;
	}

	private void HealPlayersPokemon(PlayerCharacter player){
		foreach(PlayerPokemonData data in player.players_pokemon_roster){
			data.cur_hp = data.cur_max_hp;
			data.status_condition = Pokemon.StatusConditions.NONE;
		}
	}

}
