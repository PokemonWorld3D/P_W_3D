using UnityEngine;
using System.Collections;

public class Scary_Face : Move
{
	public void FinishScaryFace()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
