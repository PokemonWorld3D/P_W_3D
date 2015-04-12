using UnityEngine;
using System.Collections;

public class Flare_Blitz : Move
{
	public ParticleSystem flareBlitz;

	public void StartFlareBlitz()
	{
		flareBlitz.Play();
	}
	public IEnumerator FinishFlareBlitz()
	{
		rigidbody.velocity = Vector3.zero;
		Vector3 position = target.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position);;
		position.y = target.transform.position.y;
		while(Vector3.Distance(transform.position, position) > 0.5f)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 10);
			yield return null;
		}
		GetComponent<Animator>().SetBool(moveName, false);
		MoveResults();
		flareBlitz.Stop();
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
