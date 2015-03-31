using UnityEngine;
using System.Collections;

public class Fire_Fang : Move
{
	public GameObject fireFangs;

	public void StartFireFang()
	{
		fireFangs.SetActive(true);
	}
	public void FinishFireFang()
	{
		MoveResults();
		fireFangs.SetActive(false);
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
