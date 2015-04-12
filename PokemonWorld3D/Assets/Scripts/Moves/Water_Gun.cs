using UnityEngine;
using System.Collections;

public class Water_Gun : Move
{
	public ParticleSystem waterGun;
	
	public void StartWaterGun()
	{
		waterGun.Play();
	}
	public void FinishWaterGun()
	{
		waterGun.Stop();
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}