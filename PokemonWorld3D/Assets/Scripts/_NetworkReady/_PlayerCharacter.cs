using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class _PlayerCharacter : MonoBehaviour
{
	public string playersName;
	public Genders gender;
	public int funds;
	public string lastZone;
	public Vector3 lastPosition;
	public _PlayerPokemonRoster pokemonRoster;
	public _PlayerPokemonInventory pokemonInventory;
	public List<string> inventory;
	public bool isInBattle;
	public GameObject activePokemon;
	public bool isInParty;
	public _PlayerCharacter[] PartyMembers = new _PlayerCharacter[6];
	public _HUD hud;
	
	public enum Genders
	{MALE, FEMALE, NONE}

	public void Save()
	{
		PlayerPrefs.SetString("Players Name", playersName);
		PlayerPrefs.SetInt("Players Gender", (int)gender);
		PlayerPrefs.SetInt("Players Funds", funds);
		PlayerPrefs.SetString("Last Zone", lastZone);
		PlayerPrefsX.SetVector3("Last Position", lastPosition);
		pokemonRoster.Save(Path.Combine(Application.persistentDataPath, "roster.xml"));
		pokemonInventory.Save(Path.Combine(Application.persistentDataPath, "pinventory.xml"));
	}
	public void Load()
	{
		playersName = PlayerPrefs.GetString("Players Name");
		gender = (_PlayerCharacter.Genders)PlayerPrefs.GetInt("Players Gender");
		funds = PlayerPrefs.GetInt("Players Funds");
		lastZone = PlayerPrefs.GetString("Last Zone");
		lastPosition = PlayerPrefsX.GetVector3("Last Position");
		if(File.Exists("roster.xml"))
			pokemonRoster = _PlayerPokemonRoster.Load(Path.Combine(Application.persistentDataPath, "roster.xml"));
		if(File.Exists("pinventory.xml"))
			pokemonInventory = _PlayerPokemonInventory.Load(Path.Combine(Application.persistentDataPath, "pinventory.xml"));
	}
	public void Quit()
	{
		Save();
		Application.Quit();
	}
	public void SetActivePokemon(GameObject theActivePokemon)
	{
		activePokemon = theActivePokemon;
		hud.SetActivePokemon(activePokemon.GetComponent<_Pokemon>());
	}
	public void RemoveActivePokemon()
	{
		activePokemon = null;
		hud.RemoveActivePokemon();
	}
}
