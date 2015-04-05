using UnityEngine;
using System.Collections;

public class Vine_Whip : Move
{
	public void FinishVineWhip()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}