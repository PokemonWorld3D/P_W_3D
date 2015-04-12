using UnityEngine;
using System.Collections;

public class Dragon_Rage : Move
{
	public Transform instantiatePoint;
	public float scale;
	public float scaleSpeed;

	public void StartDragonRage()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject rage = PhotonNetwork.Instantiate("Dragon_Rage", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			rage.GetComponent<DragonRageEffect>().scale = scale;
			rage.GetComponent<DragonRageEffect>().scaleSpeed = scaleSpeed;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - rage.transform.position;
			rage.GetComponent<DragonRageEffect>().target = target;
			rage.rigidbody.AddForce(targetPos * 50.0f);
		}
	}
	public void FinishDragonRage()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
