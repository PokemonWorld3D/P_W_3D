using UnityEngine;
using System.Collections;

public class Sleep_Powder : Move
{
	public ParticleSystem powder;
	
	public void StartSleepPowder()
	{
		powder.Play();
	}
	public void FinishSleepPowder()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}