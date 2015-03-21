using UnityEngine;
using System.Collections;

public class _CalculateCapture
{
	private bool captured;
	private int catchRate;
	private int pokemonCurHP;
	private int pokemonMaxHP;
	private PokeBall.PokeBallTypes pokeBallType;
	private float ballBonus;
	private float statusBonus;
	private int modifiedCatchRate;
	
	public bool AttemptCapture(_Pokemon.StatusConditions statusCondition, PokeBall.PokeBallTypes pokeBallType, int pokemonCurHP, int pokemonMaxHP, int catchRate)
	{
		if(pokeBallType == PokeBall.PokeBallTypes.POKEBALL)
		{
			ballBonus = 1f;
		}
		else if(pokeBallType == PokeBall.PokeBallTypes.GREATBALL)
		{
			ballBonus = 1.5f;
		}
		else if(pokeBallType == PokeBall.PokeBallTypes.ULTRABALL)
		{
			ballBonus = 2f;
		}
		else if(pokeBallType == PokeBall.PokeBallTypes.MASTERBALL)
		{
			ballBonus = 255f;
		}
		if(statusCondition == _Pokemon.StatusConditions.SLEEPING)
		{
			statusBonus = 2f;
		}
		else if(statusCondition == _Pokemon.StatusConditions.BURNED)
		{
			statusBonus = 1.5f;
		}
		else if(statusCondition == _Pokemon.StatusConditions.FROZEN)
		{
			statusBonus = 2f;
		}
		else if(statusCondition == _Pokemon.StatusConditions.PARALYZED)
		{
			statusBonus = 1.5f;
		}
		else if(statusCondition == _Pokemon.StatusConditions.POISONED)
		{
			statusBonus = 1.5f;
		}
		else
		{
			statusBonus = 1f;
		}
		modifiedCatchRate = (int)(((3 * pokemonMaxHP - 2 * pokemonCurHP) * catchRate * ballBonus) / (3 * pokemonMaxHP) * statusBonus);
		int i = Random.Range(0, 255);
		if(i <= modifiedCatchRate)
		{
			return true;
		}else{
			return false;
		}
	}
}
