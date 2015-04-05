using UnityEngine;
using System.Collections;

public class Flamethrower : Move
{
	public ParticleSystem flamethrower;

	public void StartFlamethrower()
	{
		flamethrower.Play();
	}
	public void FinishFlamethrower()
	{
		flamethrower.Stop();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
	}
}
