using UnityEngine;
using System.Collections;

public class Worry_Seed : Move
{
	public Transform instantiatePoint;
	public GameObject seed;
	public Vector3 scale;
	public float scaleSpeed;
	
	public void StartWorrySeed()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			seed = PhotonNetwork.Instantiate("Seed", instantiatePoint.position, Quaternion.identity, 0) as GameObject;
			seed.transform.parent = instantiatePoint;
			seed.GetComponent<SeedEffect>().scale = scale;
			seed.GetComponent<SeedEffect>().scaleSpeed = scaleSpeed;
		}
	}
	public void FinishWorrySeed()
	{
		Vector3 targetPos = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
		Vector3 throwSpeed = calculateBestThrowSpeed(seed.transform.position, targetPos, 1.0f);
		seed.transform.parent = null;
		seed.rigidbody.useGravity = true;
		seed.rigidbody.AddForce(throwSpeed, ForceMode.VelocityChange);
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
	
	private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget)
	{
		// calculate vectors
		Vector3 toTarget = target - origin;
		Vector3 toTargetXZ = toTarget;
		toTargetXZ.y = 0;
		
		// calculate xz and y
		float y = toTarget.y;
		float xz = toTargetXZ.magnitude;
		
		// calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
		// where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
		// so xz = v0xz * t => v0xz = xz / t
		// and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
		float t = timeToTarget;
		float v0y = y / t + 0.5f * Physics.gravity.magnitude * t;
		float v0xz = xz / t;
		
		// create result vector for calculated starting speeds
		Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
		result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
		result.y = v0y;                                // set y to v0y (starting speed of y plane)
		
		return result;
	}
}