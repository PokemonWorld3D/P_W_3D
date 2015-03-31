using UnityEngine;
using System.Collections;

public class Tackle : Move
{
	public TrailRenderer tackle;

	public void StartTackleEffect()
	{
		tackle.enabled = true;
		rigidbody.AddForce((target.transform.position - transform.position) * 5.0f * Time.smoothDeltaTime);
	}
	public void FinishTackle()
	{
		MoveResults();
		tackle.enabled = false;
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
