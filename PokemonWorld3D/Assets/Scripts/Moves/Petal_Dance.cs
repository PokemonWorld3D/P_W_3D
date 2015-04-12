using UnityEngine;
using System.Collections;

public class Petal_Dance : Move
{
	public GameObject petals;
	
	public void StartPetalDance()
	{
		petals.SetActive(true);
	}
	public void FinishPetalDance()
	{
		MoveResults();
		petals.SetActive(false);
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}