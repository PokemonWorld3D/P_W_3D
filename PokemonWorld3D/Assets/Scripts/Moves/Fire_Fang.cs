using UnityEngine;
using System.Collections;

public class Fire_Fang : Move
{
	public ParticleSystem fireFang;

	public void StartFireFang()
	{
		fireFang.Play();
	}
	public void FinishFireFang()
	{
		MoveResults();
		fireFang.Stop();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
