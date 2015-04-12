using UnityEngine;
using System.Collections;

public class Ember : Move
{
	public Transform instantiatePoint;

	public void StartEmber()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject embers = PhotonNetwork.Instantiate("Ember", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - embers.transform.position;
			embers.GetComponent<EmberEffect>().target = target;
			embers.rigidbody.AddForce(targetPos * 50.0f);
		}
	}
	public void FinishEmber()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
