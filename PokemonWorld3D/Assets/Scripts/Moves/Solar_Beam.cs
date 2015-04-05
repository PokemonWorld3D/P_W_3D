using UnityEngine;
using System.Collections;

public class Solar_Beam : Move
{
	public Transform instantiatePoint;
	public Vector3 scale;
	public float scaleSpeed;
	
	public void StartSolarBeam()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject beam = PhotonNetwork.Instantiate("Solar_Beam", instantiatePoint.position, instantiatePoint.rotation, 0) as GameObject;
			beam.GetComponent<SolarBeamEffect>().scale = scale;
			beam.GetComponent<SolarBeamEffect>().scaleSpeed = scaleSpeed;
			Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - beam.transform.position;
			beam.GetComponent<SolarBeamEffect>().target = target;
			beam.rigidbody.AddForce(targetPos * 50.0f);
		}
	}
	public void FinishSolarBeam()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}