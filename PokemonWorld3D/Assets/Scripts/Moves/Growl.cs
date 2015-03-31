using UnityEngine;
using System.Collections;

public class Growl : Move
{
	public void FinishGrowl()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
