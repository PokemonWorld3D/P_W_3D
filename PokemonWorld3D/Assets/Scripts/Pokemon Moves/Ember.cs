using UnityEngine;
using System.Collections;

public class Ember : Move
{
	public GameObject embers;
	public GameObject prefab;
	public Transform instantiate_point;
	
	public void EmberDamage()
	{
		if(hit)
		{
			target_pokemon.AdjustCurrentHP(-damage);
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
	private IEnumerator EmberEffect()
	{
		embers = Instantiate(prefab, instantiate_point.position, instantiate_point.rotation) as GameObject;
		Vector3 target_pos = target.transform.position - embers.transform.position;
		target_pos.Normalize();
		embers.rigidbody.AddForce(target_pos * 300.0f);
		while(Vector3.Distance(embers.transform.position, target.transform.position) > 0.1f)
		{
			Debug.Log (Vector3.Distance(embers.transform.position, target.transform.position));
			yield return null;
		}
		//-------------Instantiate the explosion here.---------------------------------//
		Destroy(embers);
		EmberDamage();
	}
}
