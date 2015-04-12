using UnityEngine;
using System.Collections;

public class Bug_Bite : Move
{
	public void FinishBugBite()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
