using UnityEngine;
using System.Collections;

[System.Serializable]
public class StatusEffect
{
	public Pokemon.BuffsAndDebuffs buffOrDebuff;
	public float percentage;
	public float duration;

	public StatusEffect(Pokemon.BuffsAndDebuffs _buffOrDebuff, float _percentage, float _duration)
	{
		buffOrDebuff = _buffOrDebuff;
		percentage = _percentage;
		duration = _duration;
	}
}

//public string statusEffectName;
//public bool changeStat;
//public Pokemon.Stats statToChange;
//public bool changeAccOrEva;
//public Pokemon.AccEva accOrEva;
//public int stagestoChange;
//public bool changeAbility;
//public string changeAbilityTo;
//public float successRate;
//
//public StatusEffect()
//{
//	
//}
//public StatusEffect(string name, bool stat_change, Pokemon.Stats stat, bool acceva_change, Pokemon.AccEva acceva, int stages, bool change_ability,
//                    string new_ability, float rate_of_success)
//{
//	statusEffectName = name;
//	changeStat = stat_change;
//	statToChange = stat;
//	changeAccOrEva = acceva_change;
//	accOrEva = acceva;
//	stagestoChange = stages;
//	changeAbility = change_ability;
//	changeAbilityTo = new_ability;
//	successRate = rate_of_success;
//}
