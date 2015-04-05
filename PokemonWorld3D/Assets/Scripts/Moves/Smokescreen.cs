using UnityEngine;
using System.Collections;

public class Smokescreen : Move
{
	public Transform instantiatePoint;

	public void StartSmokescreen()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			PhotonNetwork.Instantiate("Smokescreen", instantiatePoint.position, instantiatePoint.rotation, 0);
		}
	}
	public void FinishSmokescreen()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
