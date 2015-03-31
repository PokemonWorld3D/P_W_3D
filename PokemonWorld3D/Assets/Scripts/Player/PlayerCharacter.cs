using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayerCharacter : MonoBehaviour
{
	public string playersName;
	public Genders gender;
	public int funds;
	public string lastZone;
	public Vector3 lastPosition;
	public PlayerPokemonRoster pokemonRoster;
	public PlayerPokemonInventory pokemonInventory;
	public List<string> inventory;
	public bool canBattle;
	public bool isInBattle;
	public GameObject opponent;
	public GameObject activePokemon;
	public bool isInParty;
	public PlayerCharacter[] PartyMembers = new PlayerCharacter[6];
	public HUD hud;
	
	public enum Genders
	{MALE, FEMALE, NONE}

	void Update()
	{
		if(opponent != null && opponent.CompareTag("Player"))
		{
			PlayerCharacter vs = opponent.GetComponent<PlayerCharacter>();
			int ko = 0;
			for(int i = 0; i < vs.pokemonRoster.pokemonRoster.Count; i++)
			{
				if(vs.pokemonRoster.pokemonRoster[i].curHP == 0)
					ko++;
			}
			if(ko >= vs.pokemonRoster.pokemonRoster.Count)
			{
				isInBattle = false;
			}
		}
		if(opponent != null && opponent.CompareTag("Pokemon"))
		{
			Pokemon vs = opponent.GetComponent<Pokemon>();
			if(vs.curHP == 0)
			{
				isInBattle = false;
			}
		}
	}

	[RPC]
	public void BattleRequest(PhotonPlayer requestingPlayer, int requester)
	{
		hud.otherPlayer = requestingPlayer;
		hud.requestingTrainer = PhotonView.Find(requester).gameObject;
		hud.battleRequestPanel.SetActive(true);
	}
	[RPC]
	public void StartTrainerBattle(int versus)
	{
		GameObject theOpponent = PhotonView.Find(versus).gameObject;
		isInBattle = true;
		GetComponent<Animator>().SetBool("InBattle", isInBattle);
		opponent = theOpponent;
	}
	[RPC]
	public void EndRequest()
	{
		opponent.GetComponent<PhotonView>().RPC("EndTrainerBattle", PhotonTargets.AllBuffered);
		GetComponent<PhotonView>().RPC("EndTrainerBattle", PhotonTargets.AllBuffered);
	}
	[RPC]
	public void EndTrainerBattle(PhotonPlayer versus)
	{
		isInBattle = false;
		GetComponent<Animator>().SetBool("InBattle", isInBattle);
		opponent = null;

	}
	public void Save()
	{
		PlayerPrefs.SetString("Players Name", playersName);
		PlayerPrefs.SetInt("Players Gender", (int)gender);
		PlayerPrefs.SetInt("Players Funds", funds);
		PlayerPrefs.SetString("Last Zone", lastZone);
		PlayerPrefsX.SetVector3("Last Position", lastPosition);
		PlayerPrefsX.SetBool("Can Battle", canBattle);
		pokemonRoster.Save(Path.Combine(Application.persistentDataPath, "roster.xml"));
		pokemonInventory.Save(Path.Combine(Application.persistentDataPath, "pinventory.xml"));
	}
	public void Load()
	{
		playersName = PlayerPrefs.GetString("Players Name");
		gender = (PlayerCharacter.Genders)PlayerPrefs.GetInt("Players Gender");
		funds = PlayerPrefs.GetInt("Players Funds");
		lastZone = PlayerPrefs.GetString("Last Zone");
		lastPosition = PlayerPrefsX.GetVector3("Last Position");
		canBattle = PlayerPrefsX.GetBool("Can Battle");
		if(File.Exists(Path.Combine(Application.persistentDataPath, "roster.xml")))
		{
			pokemonRoster = PlayerPokemonRoster.Load(Path.Combine(Application.persistentDataPath, "roster.xml"));
		}
		if(File.Exists(Path.Combine(Application.persistentDataPath, "pinventory.xml")))
		{
			pokemonInventory = PlayerPokemonInventory.Load(Path.Combine(Application.persistentDataPath, "pinventory.xml"));
		}
		GetComponent<PhotonView>().RPC("NetworkPlayer", PhotonTargets.AllBuffered, playersName, (int)gender, funds, lastZone, lastPosition, canBattle);
	}
	[RPC]
	public void NetworkPlayer(string theName, int theGender, int theFunds, string theLastZone, Vector3 theLastPosition, bool theCanBattle)
	{
		playersName = theName;
		gender = (Genders)theGender;
		funds = theFunds;
		lastZone = theLastZone;
		lastPosition = theLastPosition;
		canBattle = theCanBattle;
	}
	public void Quit()
	{
		Save();
		Application.Quit();
	}
	public void SetActivePokemon(GameObject theActivePokemon)
	{
		activePokemon = theActivePokemon;
		hud.SetActivePokemon(activePokemon.GetComponent<Pokemon>());
	}
	public void RemoveActivePokemon()
	{
		activePokemon = null;
		hud.RemoveActivePokemon();
	}
}
