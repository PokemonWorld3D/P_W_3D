using UnityEngine;
using System.Collections;

public class Slash : Move
{
	public TrailRenderer claws;
	
	public void StartSlash()
	{
		claws.enabled = true;
	}
	public void FinishSlash()
	{
		MoveResults();
		claws.enabled = false;
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}