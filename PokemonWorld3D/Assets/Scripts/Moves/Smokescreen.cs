using UnityEngine;
using System.Collections;

public class Smokescreen : Move
{
	public ParticleSystem smoke;
	
	public void StartSmokescreen()
	{
		smoke.Play();
	}
	public void FinishSmokescreen()
	{
		smoke.Stop();
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
