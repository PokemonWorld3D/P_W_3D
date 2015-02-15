using UnityEngine;
using System.Collections;

public class StatCalculations {

	private float natureIncreaseModifier = 1.10f;
	private float natureDecreaseModifier = 0.10f;
	private float natureNeutralModifier = 1.00f;
	private float statModifier;

	public enum StatTypes{
		HITPOINTS,
		ATTACK,
		DEFENSE,
		SPECIALATTACK,
		SPECIALDEFENSE,
		SPEED
	}
	

	public int CalculateHP(int baseHP, int level, int iv, int ev){
			return (int)((((iv + (2 * baseHP) + (ev / 4) + 100) * level) / 100) + 10);
	}

	public int CalculatePP(int basePP, int level){
		return (int)((((2 * basePP) + 100) * level) / 100);
	}
	
	public int CalculateStat(int baseStat, int level, int iv, int ev, BasePokemon.NaturesList nature, StatTypes statType){
		SetModifier (nature, statType);
		return (int)(((((iv + (2 * baseStat) + (ev / 4)) * level) / 100) + 5) * statModifier);
	}

	private void SetModifier(BasePokemon.NaturesList nature, StatTypes statType){
		if(nature == BasePokemon.NaturesList.LONELY && statType == StatTypes.ATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.LONELY && statType == StatTypes.DEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.BRAVE && statType == StatTypes.ATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.BRAVE && statType == StatTypes.SPEED){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.ADAMANT && statType == StatTypes.ATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.ADAMANT && statType == StatTypes.SPECIALATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.NAUGHTY && statType == StatTypes.ATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.NAUGHTY && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.BOLD && statType == StatTypes.DEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.BOLD && statType == StatTypes.ATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.RELAXED && statType == StatTypes.DEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.RELAXED && statType == StatTypes.SPEED){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.IMPISH && statType == StatTypes.DEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.IMPISH && statType == StatTypes.SPECIALATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.LAX && statType == StatTypes.DEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.LAX && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.TIMID && statType == StatTypes.SPEED){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.TIMID && statType == StatTypes.ATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.HASTY && statType == StatTypes.SPEED){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.HASTY && statType == StatTypes.DEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.JOLLY && statType == StatTypes.SPEED){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.JOLLY && statType == StatTypes.SPECIALATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.NAIVE && statType == StatTypes.SPEED){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.NAIVE && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.MODEST && statType == StatTypes.SPECIALATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.MODEST && statType == StatTypes.ATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.MILD && statType == StatTypes.SPECIALATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.MILD && statType == StatTypes.DEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.QUIET && statType == StatTypes.SPECIALATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.QUIET && statType == StatTypes.SPEED){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.RASH && statType == StatTypes.SPECIALATTACK){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.RASH && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.CALM && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.CALM && statType == StatTypes.ATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.GENTLE && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.GENTLE && statType == StatTypes.DEFENSE){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.SASSY && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.SASSY && statType == StatTypes.SPEED){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.CAREFUL && statType == StatTypes.SPECIALDEFENSE){
			statModifier = natureIncreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.CAREFUL && statType == StatTypes.SPECIALATTACK){
			statModifier = natureDecreaseModifier;
		}else{
			statModifier = natureNeutralModifier;
		}
		if(nature == BasePokemon.NaturesList.BASHFUL || nature == BasePokemon.NaturesList.DOCILE || nature == BasePokemon.NaturesList.HARDY || 
		   nature == BasePokemon.NaturesList.QUIRKY || nature == BasePokemon.NaturesList.SERIOUS){
			statModifier = natureNeutralModifier;
		}
	}
	
}
