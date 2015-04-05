using UnityEngine;
using System.Collections;

public class Hydro_Pump : Move
{
	public ParticleSystem hydroPumpOne;
	public ParticleSystem hydroPumpTwo;
	
	public void StartHydroPump()
	{
		hydroPumpOne.Play();
		hydroPumpTwo.Play();
	}
	public void FinishHydroPump()
	{
		MoveResults();
		hydroPumpOne.Stop();
		hydroPumpTwo.Stop();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
