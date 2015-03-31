using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Move : MonoBehaviour
{
	private Pokemon thisPokemon;
	private int level;
	private int attack;
	private int specialAttack;
	private float acc;
	private int baseSpeed;
	private PokemonTypes.Types typeOne;
	private PokemonTypes.Types typeTwo;
	private int targetDefense;
	private int targetSpecialDefense;
	private float targetEva;
	private PokemonTypes.Types targetTypeOne;
	private PokemonTypes.Types targetTypeTwo;
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
	public Pokemon.StatusConditions statusCondition;
	public float statusConditionSuccessRate;
	public List<StatusEffect> StatusEffects = new List<StatusEffect>();
	public Sprite icon;
	public float range;
	public int damage;
	public float coolDown;
	public float coolingDown;
	public float animationSpeed;
	public float chanceToHit;
	public bool hit;
	public bool canTargetAlly;
	public GameObject floatingDmg;

	public GameObject target;
	private List<GameObject> Targets;
	private Pokemon targetPokemon;
	private BattleCalculations dmgCalc;
	private PokemonInput input;

	public enum MoveCategoriesList{ PHYSICAL, SPECIAL, STATUS }
	public enum ContestTypesList{ BEAUTY, COOL, CUTE, SMART, TOUGH }

	void Start()
	{
		thisPokemon = transform.GetComponent<Pokemon>();
		input = transform.GetComponent<PokemonInput>();
		dmgCalc = GetComponent<BattleCalculations>();
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
	public void UseMove(GameObject pokemon, GameObject theTarget)
	{
		hit = false;
		target = theTarget;
		targetPokemon = theTarget.GetComponent<Pokemon>();
		targetDefense = targetPokemon.curDEF;
		targetSpecialDefense = targetPokemon.curSPDEF;
		targetEva = targetPokemon.evasion;
		targetTypeOne = targetPokemon.typeOne;
		targetTypeTwo = targetPokemon.typeTwo;
		Collider[] TempList = Physics.OverlapSphere(pokemon.transform.position, range);
		Targets = new List<GameObject>();
		foreach(Collider col in TempList)
		{
			if(thisPokemon.Enemies.Contains(col.gameObject))
				Targets.Add(col.gameObject);
		}
		if(target == gameObject && !canTargetAlly)
		{
			return;
		}
		else if(!thisPokemon.Enemies.Contains(target))
		{
			return;
		}
		else
		{
			input.attacking = true;
			if(!aoe)
			{
				if(!canTargetAlly)
				{
					if(target != gameObject)//Also add something here to check if target's a team member.
					{
						if(category == MoveCategoriesList.PHYSICAL)
						{
							pokemon.transform.LookAt(target.transform);
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
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
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
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
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
					}
				}
				if(canTargetAlly)
				{
					if(target == gameObject)//Also add something here to check if target's a team member.
					{
						if(category == MoveCategoriesList.PHYSICAL)
						{
							pokemon.transform.LookAt(target.transform);
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
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
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
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
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
					}
				}
			}
			if(aoe)
			{
				hit = true;
				pokemon.GetComponent<Animator>().SetBool(moveName, true);
			}
		}
	}
	public void MoveResults()
	{
		int pokemon = GetComponent<PhotonView>().viewID;
		if(!aoe)
		{
			if(hit)
			{
				if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
				{
					target.GetComponent<PhotonView>().RPC("AdjustCurrentHP", PhotonTargets.AllBuffered, -damage, pokemon);
					if(statusCondition != Pokemon.StatusConditions.NONE)
					{
						float statusChance = Random.Range(0.0f, 1.0f);
						if(statusConditionSuccessRate > statusChance)
						{
							target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
						}
					}
					foreach(StatusEffect effect in StatusEffects)
					{
						float chanceToApply = Random.Range(0.0f, 1.0f);
						if(effect.successRate >= chanceToApply)
						{
							if(effect.changeStat)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange, effect.stagestoChange,
								                                      pokemon);
							}
							if(effect.changeAccOrEva)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva, effect.stagestoChange,
								                                      pokemon);
							}
						}
					}
				}
				if(category == MoveCategoriesList.STATUS)
				{
					if(statusCondition != Pokemon.StatusConditions.NONE)
					{
						float statusChance = Random.Range(0.0f, 1.0f);
						if(statusConditionSuccessRate > statusChance)
						{
							target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
						}
					}
					foreach(StatusEffect effect in StatusEffects)
					{
						float chanceToApply = Random.Range(0.0f, 1.0f);
						if(effect.successRate >= chanceToApply)
						{
							if(effect.changeStat)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange, effect.stagestoChange,
								                                      pokemon);
							}
							if(effect.changeAccOrEva)
							{
								target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva, effect.stagestoChange,
								                                      pokemon);
							}
						}
					}
				}
				//SpawnDamage(target.transform.position.x, target.transform.position.y, damage);
			}
			if(!targetPokemon.isCaptured)
				target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
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
							targetPokemon = target.GetComponent<Pokemon>();
							targetDefense = targetPokemon.curDEF;
							targetSpecialDefense = targetPokemon.curSPDEF;
							targetEva = targetPokemon.evasion;
							targetTypeOne = targetPokemon.typeOne;
							targetTypeTwo = targetPokemon.typeTwo;
							if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
							{
								damage = dmgCalc.CalculateAttackDamage(power, highCritChance, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
								                                       targetTypeTwo, baseSpeed);
								target.GetComponent<PhotonView>().RPC("AdjustCurrentHP", PhotonTargets.AllBuffered, -damage, pokemon);
								if(statusCondition != Pokemon.StatusConditions.NONE)
								{
									float status_chance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > status_chance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
									}
								}
								foreach(StatusEffect effect in StatusEffects)
								{
									float chance_to_apply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chance_to_apply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange,
											                                      effect.stagestoChange, pokemon);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva,
											                                      effect.stagestoChange, pokemon);
										}
									}
								}
								
							}
							if(category == MoveCategoriesList.STATUS)
							{
								if(statusCondition != Pokemon.StatusConditions.NONE)
								{
									float statusChance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > statusChance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
									}
								}
								foreach(StatusEffect effect in StatusEffects)
								{
									float chanceToApply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chanceToApply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange,
											                                      effect.stagestoChange, pokemon);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva,
											                                      effect.stagestoChange, pokemon);
										}
									}
								}
							}
						}
					}
					if(!targetPokemon.isCaptured)
						target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
				}
				if(canTargetAlly)
				{
					if(thisTarget == gameObject)//Also add something here to check if target's a team member.
					{
						if(Vector3.Distance(thisPokemon.gameObject.transform.position, thisTarget.transform.position) <= range)
						{
							target = thisTarget;
							targetPokemon = target.GetComponent<Pokemon>();
							targetDefense = targetPokemon.curDEF;
							targetSpecialDefense = targetPokemon.curSPDEF;
							targetEva = targetPokemon.evasion;
							targetTypeOne = targetPokemon.typeOne;
							targetTypeTwo = targetPokemon.typeTwo;
							if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
							{
								damage = dmgCalc.CalculateAttackDamage(power, highCritChance, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
								                                       targetTypeTwo, baseSpeed);
								target.GetComponent<PhotonView>().RPC("AdjustCurrentHP", PhotonTargets.AllBuffered, -damage, pokemon);
								if(statusCondition != Pokemon.StatusConditions.NONE)
								{
									float status_chance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > status_chance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
									}
								}
								foreach(StatusEffect effect in StatusEffects)
								{
									float chance_to_apply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chance_to_apply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange,
											                                      effect.stagestoChange, pokemon);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva,
											                                      effect.stagestoChange, pokemon);
										}
									}
								}
								
							}
							if(category == MoveCategoriesList.STATUS)
							{
								if(statusCondition != Pokemon.StatusConditions.NONE)
								{
									float statusChance = Random.Range(0.0f, 1.0f);
									if(statusConditionSuccessRate > statusChance)
									{
										target.GetComponent<PhotonView>().RPC("AdjustStatusCondition", PhotonTargets.AllBuffered, statusCondition, pokemon);
									}
								}
								foreach(StatusEffect effect in StatusEffects)
								{
									float chanceToApply = Random.Range(0.0f, 1.0f);
									if(effect.successRate >= chanceToApply)
									{
										if(effect.changeStat)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentStat", PhotonTargets.AllBuffered, effect.statToChange,
											                                      effect.stagestoChange, pokemon);
										}
										if(effect.changeAccOrEva)
										{
											target.GetComponent<PhotonView>().RPC("AdjustCurrentAccEva", PhotonTargets.AllBuffered, effect.accOrEva,
											                                      effect.stagestoChange, pokemon);
										}
									}
								}
							}
						}
					}
					if(!targetPokemon.isCaptured)
						target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
				}
			}
		}
		coolingDown = coolDown;
		GetComponent<PhotonView>().RPC("AdjustCurrentPP", PhotonTargets.AllBuffered, -ppCost, pokemon);
	}
	private void SpawnDamage(float x, float y, int damageAmount)
	{
		x = Mathf.Clamp(x, 0.05f, 0.95f); // clamp position to screen to ensure
		y = Mathf.Clamp(y, 0.05f, 0.9f);  // the string will be visible
		GameObject damage = Instantiate(floatingDmg, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
		damage.guiText.text = damageAmount.ToString();
	}
	public bool Equals (Move other)
	{
		return this.moveName == other.moveName;
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
