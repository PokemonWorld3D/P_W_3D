using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
	public GameObject owner;
	public PlayerPokemonPortrait playerPokemonPortrait;
	public TargetPokemonPortrait targetPokemonPortrait;

	public Pokemon activePokemon;
	public Pokemon targetPokemon;
	public PlayerCharacter targetTrainer;
//	public Text chatOutput;
//	public InputField chatInput;
	public GameObject partyPanel;
	public GameObject partyMemberSlot;

	

	public GameObject menu;
	public GameObject wildPokemonPanel;
	public GameObject otherTrainerPanel;
	public GameObject battleRequestPanel;
	public PartyRequestPanel partyRequestPanel;
	public GameObject moveToolTip;


	public PhotonPlayer battlePlayer;
	public GameObject battleTrainer;
	public PhotonPlayer partyPlayer;
	public GameObject partyTrainer;
	private PhotonPlayer thisPlayer;
	
	public void DisplayMoveToolTip(int moveNumber)
	{
		moveToolTip.GetComponent<MoveToolTip>().SetToolTipInfo(owner.GetComponent<PlayerCharacter>().activePokemon.GetComponent<Pokemon>().KnownMoves[moveNumber]);
		moveToolTip.SetActive(true);
	}
	public void HideToolTip()
	{
		moveToolTip.SetActive(false);
	}
	public void Save()
	{
		owner.GetComponent<PlayerCharacter>().Save();
	}
	public void Quit()
	{
		owner.GetComponent<PlayerCharacter>().Quit();
 	}
	public void EnterWildPokemonBattle()
	{
		if(!targetPokemon.isInBattle && !activePokemon.isInBattle)
		{
			int opponent = targetPokemon.gameObject.GetComponent<PhotonView>().viewID;
			int me = owner.GetComponent<PlayerCharacter>().activePokemon.GetComponent<PhotonView>().viewID;
			owner.GetComponent<PlayerCharacter>().activePokemon.GetComponent<PhotonView>().RPC("StartWildPokemonBattle", PhotonTargets.AllBuffered, opponent);
			targetPokemon.gameObject.GetComponent<PhotonView>().RPC("StartWildPokemonBattle", PhotonTargets.AllBuffered, me);
		}
	}
	public void RequestTrainerBattle()
	{
		if(!targetTrainer.isInBattle)
		{
			int me = owner.GetComponent<PhotonView>().viewID;
			thisPlayer = owner.GetComponent<PhotonView>().owner;
			battlePlayer = targetTrainer.gameObject.GetComponent<PhotonView>().owner;
			targetTrainer.gameObject.GetComponent<PhotonView>().RPC("BattleRequest", battlePlayer, thisPlayer, me);
		}
	}
	[RPC]
	public void AcceptBattleRequest()
	{
		int me = owner.GetComponent<PhotonView>().viewID;
		int opponent = battleTrainer.GetComponent<PhotonView>().viewID;
		owner.GetComponent<PhotonView>().RPC("StartTrainerBattle", PhotonTargets.AllBuffered, opponent);
		battleTrainer.GetComponent<PhotonView>().RPC("StartTrainerBattle", PhotonTargets.AllBuffered, me);

	}
	[RPC]
	public void DeclineBattleRequest()
	{

 	}
	public void Party()
	{
		if(!owner.GetComponent<PlayerCharacter>().isInParty)
		{
			int me = owner.GetComponent<PhotonView>().viewID;
			thisPlayer = owner.GetComponent<PhotonView>().owner;
			partyPlayer = targetTrainer.gameObject.GetComponent<PhotonView>().owner;
			targetTrainer.gameObject.GetComponent<PhotonView>().RPC("PartyRequest", partyPlayer, thisPlayer, me);
		}
		else
		{
			if(owner.GetComponent<PlayerCharacter>().PartyMembers.Count < 2)
			{
				int me = owner.GetComponent<PhotonView>().viewID;
				thisPlayer = owner.GetComponent<PhotonView>().owner;
				partyPlayer = targetTrainer.gameObject.GetComponent<PhotonView>().owner;
				targetTrainer.gameObject.GetComponent<PhotonView>().RPC("PartyRequest", partyPlayer, thisPlayer, me);
			}
			else
			{
				Debug.Log ("There are more than 2 people in my party.");
				//DO SOMETHING TO INDICATE THE PARTY IS FULL
			}
		}
	}
	[RPC]
	public void AcceptPartyRequest()
	{
		int me = owner.GetComponent<PhotonView>().viewID;
		int other = partyTrainer.GetComponent<PhotonView>().viewID;
		if(partyTrainer.GetComponent<PlayerCharacter>().isInParty)
		{
			owner.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, other);
			foreach(PlayerCharacter player in partyTrainer.GetComponent<PlayerCharacter>().PartyMembers)
			{
				int playerViewID = player.gameObject.GetComponent<PhotonView>().viewID;
				owner.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, playerViewID);
				player.gameObject.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, me);
			}
			partyTrainer.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, me);
		}
		else
		{
			owner.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, other);
			partyTrainer.GetComponent<PhotonView>().RPC("AddToParty", PhotonTargets.AllBuffered, me);
		}
	}
	public void AddPartyMemberSlot(PlayerCharacter trainer)
	{
		GameObject slot = Instantiate(partyMemberSlot) as GameObject;
		slot.name = trainer.playersName;
		slot.transform.SetParent(partyPanel.transform);
		slot.GetComponent<PartyMemberSlot>().Setup(trainer);
	}
}
