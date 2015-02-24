using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour
{
	public string players_name;
	public Genders players_gender;
	public int players_funds;
	public string players_last_zone;
	public Vector3 players_last_location;
	public List<PlayerPokemonData> players_pokemon_roster;
	public List<PlayerPokemonData> players_pokemon_inventory;
	public List<Item> players_inventory;
	public bool player_is_in_battle;
	public GameObject players_active_pokemon;
	public bool player_is_in_party;
	public PlayerCharacter[] party_members = new PlayerCharacter[6];
	public FINALGUISCRIPT players_hud;
	
	public enum Genders
	{MALE, FEMALE, NONE}

	public void SetActivePokemon(GameObject active_pokemon)
	{
		players_active_pokemon = active_pokemon;
		players_hud.SetActivePokemon(active_pokemon.GetComponent<Pokemon>());
	}
	public void RemoveActivePokemon()
	{
		players_active_pokemon = null;
		players_hud.RemoveActivePokemon();
	}
}
