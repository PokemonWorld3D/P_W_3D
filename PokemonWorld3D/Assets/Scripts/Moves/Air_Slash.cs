using UnityEngine;
using System.Collections;

public class Air_Slash : Move
{
	public Transform instantiatePointOne;
	public Transform instantiatePointTwo;
	public float jumpPower;
	public float gravity;
	
	public void StartAirSlash()
	{
		Vector3 velocity = rigidbody.velocity;
		Vector3 targetVelocity = Vector3.zero;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.y = CalculateJumpVerticalSpeed();
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}
	public void MiddleAirSlash()
	{
		if(GetComponent<PhotonView>().owner == PhotonNetwork.player)
		{
			GameObject slashOne = PhotonNetwork.Instantiate("Air_Slash", instantiatePointOne.position, Quaternion.identity, 0) as GameObject;
			GameObject slashTwo = PhotonNetwork.Instantiate("Air_Slash", instantiatePointOne.position, Quaternion.identity, 0) as GameObject;
			Vector3 targetPosOne = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - slashOne.transform.position;
			Vector3 targetPosTwo = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center - slashTwo.transform.position;
			slashOne.GetComponent<AirSlashEffect>().target = target;
			slashTwo.GetComponent<AirSlashEffect>().target = target;
			slashOne.rigidbody.AddForce(targetPosOne * 75.0f);
			slashTwo.rigidbody.AddForce(targetPosTwo * 75.0f);
		}
	}
	public void FinishAirSlash()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}

	private float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2 * jumpPower * gravity);
	}
}