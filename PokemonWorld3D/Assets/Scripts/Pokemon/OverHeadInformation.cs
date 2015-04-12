using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverHeadInformation : MonoBehaviour
{
	public Text textInfo;
	public Image hpBar;
	public Pokemon thisPokemon;

	void Start()
	{
		if(thisPokemon.isCaptured && thisPokemon.nickName != "")
		{
			textInfo.text = "Lv. " + thisPokemon.level + " " + thisPokemon.nickName + "\n" + "<<<" + thisPokemon.trainersName + ">>>";
		}
		if(thisPokemon.isCaptured && thisPokemon.nickName == "")
		{
			textInfo.text = "Lv. " + thisPokemon.level + " " + thisPokemon.pokemonName + "\n" + "<<<" + thisPokemon.trainersName + ">>>";
		}
		if(!thisPokemon.isCaptured)
		{
			textInfo.text = "Lv. " + thisPokemon.level + " " + thisPokemon.pokemonName;
		}
		hpBar.fillAmount = (float)thisPokemon.curHP / (float)thisPokemon.curMaxHP;
	}

	public void AdjustHP(float currentHP, float currentMaxHP)
	{
		hpBar.fillAmount = currentHP / currentMaxHP;
	}
}
