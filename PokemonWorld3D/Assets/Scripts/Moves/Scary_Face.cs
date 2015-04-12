using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scary_Face : Move
{
	public GameObject scaryFace;
	public Image face;

	public void StartScaryFace()
	{
		Color32 newColor = new Color(255, 255, 255, 255);
		face.color = newColor;
		scaryFace.SetActive(true);
	}
	public void FinishScaryFace()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
