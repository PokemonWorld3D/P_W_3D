using UnityEngine;
using System.Collections;

public class Petal_Blizzard : Move
{
	public ParticleSystem petals;
	
	public void StartPetalBlizzard()
	{
		petals.Play();
	}
	public void FinishPetalBlizzard()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}