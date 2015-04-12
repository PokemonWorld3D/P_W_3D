using UnityEngine;
using System.Collections;

public class Skull_Bash : Move
{
	//public GameObject skullBash;

	public void StartSkullBash()
	{
		//skullBash.SetActive(true);
	}
	public IEnumerator FinishSkullBash()
	{
		rigidbody.velocity = Vector3.zero;
		Vector3 position = target.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position);;
		position.y = target.transform.position.y;
		while(Vector3.Distance(transform.position, position) > 0.5f)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
			yield return null;
		}
		MoveResults();
		//skullBash.SetActive(false);
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}