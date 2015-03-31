using UnityEngine;
using System.Collections;

public class Flame_Burst : Move
{
	public Transform instantiatePoint;
	
	public void FinishFlameBurst()
	{
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	private IEnumerator FlameBurstEffect()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject burst = PhotonNetwork.Instantiate("Flame_Burst", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			Vector3 target_pos = target.transform.position - burst.transform.position;
			target_pos.Normalize();
			burst.rigidbody.AddForce(target_pos * 300.0f);
			while(Vector3.Distance(burst.transform.position, target.transform.position) > 0.1f)
			{
				yield return null;
			}
			//-------------Instantiate the explosion here.---------------------------------//
			PhotonNetwork.Destroy(burst);
		}
		MoveResults();
	}
}
