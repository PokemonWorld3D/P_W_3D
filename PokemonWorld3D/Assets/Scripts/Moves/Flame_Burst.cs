using UnityEngine;
using System.Collections;

public class Flame_Burst : Move
{
	public Transform instantiatePoint;

	public void StartFlameBurst()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject burst = PhotonNetwork.Instantiate("Flame_Burst", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - burst.transform.position;
			burst.GetComponent<FlameBurstEffect>().target = target;
			burst.rigidbody.AddForce(targetPos * 50.0f);
		}
	}
	public void FinishFlameBurst()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
