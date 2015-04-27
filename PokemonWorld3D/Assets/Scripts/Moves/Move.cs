using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Move : MonoBehaviour
{
	[HideInInspector]
	public Pokemon thisPokemon;
	[HideInInspector]
	public int level;
	[HideInInspector]
	public int attack;
	[HideInInspector]
	public int specialAttack;
	[HideInInspector]
	public float acc;
	[HideInInspector]
	public int baseSpeed;
	[HideInInspector]
	public PokemonTypes.Types typeOne;
	[HideInInspector]
	public PokemonTypes.Types typeTwo;
	[HideInInspector]
	public int targetDefense;
	[HideInInspector]
	public int targetSpecialDefense;
	[HideInInspector]
	public float targetEva;
	[HideInInspector]
	public PokemonTypes.Types targetTypeOne;
	[HideInInspector]
	public PokemonTypes.Types targetTypeTwo;
	[HideInInspector]
	public bool aoe;
	[HideInInspector]
	public bool selfTargeting;
	[HideInInspector]
	public bool allyTargeting;
	[HideInInspector]
	public string moveName;
	[HideInInspector]
	public string description;
	[HideInInspector]
	public PokemonTypes.Types type;
	[HideInInspector]
	public MoveCategoriesList category;
	[HideInInspector]
	public ContestTypesList contestCategory;
	[HideInInspector]
	public int ppCost;
	[HideInInspector]
	public int power;
	[HideInInspector]
	public float accuracy;
	[HideInInspector]
	public bool recoil;
	[HideInInspector]
	public float recoilDamage;
	[HideInInspector]
	public bool highCritChance;
	[HideInInspector]
	public bool flinch;
	[HideInInspector]
	public bool makesContact;
	[HideInInspector]
	public bool affectedByProtect;
	[HideInInspector]
	public bool affectedByMagicCoat;
	[HideInInspector]
	public bool affectedBySnatch;
	[HideInInspector]
	public bool affectedByKingsRock;
	[HideInInspector]
	public float coolDown;
	[HideInInspector]
	public bool disabled;

	public int levelLearned;
	public List<StatusEffect> StatusEffects;
	public Sprite icon;
	public float range;

	public float coolingDown {get; private set;}
	public GameObject target {get; private set;}

	private float chanceToHit;
	private bool hit;
	private bool critHit;
	private int damage;
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
		level = thisPokemon.level;
		attack = thisPokemon.curATK;
		specialAttack = thisPokemon.curSPATK;
		acc = thisPokemon.accuracy;
		baseSpeed = thisPokemon.baseSPD;
		typeOne = thisPokemon.typeOne;
		typeTwo = thisPokemon.typeTwo;
		hit = false;
		critHit = false;
		if(selfTargeting)
		{
			target = gameObject;
		}
		else
		{
			target = theTarget;
		}
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
		if(target == gameObject && !selfTargeting)
		{
			return;
		}
		else if(!thisPokemon.Enemies.Contains(target) && !selfTargeting && !allyTargeting)
		{
			return;
		}
		else
		{
			pokemon.transform.LookAt(target.transform);
			input.attacking = true;
			if(!aoe)
			{
				if(!allyTargeting && !selfTargeting)
				{
					if(target != gameObject)//Also add something here to check if target's a team member.
					{
						if(category == MoveCategoriesList.PHYSICAL)
						{
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							critHit = dmgCalc.DetermineCritical(baseSpeed, highCritChance);
							damage = dmgCalc.CalculateAttackDamage(power, critHit, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
							                                       targetTypeTwo);
							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
						if(category == MoveCategoriesList.SPECIAL)
						{
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							critHit = dmgCalc.DetermineCritical(baseSpeed, highCritChance);
							damage = dmgCalc.CalculateSpecialAttackDamage(power, critHit, type, level, specialAttack, targetSpecialDefense, typeOne, typeTwo,
							                                              targetTypeOne, targetTypeTwo);

							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
						if(category == MoveCategoriesList.STATUS)
						{
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
				if(allyTargeting || selfTargeting)
				{
					if(target == gameObject)//Also add something here to check if target's a team member.
					{
						if(category == MoveCategoriesList.PHYSICAL)
						{
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							damage = dmgCalc.CalculateAttackDamage(power, critHit, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
							                                       targetTypeTwo);
							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
						if(category == MoveCategoriesList.SPECIAL)
						{
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							damage = dmgCalc.CalculateSpecialAttackDamage(power, critHit, type, level, specialAttack, targetSpecialDefense, typeOne, typeTwo,
							                                              targetTypeOne, targetTypeTwo);
							
							chanceToHit = accuracy * (acc / targetEva);
							float hit_or_miss = Random.Range(0.0f, 1.0f);
							if(chanceToHit >= hit_or_miss)
							{
								hit = true;
							}
						}
						if(category == MoveCategoriesList.STATUS)
						{
							pokemon.GetComponent<Animator>().SetBool(moveName, true);
							hit = true;
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
				if(selfTargeting)
				{
					target = gameObject;
				}
				if(category == MoveCategoriesList.PHYSICAL || category == MoveCategoriesList.SPECIAL)
				{
					target.GetComponent<PhotonView>().RPC("AdjustHP", PhotonTargets.AllBuffered, -damage, "current", pokemon, critHit);
					foreach(StatusEffect effect in StatusEffects)
					{
						target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
						                                       effect.duration, pokemon);
					}
				}
				if(category == MoveCategoriesList.STATUS)
				{
					foreach(StatusEffect effect in StatusEffects)
					{
						target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
						                                       effect.duration, pokemon);
					}
				}
			}
			if(!targetPokemon.isCaptured)
				target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
		}
		if(aoe)
		{
			foreach(GameObject thisTarget in Targets)
			{
				if(!allyTargeting && !selfTargeting)
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
								damage = dmgCalc.CalculateAttackDamage(power, critHit, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
								                                       targetTypeTwo);
								target.GetComponent<PhotonView>().RPC("AdjustHP", PhotonTargets.AllBuffered, -damage, "current", pokemon, critHit);
								foreach(StatusEffect effect in StatusEffects)
								{
									target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
									                                       effect.duration, pokemon);
								}
								
							}
							if(category == MoveCategoriesList.STATUS)
							{
								foreach(StatusEffect effect in StatusEffects)
								{
									target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
									                                       effect.duration, pokemon);
								}
							}
						}
					}
					if(!targetPokemon.isCaptured)
						target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
				}
				if(allyTargeting || selfTargeting)
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
								damage = dmgCalc.CalculateAttackDamage(power, critHit, type, level, attack, targetDefense, typeOne, typeTwo, targetTypeOne,
								                                       targetTypeTwo);
								target.GetComponent<PhotonView>().RPC("AdjustHP", PhotonTargets.AllBuffered, -damage, "current", pokemon, critHit);
								foreach(StatusEffect effect in StatusEffects)
								{
									target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
									                                       effect.duration, pokemon);
								}
								
							}
							if(category == MoveCategoriesList.STATUS)
							{
								foreach(StatusEffect effect in StatusEffects)
								{
									target.GetComponent<PhotonView>().RPC ("AddStatusEffect", PhotonTargets.AllBuffered, effect.buffOrDebuff, effect.percentage,
									                                       effect.duration, pokemon);
								}
							}
						}
					}
					if(!targetPokemon.isCaptured)
						target.GetComponent<PhotonView>().RPC("IncreaseHate", PhotonTargets.AllBuffered, pokemon, 10);
				}
			}
		}
		thisPokemon.lastMoveUsed = this;
		coolingDown = coolDown;
		GetComponent<PhotonView>().RPC("AdjustPP", PhotonTargets.AllBuffered, -ppCost, "current", pokemon);
	}
	public bool Equals (Move other)
	{
		return this.moveName == other.moveName;
	}
	public Move()
	{
		aoe = false;
		selfTargeting = false;
		allyTargeting = false;
		moveName = "";
		description = "";
		levelLearned = 0;
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.PHYSICAL;
		contestCategory = ContestTypesList.BEAUTY;
		ppCost = 0;
		power = 0;
		accuracy = 0.0f;
		recoil = false;
		recoilDamage = 0.0f;
		highCritChance = false;
		flinch = false;
		makesContact = false;
		affectedByProtect = false;
		affectedByMagicCoat = false;
		affectedBySnatch = false;
		affectedByKingsRock = false;
		StatusEffects = new List<StatusEffect>();
		range = 1.0f;
		coolDown = 1.0f;
		disabled = false;
	}
	public Move(string this_name, string this_description, int this_level_learned, PokemonTypes.Types this_type, MoveCategoriesList this_category,
	            ContestTypesList this_contest_type, int this_pp_cost, int this_power, float this_accuracy, bool this_recoil, float this_recoil_damage,
	            bool this_high_crit_chance, bool this_flinch, bool this_makes_contact, bool this_affected_by_protect,
	            bool this_affected_by_magic_coat, bool this_affected_by_snatch, bool this_affected_by_kings_rock, List<StatusEffect> this_status_effects,
	            Sprite this_icon, float this_range, int this_damage, float this_cool_down, float this_cooling_down)
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
		makesContact = this_makes_contact;
		affectedByProtect = this_affected_by_protect;
		affectedByMagicCoat = this_affected_by_magic_coat;
		affectedBySnatch = this_affected_by_snatch;
		affectedByKingsRock = this_affected_by_kings_rock;
		StatusEffects = this_status_effects;
		icon = this_icon;
		range = this_range;
		damage = this_damage;
		coolDown = this_cool_down;
		coolingDown = this_cooling_down;
	}
}
