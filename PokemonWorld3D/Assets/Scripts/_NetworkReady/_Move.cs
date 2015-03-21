using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class _Move : MonoBehaviour
{
	public _Pokemon thisPokemon;
	public int level;
	public int attack;
	public int specialAttack;
	public float acc;
	public int baseSpeed;
	public PokemonTypes.Types typeOne;
	public PokemonTypes.Types typeTwo;
	public int targetDefense;
	public int targetSpecialDefense;
	public float targetEva;
	public PokemonTypes.Types targetTypeOne;
	public PokemonTypes.Types targetTypeTwo;
	public bool aoe;
	public string moveName;
	public string description;
	public int levelLearned;
	public PokemonTypes.Types type;
	public MoveCategoriesList category;
	public ContestTypesList contestCategory;
	public int ppCost;
	public int power;
	public float accuracy;
	public bool recoil;
	public float recoilDamage;
	public bool highCritChance;
	public bool flinch;
	public float flinchChance;
	public bool makesContact;
	public bool affectedByProtect;
	public bool affectedByMagicCoat;
	public bool affectedBySnatch;
	public bool affectedByKingsRock;
	public _Pokemon.StatusConditions statusCondition;
	public float statusConditionSuccessRate;
	public List<_StatusEffect> StatusEffects = new List<_StatusEffect>();
	public Sprite icon;
	public float range;
	public int damage;
	public float coolDown;
	public float coolingDown;
	public float animationSpeed;
	public float chanceToHit;
	public bool hit;
	public bool canTargetAlly;

	public GameObject target;
	public List<GameObject> Targets;
	public _Pokemon targetPokemon;
	private DamageCalculation dmgCalc = new DamageCalculation();
	private _PokemonInput input;

	public enum MoveCategoriesList{ PHYSICAL, SPECIAL, STATUS }
	public enum ContestTypesList{ BEAUTY, COOL, CUTE, SMART, TOUGH }

	void Start()
	{
		thisPokemon = transform.GetComponent<_Pokemon>();
		input = transform.GetComponent<_PokemonInput>();
	}
	void Update()
	{
		level = thisPokemon.level;
		attack = thisPokemon.curATK;
		specialAttack = thisPokemon.curSPATK;
		acc = thisPokemon.accuracy;
		baseSpeed = thisPokemon.baseSPD;
		typeOne = thisPokemon.typeOne;
		typeTwo = thisPokemon.typeTwo;
		if(coolingDown > 0.0f)
		{
			coolingDown -= Time.deltaTime;
			if(coolingDown < 0.0f)
			{
				coolingDown = 0.0f;
			}

		}
	}
	public void UseMove(GameObject pokemon, GameObject theTarget, List<GameObject> TheTargets)
	{
		hit = false;
		target = theTarget;
		targetPokemon = theTarget.GetComponent<_Pokemon>();
		targetDefense = targetPokemon.curDEF;
		targetSpecialDefense = targetPokemon.curSPDEF;
		targetEva = targetPokemon.evasion;
		targetTypeOne = targetPokemon.typeOne;
		targetTypeTwo = targetPokemon.typeTwo;
		Targets = TheTargets;
		if(target == gameObject && !canTargetAlly)
		{
			return;
		}
		else
		{
			input.attacking = true;
			if(!aoe)
			{
				if(category == MoveCategoriesList.PHYSICAL)
				{
					pokemon.transform.LookAt(target.transform);
					pokemon.GetComponent<Animator>().SetBool(moveName, true);;
					damage = dmgCalc.CalculateAttackDamage(power, highCritChance, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
					                                       targetTypeTwo, baseSpeed);
					chanceToHit = accuracy * (acc / targetEva);
					float hit_or_miss = Random.Range(0.0f, 1.0f);
					if(chanceToHit >= hit_or_miss)
					{
						hit = true;
					}
				}
				if(category == MoveCategoriesList.SPECIAL)
				{
					pokemon.transform.LookAt(target.transform);
					pokemon.animation[moveName].speed = animationSpeed;
					pokemon.animation.Play(moveName);
					damage = dmgCalc.CalculateSpecialAttackDamage(power, highCritChance, type, level, specialAttack, targetSpecialDefense, typeOne, typeTwo,
					                                              targetTypeOne, targetTypeTwo, baseSpeed);

					chanceToHit = accuracy * (acc / targetEva);
					float hit_or_miss = Random.Range(0.0f, 1.0f);
					if(chanceToHit >= hit_or_miss)
					{
						hit = true;
					}
				}
				if(category == MoveCategoriesList.STATUS)
				{
					pokemon.transform.LookAt(target.transform);
					pokemon.animation[moveName].speed = animationSpeed;
					pokemon.animation.Play(moveName);
					chanceToHit = accuracy * (acc / targetEva);
					float hit_or_miss = Random.Range(0.0f, 1.0f);
					if(chanceToHit >= hit_or_miss)
					{
						hit = true;
					}
				}
			}
			if(aoe)
			{
				hit = true;
				pokemon.animation[moveName].speed = animationSpeed;
				pokemon.animation.Play(moveName);
			}
		}
	}
	public void MoveResults()
	{
		if(!aoe)
		{
			if(hit)
			{
				if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
				{
					//target.GetComponent<_Pokemon>().AdjustCurrentHP(-damage);
					int pokemon = GetComponent<PhotonView>().viewID;
					target.GetComponent<PhotonView>().RPC("AdjustCurrentHP", PhotonTargets.All, -damage, pokemon);
					//target.GetComponent<_Pokemon>().Enemies.Add(gameObject);
					if(statusCondition != _Pokemon.StatusConditions.NONE)
					{
						float statusChance = Random.Range(0.0f, 1.0f);
						if(statusConditionSuccessRate > statusChance)
						{
							target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.All, statusCondition);
						}
					}
					foreach(_StatusEffect effect in StatusEffects)
					{
						float chanceToApply = Random.Range(0.0f, 1.0f);
						if(effect.successRate >= chanceToApply)
						{
							if(effect.changeStat)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.All, effect.statToChange, effect.stagestoChange);
							}
							if(effect.changeAccOrEva)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.All, effect.accOrEva, effect.stagestoChange);
							}
						}
					}
				}
				if(category == MoveCategoriesList.STATUS)
				{
					if(statusCondition != _Pokemon.StatusConditions.NONE)
					{
						float statusChance = Random.Range(0.0f, 1.0f);
						if(statusConditionSuccessRate > statusChance)
						{
							target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.All, statusCondition);
						}
					}
					foreach(_StatusEffect effect in StatusEffects)
					{
						float chanceToApply = Random.Range(0.0f, 1.0f);
						if(effect.successRate >= chanceToApply)
						{
							if(effect.changeStat)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.All, effect.statToChange, effect.stagestoChange);
							}
							if(effect.changeAccOrEva)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.All, effect.accOrEva, effect.stagestoChange);
							}
						}
					}
				}
			}
		}
		if(aoe)
		{
			foreach(GameObject thisTarget in Targets)
			{
				if(!canTargetAlly)
				{
					if(thisTarget != gameObject)//Also add something here to check if target's a team member.
					{
						if(Vector3.Distance(thisPokemon.gameObject.transform.position, thisTarget.transform.position) <= range)
						{
							target = thisTarget;
							targetPokemon = target.GetComponent<_Pokemon>();
							targetDefense = targetPokemon.curDEF;
							targetSpecialDefense = targetPokemon.curSPDEF;
							targetEva = targetPokemon.evasion;
							targetTypeOne = targetPokemon.typeOne;
							targetTypeTwo = targetPokemon.typeTwo;
							if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
							{
								damage = dmgCalc.CalculateAttackDamage(power, highCritChance, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
								                                       targetTypeTwo, baseSpeed);
								target.GetComponent<PhotonView>().RPC("AdjustCurrentHP", PhotonTargets.All, -damage);
								if(statusCondition != _Pokemon.StatusConditions.NONE)
								{
									float status_chance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > status_chance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.All, statusCondition);
									}
								}
								foreach(_StatusEffect effect in StatusEffects)
								{
									float chance_to_apply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chance_to_apply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.All, effect.statToChange, effect.stagestoChange);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.All, effect.accOrEva, effect.stagestoChange);
										}
									}
								}
								
							}
							if(category == MoveCategoriesList.STATUS)
							{
								if(statusCondition != _Pokemon.StatusConditions.NONE)
								{
									float statusChance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > statusChance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.All, statusCondition);
									}
								}
								foreach(_StatusEffect effect in StatusEffects)
								{
									float chanceToApply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chanceToApply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.All, effect.statToChange, effect.stagestoChange);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.All, effect.accOrEva, effect.stagestoChange);
										}
									}
								}
							}
						}
					}
				}
				if(canTargetAlly)
				{
					if(thisTarget == gameObject)//Also add something here to check if target's a team member.
					{

					}
				}
			}
		}
		coolingDown = coolDown;
		GetComponent<PhotonView>().RPC("AdjustCurrentPP", PhotonTargets.All, -ppCost);
	}
	public bool Equals (Move other)
	{
		return this.moveName == other.move_name;
	}
	public _Move()
	{
		
	}
	public _Move(string this_name, string this_description, int this_level_learned, PokemonTypes.Types this_type, MoveCategoriesList this_category,
	            ContestTypesList this_contest_type, int this_pp_cost, int this_power, float this_accuracy, bool this_recoil, float this_recoil_damage,
	            bool this_high_crit_chance, bool this_flinch, int this_flinch_chance, bool this_makes_contact, bool this_affected_by_protect,
	            bool this_affected_by_magic_coat, bool this_affected_by_snatch, bool this_affected_by_kings_rock, _Pokemon.StatusConditions this_status_condition,
	            int this_status_condition_success_rate, List<_StatusEffect> this_status_effects, Sprite this_icon,
	            float this_range, int this_damage, float this_cool_down, float this_cooling_down, float this_animation_speed)
	{
		moveName = this_name;
		description = this_description;
		levelLearned = this_level_learned;
		type = this_type;
		category = this_category;
		contestCategory = this_contest_type;
		ppCost = this_pp_cost;
		power = this_power;
		accuracy = this_accuracy;
		recoil = this_recoil;
		recoilDamage = this_recoil_damage;
		highCritChance = this_high_crit_chance;
		flinch = this_flinch;
		flinchChance = this_flinch_chance;
		makesContact = this_makes_contact;
		affectedByProtect = this_affected_by_protect;
		affectedByMagicCoat = this_affected_by_magic_coat;
		affectedBySnatch = this_affected_by_snatch;
		affectedByKingsRock = this_affected_by_kings_rock;
		statusCondition = this_status_condition;
		statusConditionSuccessRate = this_status_condition_success_rate;
		StatusEffects = this_status_effects;
		icon = this_icon;
		range = this_range;
		damage = this_damage;
		coolDown = this_cool_down;
		coolingDown = this_cooling_down;
		animationSpeed = this_animation_speed;
	}
}
