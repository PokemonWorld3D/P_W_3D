using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
	public GameObject owner;
	public Text chatOutput;
	public InputField chatInput;
	public GameObject target;
	public PlayerCharacter targetTrainer;
#region Pokemon Canvas
	public GameObject pokemonCanvas;
	public GameObject playerPokemonPortrait;
	public GameObject movePanel;
	public GameObject thisPokemon;
	public Pokemon activePokemon;
	public Text pokemonInfo;
	public Image hpBar;
	public Image ppBar;
	public Image expBar;
	public Image avatar;
	public Text hitPoints;
	public Text powerPoints;
	public Text experiencePoints;
	public Pokemon targetPokemon;
	public GameObject targetPokemonPortrait;
	public Text targetInfo;
	public Image targetHPBar;
	public Image targetAvatar;
	public GameObject partyPanel;
	public Text[] partyMemberLevelName;
	public Image[] partyMemberHP;
	public Image[] partyMemberPP;
	public Text[] partyMemberHitPoints;
	public Text[] PartyMemberPowerPoints;
	public GameObject moveOne;
	public Image moveOneIcon;
	public Image moveOneTimer;
	public Text moveOnePP;
	public GameObject move_two;
	public Image move_two_icon;
	public Image move_two_timer;
	public Text move_two_pp;
	public GameObject move_three;
	public Image move_three_icon;
	public Image move_three_timer;
	public Text move_three_pp;
	public GameObject move_four;
	public Image move_four_icon;
	public Image move_four_timer;
	public Text move_four_pp;
	public GameObject move_five;
	public Image move_five_icon;
	public Image move_five_timer;
	public Text move_five_pp;
	public GameObject move_six;
	public Image move_six_icon;
	public Image move_six_timer;
	public Text move_six_pp;
	public GameObject move_seven;
	public Image move_seven_icon;
	public Image move_seven_timer;
	public Text move_seven_pp;
	public GameObject move_eight;
	public Image move_eight_icon;
	public Image move_eight_timer;
	public Text move_eight_pp;
	public GameObject move_nine;
	public Image move_nine_icon;
	public Image move_nine_timer;
	public Text move_nine_pp;
	public GameObject move_ten;
	public Image move_ten_icon;
	public Image move_ten_timer;
	public Text move_ten_pp;
#endregion
	public GameObject menu;
	public GameObject wildPokemonPanel;
	public GameObject otherTrainerPanel;
	public GameObject battleRequestPanel;

	public PhotonPlayer otherPlayer;
	public GameObject requestingTrainer;
	private PhotonPlayer thisPlayer;

	void Update()
	{
		if(playerPokemonPortrait.activeInHierarchy)
			HandlePlayerPokemonGUI();
		if(targetPokemonPortrait.activeInHierarchy)
			HandleTargetGUI();
		if(targetPokemon == null || targetPokemon.Equals(null))
			NoTargetPokemon();
	}

	public void Save()
	{
		owner.GetComponent<PlayerCharacter>().Save();
	}
	public void Quit()
	{
		owner.GetComponent<PlayerCharacter>().Quit();
	}
	public void SetActivePokemon(Pokemon playersActivePokemon)
	{
		activePokemon = playersActivePokemon;
		thisPokemon = owner.GetComponent<PlayerCharacter>().activePokemon;
		playerPokemonPortrait.SetActive(true);
		movePanel.SetActive(true);
	}
	public void RemoveActivePokemon()
	{
		activePokemon = null;
		thisPokemon = null;
		playerPokemonPortrait.SetActive(false);
		movePanel.SetActive(false);
	}
	public void SetTargetPokemon(Pokemon playersTarget)
	{
		targetPokemon = playersTarget;
		targetPokemonPortrait.SetActive(true);
	}
	public void NoTargetPokemon()
	{
		targetPokemon = null;
		targetPokemonPortrait.SetActive(false);
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
			otherPlayer = targetTrainer.gameObject.GetComponent<PhotonView>().owner;
			targetTrainer.gameObject.GetComponent<PhotonView>().RPC("BattleRequest", otherPlayer, thisPlayer, me);
		}
	}
	[RPC]
	public void AcceptBattleRequest()
	{
		int me = owner.GetComponent<PhotonView>().viewID;
		int opponent = requestingTrainer.gameObject.GetComponent<PhotonView>().viewID;
		owner.GetComponent<PhotonView>().RPC("StartTrainerBattle", PhotonTargets.AllBuffered, opponent);
		requestingTrainer.gameObject.GetComponent<PhotonView>().RPC("StartTrainerBattle", PhotonTargets.AllBuffered, me);

	}
	[RPC]
	public void DeclineBattleRequest()
	{

	}
	public void HandlePlayerPokemonGUI()
	{
		string pokemonsName = activePokemon.pokemonName;
		if(activePokemon.nickName != "")
		{
			pokemonsName = activePokemon.nickName;
		}
		pokemonInfo.text = "Level " + activePokemon.level + " " + pokemonsName;
		int currentHP = activePokemon.curHP;
		int currentMaxHP = activePokemon.curMaxHP;
		avatar.sprite = activePokemon.avatar;
		hitPoints.text = "HP : " + currentHP + " / " + currentMaxHP;
		int currentPP = activePokemon.curPP;
		int currentMaxPP = activePokemon.curMaxPP;
		powerPoints.text = "PP : " + currentPP + " / " + currentMaxPP;
		int lastEXP = activePokemon.lastRequiredEXP;
		int currentEXP = activePokemon.currentEXP;
		int requiredEXP = activePokemon.nextRequiredEXP;
		experiencePoints.text = "EXP : " + currentEXP + " / " + requiredEXP;

		float hpFillAmount = ((float)currentHP / (float)currentMaxHP);
		hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hpFillAmount, Time.deltaTime * 5.0f);
		if(currentHP > currentMaxHP / 2){ //More than 50% health.
			hpBar.color = new Color32((byte)CalculateValue(currentHP, currentMaxHP / 2, currentMaxHP, 255, 0), 255, 0, 255);
		}else{ //Less than 50% health.
			hpBar.color = new Color32(255, (byte)CalculateValue(currentHP, 0, currentMaxHP / 2, 0, 255), 0 , 255);
		}

		float ppFillAmount = ((float)currentPP / (float)currentMaxPP);
		ppBar.fillAmount = Mathf.Lerp(ppBar.fillAmount, ppFillAmount, Time.deltaTime * 5.0f);
		float expFillAmount = (((float)currentEXP - (float)lastEXP) / ((float)requiredEXP - (float)lastEXP));
		expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, expFillAmount, Time.deltaTime * 5.0f);
		#region Moves
		Color inactive_color = new Color32(255, 255, 255, 0);
		Color icon_color = new Color32(255, 255, 255, 255);
		if(activePokemon.KnownMoves.Count >= 1)
		{
			moveOne.SetActive(true);
			moveOneIcon.color = icon_color;
			moveOneIcon.sprite = activePokemon.KnownMoves[0].icon;
			moveOneTimer.fillAmount = activePokemon.KnownMoves[0].coolingDown / activePokemon.KnownMoves[0].coolDown;
			moveOnePP.text = activePokemon.KnownMoves[0].ppCost.ToString();
		}
		else
		{
			moveOneIcon.color = inactive_color;
			moveOne.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 2)
		{
			move_two.SetActive(true);
			move_two_icon.color = icon_color;
			move_two_icon.sprite = activePokemon.KnownMoves[1].icon;
			move_two_timer.fillAmount = activePokemon.KnownMoves[1].coolingDown / activePokemon.KnownMoves[1].coolDown;
			move_two_pp.text = activePokemon.KnownMoves[1].ppCost.ToString();
		}
		else
		{
			move_two_icon.color = inactive_color;
			move_two.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 3)
		{
			move_three.SetActive(true);
			move_three_icon.color = icon_color;
			move_three_icon.sprite = activePokemon.KnownMoves[2].icon;
			move_three_timer.fillAmount = activePokemon.KnownMoves[2].coolingDown / activePokemon.KnownMoves[2].coolDown;
			move_three_pp.text = activePokemon.KnownMoves[2].ppCost.ToString();
		}
		else
		{
			move_three_icon.color = inactive_color;
			move_three.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 4)
		{
			move_four.SetActive(true);
			move_four_icon.color = icon_color;
			move_four_icon.sprite = activePokemon.KnownMoves[3].icon;
			move_four_timer.fillAmount = activePokemon.KnownMoves[3].coolingDown / activePokemon.KnownMoves[3].coolDown;
			move_four_pp.text = activePokemon.KnownMoves[3].ppCost.ToString();
		}
		else
		{
			move_four_icon.color = inactive_color;
			move_four.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 5)
		{
			move_five.SetActive(true);
			move_five_icon.color = icon_color;
			move_five_icon.sprite = activePokemon.KnownMoves[4].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_five_pp.text = activePokemon.KnownMoves[4].ppCost.ToString();
		}
		else
		{
			move_five_icon.color = inactive_color;
			move_five.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 6)
		{
			move_six.SetActive(true);
			move_six_icon.color = icon_color;
			move_six_icon.sprite = activePokemon.KnownMoves[5].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_six_pp.text = activePokemon.KnownMoves[5].ppCost.ToString();
		}
		else
		{
			move_six_icon.color = inactive_color;
			move_six.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 7)
		{
			move_seven.SetActive(true);
			move_seven_icon.color = icon_color;
			move_seven_icon.sprite = activePokemon.KnownMoves[6].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_seven_pp.text = activePokemon.KnownMoves[6].ppCost.ToString();
		}
		else
		{
			move_seven_icon.color = inactive_color;
			move_seven.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 8)
		{
			move_eight.SetActive(true);
			move_eight_icon.color = icon_color;
			move_eight_icon.sprite = activePokemon.KnownMoves[7].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_eight_pp.text = activePokemon.KnownMoves[7].ppCost.ToString();
		}
		else
		{
			move_eight_icon.color = inactive_color;
			move_eight.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 9)
		{
			move_nine.SetActive(true);
			move_nine_icon.color = icon_color;
			move_nine_icon.sprite = activePokemon.KnownMoves[8].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_nine_pp.text = activePokemon.KnownMoves[8].ppCost.ToString();
		}
		else
		{
			move_nine_icon.color = inactive_color;
			move_nine.SetActive(false);
		}
		if(activePokemon.KnownMoves.Count >= 10)
		{
			move_ten.SetActive(true);
			move_ten_icon.color = icon_color;
			move_ten_icon.sprite = activePokemon.KnownMoves[9].icon;
			//		move_one_timer = active_pokemon.known_moves[0]. ...
			move_ten_pp.text = activePokemon.KnownMoves[9].ppCost.ToString();
		}
		else
		{
			move_ten_icon.color = inactive_color;
			move_ten.SetActive(false);
		}
		#endregion
	}
	public void HandleTargetGUI()
	{
		string pokemonsName = targetPokemon.pokemonName;
		if(targetPokemon.nickName != "")
		{
			pokemonsName = activePokemon.nickName;
		}
		targetInfo.text = "Level " + targetPokemon.level + " " + pokemonsName;
		int current_hp = targetPokemon.curHP;
		int current_max_hp = targetPokemon.curMaxHP;
		targetAvatar.sprite = targetPokemon.avatar;

		float hpfillAmount = ((float)current_hp / (float)current_max_hp);
		targetHPBar.fillAmount = Mathf.Lerp(targetHPBar.fillAmount, hpfillAmount, Time.deltaTime * 5.0f);
		if(current_hp > current_max_hp / 2){ //More than 50% health.
			targetHPBar.color = new Color32((byte)CalculateValue(current_hp, current_max_hp / 2, current_max_hp, 255, 0), 255, 0, 255);
		}else{ //Less than 50% health.
			targetHPBar.color = new Color32(255, (byte)CalculateValue(current_hp, 0, current_max_hp / 2, 0, 255), 0 , 255);
		}
	}
	private float CalculateValue(float curValue, float minValue, float maxValue, float minXPos, float maxXPos)
	{
		return (curValue - minValue) * (maxXPos - minXPos) / (maxValue - minValue) + minXPos;
	}

}
