using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BasePlayer
{
	public string players_name {get; private set;}
	public Genders players_gender {get; private set;}
	public int players_funds {get; set;}
	public string players_last_zone {get; set;}
	public Vector3 players_last_location {get; set;}
	public List<NEWPlayerPokemonData> players_pokemon_roster {get; set;}
	public List<NEWPlayerPokemonData> players_pokemon_inventory {get; set;}
	public List<Item> players_inventory {get; set;}
	public bool is_in_battle {get; set;}

	public enum Genders
	{MALE, FEMALE, NONE}

	public BasePlayer()
	{
		players_name = string.Empty;
		players_gender = Genders.NONE;
		players_funds = 0;
		players_last_zone = string.Empty;
		players_last_location = Vector3.zero;
		players_pokemon_roster = new List<NEWPlayerPokemonData>();
		players_pokemon_inventory = new List<NEWPlayerPokemonData>();
		players_inventory = new List<Item>();
		is_in_battle = false;
	}

	public BasePlayer(string this_name, Genders this_gender, int this_funds, string this_last_zone, Vector3 this_last_location,
	                  List<NEWPlayerPokemonData> this_pkmn_roster, List<NEWPlayerPokemonData> this_pkmn_inventory, List<Item> this_inventory, bool this_battle)
	{
		players_name = this_name;
		players_gender = this_gender;
		players_funds = this_funds;
		players_last_zone = this_last_zone;
		players_last_location = this_last_location;
		players_pokemon_roster = this_pkmn_roster;
		players_pokemon_inventory = this_pkmn_inventory;
		players_inventory = this_inventory;
		is_in_battle = this_battle;
	}
}
