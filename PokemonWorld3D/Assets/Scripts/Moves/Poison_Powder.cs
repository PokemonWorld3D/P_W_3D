using UnityEngine;
using System.Collections;

public class Poison_Powder : Move
{
	public ParticleSystem powder;
	
	public void StartPoisonPowder()
	{
		powder.Play();
	}
	public void FinishPoisonPowder()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}