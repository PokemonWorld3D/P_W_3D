using UnityEngine;
using System.Collections;

public class Bubble : Move
{
	public ParticleSystem bubble;
	
	public void StartBubble()
	{
		bubble.Play();
	}
	public void FinishBubble()
	{
		MoveResults();
		GetComponent<Animator>().SetBool(moveName, false);
		GetComponent<PokemonInput>().NotAttacking();
		GetComponent<WildPokemonAI>().usingMove = false;
	}
}