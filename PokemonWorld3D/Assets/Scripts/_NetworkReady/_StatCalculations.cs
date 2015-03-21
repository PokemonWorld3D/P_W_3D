using UnityEngine;
using System.Collections;

public class _StatCalculations
{
	private float natureIncreaseModifier = 1.10f;
	private float natureDecreaseModifier = 0.10f;
	private float natureNeutralModifier = 1.00f;
	private float statModifier;

	public enum StatTypes { HITPOINTS, ATTACK, DEFENSE, SPECIALATTACK, SPECIALDEFENSE, SPEED }
	

	public int CalculateHP(int baseHP, int level, int iv, int ev)
	{
		return (int)((((iv + (2 * baseHP) + (ev / 4) + 100) * level) / 100) + 10);
	}

	public int CalculatePP(int basePP, int level)
	{
		return (int)((((2 * basePP) + 100) * level) / 100);
	}
	
	public int CalculateStat(int baseStat, int level, int iv, int ev, _Pokemon.Natures nature, StatTypes statType)
	{
		SetModifier(nature, statType);
		return (int)(((((iv + (2 * baseStat) + (ev / 4)) * level) / 100) + 5) * statModifier);
	}

	private void SetModifier(_Pokemon.Natures nature, StatTypes statType)
	{
		if(nature == _Pokemon.Natures.LONELY && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.LONELY && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.BRAVE && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.BRAVE && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.ADAMANT && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.ADAMANT && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.NAUGHTY && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.NAUGHTY && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.BOLD && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.BOLD && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.RELAXED && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.RELAXED && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.IMPISH && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.IMPISH && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.LAX && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.LAX && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.TIMID && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.TIMID && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.HASTY && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.HASTY && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.JOLLY && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.JOLLY && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.NAIVE && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.NAIVE && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.MODEST && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.MODEST && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.MILD && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.MILD && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.QUIET && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.QUIET && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.RASH && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.RASH && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.CALM && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.CALM && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.GENTLE && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.GENTLE && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.SASSY && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.SASSY && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.CAREFUL && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.CAREFUL && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == _Pokemon.Natures.BASHFUL || nature == _Pokemon.Natures.DOCILE || nature == _Pokemon.Natures.HARDY || 
		   nature == _Pokemon.Natures.QUIRKY || nature == _Pokemon.Natures.SERIOUS)
		{
			statModifier = natureNeutralModifier;
		}
	}
	
}
