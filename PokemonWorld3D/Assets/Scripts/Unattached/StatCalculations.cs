using UnityEngine;
using System.Collections;

public class StatCalculations
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
	
	public int CalculateStat(int baseStat, int level, int iv, int ev, Pokemon.Natures nature, StatTypes statType)
	{
		SetModifier(nature, statType);
		return (int)(((((iv + (2 * baseStat) + (ev / 4)) * level) / 100) + 5) * statModifier);
	}

	private void SetModifier(Pokemon.Natures nature, StatTypes statType)
	{
		if(nature == Pokemon.Natures.LONELY && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.LONELY && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.BRAVE && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.BRAVE && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.ADAMANT && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.ADAMANT && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.NAUGHTY && statType == StatTypes.ATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.NAUGHTY && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.BOLD && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.BOLD && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.RELAXED && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.RELAXED && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.IMPISH && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.IMPISH && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.LAX && statType == StatTypes.DEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.LAX && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.TIMID && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.TIMID && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.HASTY && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.HASTY && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.JOLLY && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.JOLLY && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.NAIVE && statType == StatTypes.SPEED)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.NAIVE && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.MODEST && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.MODEST && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.MILD && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.MILD && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.QUIET && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.QUIET && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.RASH && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.RASH && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.CALM && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.CALM && statType == StatTypes.ATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.GENTLE && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.GENTLE && statType == StatTypes.DEFENSE)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.SASSY && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.SASSY && statType == StatTypes.SPEED)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.CAREFUL && statType == StatTypes.SPECIALDEFENSE)
		{
			statModifier = natureIncreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.CAREFUL && statType == StatTypes.SPECIALATTACK)
		{
			statModifier = natureDecreaseModifier;
		}
		else
		{
			statModifier = natureNeutralModifier;
		}
		if(nature == Pokemon.Natures.BASHFUL || nature == Pokemon.Natures.DOCILE || nature == Pokemon.Natures.HARDY || 
		   nature == Pokemon.Natures.QUIRKY || nature == Pokemon.Natures.SERIOUS)
		{
			statModifier = natureNeutralModifier;
		}
	}
	
}
