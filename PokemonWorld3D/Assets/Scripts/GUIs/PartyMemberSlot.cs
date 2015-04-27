using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PartyMemberSlot : MonoBehaviour
{
	public PlayerCharacter trainer;
	public Text trainersName;
	public Pokemon activePokemon;
	public Text activePokemonName;
	public Image activePokemonHP;
	public Image activePokemonPP;
	public Text activePokemonHitPoints;
	public Text activePokemonPowerPoints;

	void Update()
	{
		if(activePokemon != null)
		{
			activePokemonHP.fillAmount = ((float)activePokemon.curHP / (float)activePokemon.curMaxHP);
			activePokemonPP.fillAmount = ((float)activePokemon.curPP / (float)activePokemon.curMaxPP);
			activePokemonHitPoints.text = activePokemon.curHP.ToString();
			activePokemonPowerPoints.text = activePokemon.curPP.ToString();
		}
		else
		{
			activePokemonName.gameObject.SetActive(false);
			activePokemonHP.gameObject.SetActive(false);
			activePokemonPP.gameObject.SetActive(false);
			activePokemonHitPoints.gameObject.SetActive(false);
			activePokemonPowerPoints.gameObject.SetActive(false);
		}
	}

	public void Setup(PlayerCharacter theTrainer)
	{
		trainer = theTrainer;
		trainersName.text = trainer.playersName;
		if(trainer.activePokemon != null)
		{
			activePokemon = trainer.activePokemon.GetComponent<Pokemon>();
			activePokemonName.gameObject.SetActive(true);
			activePokemonHP.gameObject.SetActive(true);
			activePokemonPP.gameObject.SetActive(true);
			activePokemonHitPoints.gameObject.SetActive(true);
			activePokemonPowerPoints.gameObject.SetActive(true);
			if(activePokemon.nickName != "")
			{
				activePokemonName.text = "Lv. " + activePokemon.level + "   " + activePokemon.nickName;
			}
			else
			{
				activePokemonName.text = "Lv. " + activePokemon.level + "   " + activePokemon.pokemonName;
			}
		}
	}
}
