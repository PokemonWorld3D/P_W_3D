using UnityEngine;
using System.Collections;

public class Growth : Move
{
	public Vector3 scale;
	public float scaleSpeed;

	private float timer = 0.0f;

	public IEnumerator StartGrowth()
	{
		float current = transform.localScale.sqrMagnitude;
		float target = scale.sqrMagnitude;
		while(current < target)
		{
			timer += Time.deltaTime;
			transform.localScale = Vector3.Lerp(transform.localScale, scale, timer / scaleSpeed);
			yield return null;
		}
	}
	public void FinishGrowth()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}