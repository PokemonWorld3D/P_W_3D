using UnityEngine;
using System.Collections;

public class Take_Down : Move
{
	public IEnumerator StartTakeDown()
	{
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
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}