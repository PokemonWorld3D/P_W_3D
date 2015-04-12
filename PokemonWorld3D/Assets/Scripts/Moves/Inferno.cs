using UnityEngine;
using System.Collections;

public class Inferno : Move
{
	public ParticleSystem inferno;
	
	public IEnumerator StartInferno()
	{
		inferno.Play();
		yield return new WaitForSeconds(1.5f);
		PhotonNetwork.Instantiate("Inferno", target.transform.position, target.transform.rotation, 0);
	}
	public void FinishInferno()
	{
		inferno.Stop();
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}
