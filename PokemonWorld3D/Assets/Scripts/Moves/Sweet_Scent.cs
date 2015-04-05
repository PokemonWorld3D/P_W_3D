using UnityEngine;
using System.Collections;

public class Sweet_Scent : Move
{
	public ParticleSystem scent;
	
	public void StartSweetScent()
	{
		scent.Play();
	}
	public void FinishSweetScent()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}