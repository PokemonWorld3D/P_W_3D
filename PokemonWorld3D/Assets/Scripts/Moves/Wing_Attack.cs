using UnityEngine;
using System.Collections;

public class Wing_Attack : Move
{
	public GameObject wingAttackOne;
	public GameObject wingAttackTwo;
	public float jumpPower;
	public float gravity;

	public void StartWingAttack()
	{
		Vector3 velocity = rigidbody.velocity;
		Vector3 targetVelocity = Vector3.zero;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.y = CalculateJumpVerticalSpeed();
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}
	public IEnumerator FinishWingAttack()
	{
		rigidbody.velocity = Vector3.zero;
		Vector3 position = target.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position);;
		position.y = target.transform.position.y;
		wingAttackOne.SetActive(true);
		wingAttackTwo.SetActive(true);
		while(Vector3.Distance(transform.position, position) > 0.5f)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
			yield return null;
		}
		MoveResults();
		wingAttackOne.SetActive(false);
		wingAttackTwo.SetActive(false);
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}

	private float CalculateJumpVerticalSpeed()
	{
		return Mathf.Sqrt(2 * jumpPower * gravity);
	}
}