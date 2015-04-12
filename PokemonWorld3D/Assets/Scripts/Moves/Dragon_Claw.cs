using UnityEngine;
using System.Collections;

public class Dragon_Claw : Move
{
	public void FinishDragonClaw()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}	
}