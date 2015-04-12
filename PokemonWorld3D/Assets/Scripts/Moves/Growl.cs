using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Growl : Move
{
	public GameObject growl;
	public Image growlBolts;
	
	public void StartGrowl()
	{
		Color32 newColor = new Color(255, 255, 255, 255);
		growlBolts.color = newColor;
		growl.SetActive(true);
	}
	public void FinishGrowl()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
