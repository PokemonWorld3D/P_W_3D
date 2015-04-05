using UnityEngine;
using System.Collections;

public class Rapid_Spin : Move
{
	public TrailRenderer rapidSpinOne;
	public TrailRenderer rapidSpinTwo;
	public TrailRenderer rapidSpinThree;
	public TrailRenderer rapidSpinFour;
	
	public void StartRapidSpin()
	{
		rapidSpinOne.enabled = true;
		rapidSpinTwo.enabled = true;
		rapidSpinThree.enabled = true;
		rapidSpinFour.enabled = true;
		rigidbody.velocity = Vector3.zero;
		float distance = Vector3.Distance(transform.position, target.transform.position);
		rigidbody.AddRelativeForce(0, 0, distance * 10000);
	}
	public void FinishRapidSpin()
	{
		MoveResults();
		rapidSpinOne.enabled = false;
		rapidSpinTwo.enabled = false;
		rapidSpinThree.enabled = false;
		rapidSpinFour.enabled = false;
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}