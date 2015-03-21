using UnityEngine;
using System.Collections;

[System.Serializable]
public class _StatusEffect
{
	public string statusEffectName;
	public bool changeStat;
	public _Pokemon.Stats statToChange;
	public bool changeAccOrEva;
	public _Pokemon.AccEva accOrEva;
	public int stagestoChange;
	public bool changeAbility;
	public string changeAbilityTo;
	public float successRate;

	public _StatusEffect()
	{
		
	}
	public _StatusEffect(string name, bool stat_change, _Pokemon.Stats stat, bool acceva_change, _Pokemon.AccEva acceva, int stages, bool change_ability,
	                    string new_ability, float rate_of_success)
	{
		statusEffectName = name;
		changeStat = stat_change;
		statToChange = stat;
		changeAccOrEva = acceva_change;
		accOrEva = acceva;
		stagestoChange = stages;
		changeAbility = change_ability;
		changeAbilityTo = new_ability;
		successRate = rate_of_success;
	}
}
