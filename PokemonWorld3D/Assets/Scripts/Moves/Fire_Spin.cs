using UnityEngine;
using System.Collections;

public class Fire_Spin : Move
{
	public ParticleSystem fireSpin;
	
	public void StartFireSpin()
	{
		fireSpin.Play();
	}
	public void FinishFireSpin()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
