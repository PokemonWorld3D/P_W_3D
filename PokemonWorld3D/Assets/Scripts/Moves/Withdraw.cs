using UnityEngine;
using System.Collections;

public class Withdraw : Move
{
	public IEnumerator StartWithdraw()
	{
		MoveResults();
		yield return new WaitForSeconds(5);
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}