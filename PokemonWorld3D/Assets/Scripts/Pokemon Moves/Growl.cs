using UnityEngine;
using System.Collections;

public class Growl : Move
{
	public void GrowlDamage()
	{
		foreach(GameObject pokemon in these_targets)
		{
			chance_to_hit = accuracy * (acc / target_eva);
			float hit_or_miss = Random.Range(0.0f, 1.0f);
			if(chance_to_hit >= hit_or_miss)
			{
				hit = true;
			}
			if(hit)
			{
				if(status_condition != Pokemon.StatusConditions.NONE)
				{
					float status_chance = Random.Range(0.0f, 1.0f);
					if(status_condition_success_rate > status_chance)
					{
						target_pokemon.status_condition = status_condition;
					}
				}
				foreach(StatusEffect effect in status_effects)
				{
					if(effect.change_stat)
					{
						target_pokemon.AdjustCurrentStat(effect.stat_to_change, effect.stages_to_change);
					}
					if(effect.change_acc_or_eva)
					{
						target_pokemon.AdjustCurrentAccEva(effect.acc_or_eva, effect.stages_to_change);
					}
				}
			}
		}
	}
	
	public IEnumerator GrowlEffect()
	{
		yield return new WaitForSeconds(2.0f);
		GrowlDamage();
	}
}
