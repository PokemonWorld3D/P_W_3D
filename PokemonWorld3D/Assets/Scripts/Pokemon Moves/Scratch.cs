using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Scratch : Move
{
	public TrailRenderer claws;

	public void ScratchDamage()
	{
		if(hit = true){
			target.GetComponent<Pokemon>().AdjustCurrentHP(-damage);
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
				float chance_to_apply = Random.Range(0.0f, 1.0f);
				if(effect.success_rate >= chance_to_apply)
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
	public void StartScratchEffect()
	{
		claws.enabled = true;
	}
	public void EndScratchEffect()
	{
		claws.enabled = false;
	}
}
