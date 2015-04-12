using UnityEngine;
using System.Collections;

public class Protect : Move
{
	public GameObject protect;

	public void StartProtect()
	{
		protect.SetActive(true);
	}
	public void FinishProtect()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}