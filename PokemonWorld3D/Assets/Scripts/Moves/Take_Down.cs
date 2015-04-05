using UnityEngine;
using System.Collections;

public class Take_Down : Move
{
	public TrailRenderer takeDownOne;
	public TrailRenderer takeDownTwo;
	public TrailRenderer takeDownThree;
	public TrailRenderer takeDownFour;
	
	public IEnumerator StartTakeDown()
	{
		takeDownOne.enabled = true;
		takeDownTwo.enabled = true;
		takeDownThree.enabled = true;
		takeDownFour.enabled = true;
		rigidbody.velocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;
		Vector3 position = target.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position);;
		position.y = target.transform.position.y;
		while(Vector3.Distance(transform.position, position) > 0.5f)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
			yield return null;
		}
	}
	public void FinishTakeDown()
	{
		MoveResults();
		takeDownOne.enabled = false;
		takeDownTwo.enabled = false;
		takeDownThree.enabled = false;
		takeDownFour.enabled = false;
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}