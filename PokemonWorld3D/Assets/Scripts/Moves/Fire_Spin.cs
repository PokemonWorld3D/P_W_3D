using UnityEngine;
using System.Collections;

public class Fire_Spin : Move
{
	public Transform instantiatePoint;
	
	public void FinishFireSpin()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	public IEnumerator FireSpinEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject fireSpin = PhotonNetwork.Instantiate("Fire_Spin", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			yield return new WaitForSeconds(2.0f);
			MoveResults();
			yield return new WaitForSeconds(15.0f);
			PhotonNetwork.Destroy(fireSpin);
		}
	}
}
