using UnityEngine;
using System.Collections;

public class Inferno : Move
{
	public Transform instantiatePoint;
	
	public void FinishInferno()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	public IEnumerator InfernoEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject inferno = PhotonNetwork.Instantiate("Inferno", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			yield return new WaitForSeconds(2.0f);
			MoveResults();
			yield return new WaitForSeconds(15.0f);
			PhotonNetwork.Destroy(inferno);
		}
	}
}
