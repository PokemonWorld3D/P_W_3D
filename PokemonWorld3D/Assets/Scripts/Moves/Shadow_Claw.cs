using UnityEngine;
using System.Collections;

public class Shadow_Claw : Move
{
	public void FinishShadowClaw()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}	
}
