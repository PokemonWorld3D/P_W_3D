using UnityEngine;
using System.Collections;

public class Smokescreen : Move
{
	public Transform instantiatePoint;

	public void FinishSmokescreen()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	public IEnumerator SmokescreenEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject smokescreen = PhotonNetwork.Instantiate("Smokescreen", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			yield return new WaitForSeconds(2.0f);
			MoveResults();
			yield return new WaitForSeconds(15.0f);
			PhotonNetwork.Destroy(smokescreen);
		}
	}
}
