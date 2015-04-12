using UnityEngine;
using System.Collections;

public class Bite : Move
{
	public void FinishBite()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}