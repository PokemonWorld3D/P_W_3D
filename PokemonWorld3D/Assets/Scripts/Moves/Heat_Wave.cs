using UnityEngine;
using System.Collections;

public class Heat_Wave : Move
{
	public ParticleSystem heatWave;
	
	public void StartHeatWave()
	{
		heatWave.Play();
	}
	public void FinishHeatWave()
	{
		heatWave.Stop();
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}