using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scratch : Move
{
	public TrailRenderer claws;
	
	public void StartScratchEffect()
	{
		claws.enabled = true;
	}
	public void FinishScratch()
	{
		MoveResults();
		claws.enabled = false;
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
