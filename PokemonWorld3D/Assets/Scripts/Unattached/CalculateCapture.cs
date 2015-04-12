using UnityEngine;
using System.Collections;

public class CalculateCapture
{
	private bool captured;
	private _PokeBall.PokeBallTypes pokeBallType;
	private float ballBonus;
	private float statusBonus;
	private int modifiedCatchRate;
	
	public bool AttemptCapture(Pokemon pokemon, _PokeBall.PokeBallTypes pokeBallType)
	{
		if(pokeBallType == _PokeBall.PokeBallTypes.POKEBALL)
		{
			ballBonus = 1f;
		}
		else if(pokeBallType == _PokeBall.PokeBallTypes.GREATBALL)
		{
			ballBonus = 1.5f;
		}
		else if(pokeBallType == _PokeBall.PokeBallTypes.ULTRABALL)
		{
			ballBonus = 2f;
		}
		else if(pokeBallType == _PokeBall.PokeBallTypes.MASTERBALL)
		{
			ballBonus = 255f;
		}
		statusBonus = StatusBonus(pokemon);
		modifiedCatchRate = (int)(((3 * pokemon.maxHP - 2 * pokemon.curHP) * pokemon.captureRate * ballBonus) / (3 * pokemon.maxHP) * statusBonus);
		int i = Random.Range(0, 255);
		if(i <= modifiedCatchRate)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	private float StatusBonus(Pokemon pokemon)
	{
		if(pokemon.sleeping)
		{
			return 2f;
		}
		else if(pokemon.burned)
		{
			return 1.5f;
		}
		else if(pokemon.frozen)
		{
			return 2f;
		}
		else if(pokemon.paralyzed)
		{
			return 1.5f;
		}
		else if(pokemon.poisoned)
		{
			return 1.5f;
		}
		else
		{
			return 1f;
		}
	}
}
