using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Move : MonoBehaviour
{
	public Pokemon this_pokemon;
	public int level;
	public int attack;
	public int special_attack;
	public float acc;
	public int base_speed;
	public PokemonTypes.Types type_one;
	public PokemonTypes.Types type_two;
	public int target_defense;
	public int target_special_defense;
	public float target_eva;
	public PokemonTypes.Types target_type_one;
	public PokemonTypes.Types target_type_two;
	public PokemonInput input;
	public bool aoe;
	public bool single_target;
	public string move_name;
	public string description;
	public int level_learned;
	public PokemonTypes.Types type;
	public MoveCategoriesList category;
	public ContestTypesList contest_category;
	public int pp_cost;
	public int power;
	public float accuracy;
	public bool recoil;
	public float recoil_damage;
	public bool high_crit_chance;
	public bool flinch;
	public float flinch_chance;
	public bool makes_contact;
	public bool affected_by_protect;
	public bool affected_by_magic_coat;
	public bool affected_by_snatch;
	public bool affected_by_kings_rock;
	public Pokemon.StatusConditions status_condition;
	public float status_condition_success_rate;
	public List<StatusEffect> status_effects = new List<StatusEffect>();
	public Sprite icon;
	public float range;
	public int damage;
	public float cool_down;
	public float cooling_down;
	public float animation_speed;
	public float chance_to_hit;
	public bool hit;

	public GameObject target;
	public List<GameObject> these_targets;
	public Pokemon target_pokemon;
	private DamageCalculation dmgCalc = new DamageCalculation();

	public enum MoveCategoriesList{ PHYSICAL, SPECIAL, STATUS }
	public enum ContestTypesList{ BEAUTY, COOL, CUTE, SMART, TOUGH }

	void Start()
	{
		this_pokemon = transform.GetComponent<Pokemon>();
		input = transform.GetComponent<PokemonInput>();
	}
	void Update()
	{
		level = this_pokemon.level;
		attack = this_pokemon.cur_atk;
		special_attack = this_pokemon.cur_spatk;
		acc = this_pokemon.accuracy;
		base_speed = this_pokemon.base_spd;
		type_one = this_pokemon.type_one;
		type_two = this_pokemon.type_two;
		if(cooling_down > 0.0f)
		{
			cooling_down -= Time.deltaTime;
			if(cooling_down < 0.0f)
			{
				cooling_down = 0.0f;
			}

		}
	}

	public void UseMove(GameObject pokemon, GameObject the_target, List<GameObject> targets)
	{
		hit = false;
		these_targets = targets;
		if(single_target)
		{
			target = the_target;
			target_pokemon = target.GetComponent<Pokemon>();
			target_defense = target_pokemon.cur_def;
			target_special_defense = target_pokemon.cur_spdef;
			target_eva = target_pokemon.evasion;
			target_type_one = target_pokemon.type_one;
			target_type_two = target_pokemon.type_two;
			if(category == MoveCategoriesList.PHYSICAL)
			{
				pokemon.transform.LookAt(target.transform);
				pokemon.animation[move_name].speed = animation_speed;
				pokemon.animation.Play(move_name);
				damage = dmgCalc.CalculateAttackDamage(power, high_crit_chance, type, level, attack, target_defense, type_one, type_two, target_type_one,
				                                       target_type_two, base_speed);
				chance_to_hit = accuracy * (acc / target_eva);
				float hit_or_miss = Random.Range(0.0f, 1.0f);
				if(chance_to_hit >= hit_or_miss)
				{
					hit = true;
				}
			}
			if(category == MoveCategoriesList.SPECIAL)
			{
				pokemon.transform.LookAt(target.transform);
				pokemon.animation[move_name].speed = animation_speed;
				pokemon.animation.Play(move_name);
				damage = dmgCalc.CalculateSpecialAttackDamage(power, high_crit_chance, type, level, special_attack, target_special_defense, type_one, type_two,
				                                              target_type_one, target_type_two, base_speed);

				chance_to_hit = accuracy * (acc / target_eva);
				float hit_or_miss = Random.Range(0.0f, 1.0f);
				if(chance_to_hit >= hit_or_miss)
				{
					hit = true;
				}
			}
			if(category == MoveCategoriesList.STATUS)
			{
				pokemon.transform.LookAt(target.transform);
				pokemon.animation[move_name].speed = animation_speed;
				pokemon.animation.Play(move_name);
				chance_to_hit = accuracy * (acc / target_eva);
				float hit_or_miss = Random.Range(0.0f, 1.0f);
				if(chance_to_hit >= hit_or_miss)
				{
					hit = true;
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
			if(hit)
			{
				target_pokemon.enemies.Add(pokemon);
			}
		}
		if(aoe)
		{
			foreach(GameObject this_target in targets)
			{
				if(Vector3.Distance(pokemon.transform.position, this_target.transform.position) < range)
				{
					target = this_target;
					target_pokemon = target.GetComponent<Pokemon>();
					target_defense = target_pokemon.cur_def;
					target_special_defense = target_pokemon.cur_spdef;
					target_eva = target_pokemon.evasion;
					target_type_one = target_pokemon.type_one;
					target_type_two = target_pokemon.type_two;
					if(category == MoveCategoriesList.PHYSICAL)
					{
						pokemon.animation[move_name].speed = animation_speed;
						pokemon.animation.Play(move_name);
						damage = dmgCalc.CalculateAttackDamage(power, high_crit_chance, type, level, attack, target_defense, type_one, type_two, target_type_one,
						                                       target_type_two, base_speed);
						chance_to_hit = accuracy * (acc / target_eva);
						float hit_or_miss = Random.Range(0.0f, 1.0f);
						if(chance_to_hit >= hit_or_miss)
						{
							hit = true;
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
					if(category == MoveCategoriesList.SPECIAL)
					{
						pokemon.animation[move_name].speed = animation_speed;
						pokemon.animation.Play(move_name);
						damage = dmgCalc.CalculateSpecialAttackDamage(power, high_crit_chance, type, level, special_attack, target_special_defense, type_one, type_two,
						                                              target_type_one, target_type_two, base_speed);
						
						chance_to_hit = accuracy * (acc / target_eva);
						float hit_or_miss = Random.Range(0.0f, 1.0f);
						if(chance_to_hit >= hit_or_miss)
						{
							hit = true;
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
					if(category == MoveCategoriesList.STATUS)
					{
						pokemon.animation[move_name].speed = animation_speed;
						pokemon.animation.Play(move_name);
					}
				}
				if(hit)
				{
					target_pokemon.enemies.Add(pokemon);
				}
			}
		}
		cooling_down = cool_down;
		this_pokemon.AdjustCurrentPP(-pp_cost);
	}
	public bool Equals (Move other)
	{
		return this.move_name == other.move_name;
	}
	public Move()
	{
		
	}
	public Move(string this_name, string this_description, int this_level_learned, PokemonTypes.Types this_type, MoveCategoriesList this_category,
	            ContestTypesList this_contest_type, int this_pp_cost, int this_power, float this_accuracy, bool this_recoil, float this_recoil_damage,
	            bool this_high_crit_chance, bool this_flinch, int this_flinch_chance, bool this_makes_contact, bool this_affected_by_protect,
	            bool this_affected_by_magic_coat, bool this_affected_by_snatch, bool this_affected_by_kings_rock, Pokemon.StatusConditions this_status_condition,
	            int this_status_condition_success_rate, List<StatusEffect> this_status_effects, Sprite this_icon,
	            float this_range, int this_damage, float this_cool_down, float this_cooling_down, float this_animation_speed)
	{
		move_name = this_name;
		description = this_description;
		level_learned = this_level_learned;
		type = this_type;
		category = this_category;
		contest_category = this_contest_type;
		pp_cost = this_pp_cost;
		power = this_power;
		accuracy = this_accuracy;
		recoil = this_recoil;
		recoil_damage = this_recoil_damage;
		high_crit_chance = this_high_crit_chance;
		flinch = this_flinch;
		flinch_chance = this_flinch_chance;
		makes_contact = this_makes_contact;
		affected_by_protect = this_affected_by_protect;
		affected_by_magic_coat = this_affected_by_magic_coat;
		affected_by_snatch = this_affected_by_snatch;
		affected_by_kings_rock = this_affected_by_kings_rock;
		status_condition = this_status_condition;
		status_condition_success_rate = this_status_condition_success_rate;
		status_effects = this_status_effects;
		icon = this_icon;
		range = this_range;
		damage = this_damage;
		cool_down = this_cool_down;
		cooling_down = this_cooling_down;
		animation_speed = this_animation_speed;
	}
}
