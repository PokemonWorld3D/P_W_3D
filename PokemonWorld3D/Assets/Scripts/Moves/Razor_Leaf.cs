using UnityEngine;
using System.Collections;

public class Razor_Leaf : Move
{
	public Transform instantiatePointOne;
	public Transform instantiatePointTwo;
	
	public void StartRazorLeafLeft()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject leaf = PhotonNetwork.Instantiate("Razor_Leaf", instantiatePointOne.position, Quaternion.identity, 0) as GameObject;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - leaf.transform.position;
			leaf.GetComponent<RazorLeafEffect>().target = target;
			leaf.rigidbody.AddForce(targetPos * 75.0f);
		}
	}
	public void StartRazorLeafRight()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject leaf = PhotonNetwork.Instantiate("Razor_Leaf", instantiatePointTwo.position, Quaternion.identity, 0) as GameObject;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - leaf.transform.position;
			leaf.GetComponent<RazorLeafEffect>().target = target;
			leaf.rigidbody.AddForce(targetPos * 75.0f);
		}
	}
	public void FinishRazorLeaf()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}