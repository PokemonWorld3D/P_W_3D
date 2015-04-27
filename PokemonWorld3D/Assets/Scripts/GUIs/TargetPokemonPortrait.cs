using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetPokemonPortrait : MonoBehaviour
{
	public GameObject targetPokemon;
	public Pokemon targetPokemonScript;
	public Text targetInfo;
	public Image targetHPBar;
	public Image targetAvatar;

	void Update()
	{
		HandleTargetGUI();
		if(targetPokemon == null || targetPokemon.Equals(null))
			RemoveTargetPokemon();
	}

	public void SetTargetPokemon(GameObject pokemon)
	{
		targetPokemon = pokemon;
		targetPokemonScript = pokemon.GetComponent<Pokemon>();
		gameObject.SetActive(true);
		this.enabled = true;
	}
	public void RemoveTargetPokemon()
	{
		targetPokemon = null;
		targetPokemonScript = null;
		this.enabled = false;
	}
	public void HandleTargetGUI()
	{
		string pokemonsName = targetPokemonScript.pokemonName;
		if(targetPokemonScript.nickName != "")
		{
			pokemonsName = targetPokemonScript.nickName;
		}
		targetInfo.text = "Level " + targetPokemonScript.level + " " + pokemonsName;
		int current_hp = targetPokemonScript.curHP;
		int current_max_hp = targetPokemonScript.curMaxHP;
		targetAvatar.sprite = targetPokemonScript.avatar;
		
		float hpfillAmount = ((float)current_hp / (float)current_max_hp);
		targetHPBar.fillAmount = Mathf.Lerp(targetHPBar.fillAmount, hpfillAmount, Time.deltaTime * 5.0f);
		if(current_hp > current_max_hp / 2)
		{
			targetHPBar.color = new Color32((byte)CalculateValue(current_hp, current_max_hp / 2, current_max_hp, 255, 0), 255, 0, 255);
		}
		else
		{
			targetHPBar.color = new Color32(255, (byte)CalculateValue(current_hp, 0, current_max_hp / 2, 0, 255), 0 , 255);
		}
	}
	private float CalculateValue(float curValue, float minValue, float maxValue, float minXPos, float maxXPos)
	{
		return (curValue - minValue) * (maxXPos - minXPos) / (maxValue - minValue) + minXPos;
	}
}
