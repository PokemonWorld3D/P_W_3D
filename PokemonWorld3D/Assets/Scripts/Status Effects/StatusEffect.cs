using UnityEngine;
using System.Collections;

[System.Serializable]
public class StatusEffect
{
	public string status_effect_name;
	public bool change_stat;
	public Pokemon.Stats stat_to_change;
	public bool change_acc_or_eva;
	public Pokemon.AccEva acc_or_eva;
	public int stages_to_change;
	public bool ability_change;
	public string change_ability_to;
	public float success_rate;

	public StatusEffect()
	{
		
	}
	public StatusEffect(string name, bool stat_change, Pokemon.Stats stat, bool acceva_change, Pokemon.AccEva acceva, int stages, bool change_ability,
	                    string new_ability, float rate_of_success)
	{
		status_effect_name = name;
		change_stat = stat_change;
		stat_to_change = stat;
		change_acc_or_eva = acceva_change;
		acc_or_eva = acceva;
		stages_to_change = stages;
		ability_change = change_ability;
		change_ability_to = new_ability;
		success_rate = rate_of_success;
	}
}
