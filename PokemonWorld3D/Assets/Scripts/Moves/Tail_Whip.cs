using UnityEngine;
using System.Collections;

public class Tail_Whip : Move
{
	public void FinishTailWhip()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}