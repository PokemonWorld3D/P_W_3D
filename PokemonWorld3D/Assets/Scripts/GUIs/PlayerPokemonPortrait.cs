using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerPokemonPortrait : MonoBehaviour
{
	public GameObject thisPokemon;
	public Pokemon pokemonScript;
	public Text pokemonInfo;
	public Image hpBar;
	public Image ppBar;
	public Image expBar;
	public Image avatar;
	public Text hitPoints;
	public Text powerPoints;
	public Text experiencePoints;
	public GameObject statusEffectPanel;
	public GameObject statusEffectPrefab;
	public GameObject[] moves;
	public Sprite[] StatusConditionSprites;
	public Sprite[] BuffDebuffSprites;

	void Update()
	{
		HandlePlayerPokemonGUI();
	}

	public void SetActivePokemon(GameObject pokemon)
	{
		thisPokemon = pokemon;
		pokemonScript = pokemon.GetComponent<Pokemon>();
		gameObject.SetActive(true);
		this.enabled = true;
	}
	public void RemoveActivePokemon()
	{
		thisPokemon = null;
		pokemonScript = null;
		this.enabled = false;
	}
	public void HandlePlayerPokemonGUI()
	{
		string pokemonsName = pokemonScript.pokemonName;
		if(pokemonScript.nickName != "")
		{
			pokemonsName = pokemonScript.nickName;
		}
		pokemonInfo.text = "Level " + pokemonScript.level + " " + pokemonsName;
		int currentHP = pokemonScript.curHP;
		int currentMaxHP = pokemonScript.curMaxHP;
		avatar.sprite = pokemonScript.avatar;
		hitPoints.text = "HP : " + currentHP + " / " + currentMaxHP;
		int currentPP = pokemonScript.curPP;
		int currentMaxPP = pokemonScript.curMaxPP;
		powerPoints.text = "PP : " + currentPP + " / " + currentMaxPP;
		int lastEXP = pokemonScript.lastRequiredEXP;
		int currentEXP = pokemonScript.currentEXP;
		int requiredEXP = pokemonScript.nextRequiredEXP;
		experiencePoints.text = "EXP : " + currentEXP + " / " + requiredEXP;
		
		float hpFillAmount = ((float)currentHP / (float)currentMaxHP);
		hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, hpFillAmount, Time.deltaTime * 5.0f);
		if(currentHP > currentMaxHP / 2)
		{ 
			hpBar.color = new Color32((byte)CalculateValue(currentHP, currentMaxHP / 2, currentMaxHP, 255, 0), 255, 0, 255);
		}else
		{ 
			hpBar.color = new Color32(255, (byte)CalculateValue(currentHP, 0, currentMaxHP / 2, 0, 255), 0 , 255);
		}
		
		float ppFillAmount = ((float)currentPP / (float)currentMaxPP);
		ppBar.fillAmount = Mathf.Lerp(ppBar.fillAmount, ppFillAmount, Time.deltaTime * 5.0f);
		float expFillAmount = (((float)currentEXP - (float)lastEXP) / ((float)requiredEXP - (float)lastEXP));
		expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, expFillAmount, Time.deltaTime * 5.0f);
		for(int i = 0; i < pokemonScript.KnownMoves.Count; i++)
		{
			if(!moves[i].activeInHierarchy)
			{
				moves[i].GetComponent<MoveIcon>().SetupIcon(pokemonScript.KnownMoves[i]);
				moves[i].SetActive(true);
			}
		}
	}
	public void SpawnStatusEffectIcon(StatusEffect effect)
	{
		GameObject effectIcon = Instantiate(statusEffectPrefab) as GameObject;
		effectIcon.transform.SetParent(statusEffectPanel.transform);
		effectIcon.GetComponent<StatusEffectIcon>().SetupIcon(BuffDebuffSprites[(int)(effect.buffOrDebuff - 1)], effect.duration);
	}

	private float CalculateValue(float curValue, float minValue, float maxValue, float minXPos, float maxXPos)
	{
		return (curValue - minValue) * (maxXPos - minXPos) / (maxValue - minValue) + minXPos;
	}
}
