using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pokemon : MonoBehaviour
{
	#region Variables
	public bool isAlive = false;
	public bool isSetup = false;
	public float timeOfDeath;
	public bool isCaptured = false;
	public GameObject trainer;
	public string trainersName = "";
	public int pokemonNumber;
	public string pokemonName;
	public string nickName = "";
	public string description;
	public bool isFromTrade = false;
	public int level;
	public int evolveLevel;
	public PokemonTypes.Types typeOne;
	public PokemonTypes.Types typeTwo;
	public Genders gender;
	public Natures nature;
	public string abilityOne;
	public string abilityTwo;
	public int baseHP;
	public int basePP;
	public int baseATK;
	public int baseDEF;
	public int baseSPATK;
	public int baseSPDEF;
	public int baseSPD;
	public int maxHP;
	public int maxPP;
	public int maxATK;
	public int maxDEF;
	public int maxSPATK;
	public int maxSPDEF;
	public int maxSPD;
	public int curMaxHP;
	public int curMaxPP;
	public int curHP;
	public int curPP;
	public int curATK;
	public int curDEF;
	public int curSPATK;
	public int curSPDEF;
	public int curSPD;
	public float evasion;
	public float accuracy;
	public int hpEV;
	public int atkEV;
	public int defEV;
	public int spatkEV;
	public int spdefEV;
	public int spdEV;
	public int hpIV;
	public int atkIV;
	public int defIV;
	public int spatkIV;
	public int spdefIV;
	public int spdIV;
	public int baseEXPYield;
	public LevelingRates levelingRate;
	public int lastRequiredEXP;
	public int currentEXP;
	public int nextRequiredEXP;
	public int hpEVYield;
	public int atkEVYield;
	public int defEVYield;
	public int spatkEVYield;
	public int spdefEVYield;
	public int spdEVYield;
	public int baseFriendship;
	public int captureRate;
	public List<StatusEffect> StatusEffect;
	public float effectTicker;
	public bool badlyPoisoned;
	public bool burned;
	public bool frozen;
	public bool paralyzed;
	public bool poisoned;
	public bool sleeping;
	public float badlyPoisonedTimer;
	public float sleepTimer;
	public bool confused;
	public float confusedTimer;
	public bool cursed;
	public bool embargoed;
	public float embargoTimer;
	public bool encored;
	public float encoreTimer;
	public bool flinched;
	public bool healBlocked;
	public float healBlockTimer;
	public bool identified;
	public bool infatuated;
	public bool nightmared;
	public bool partiallyTrapped;
	public float partiallyTrappedTimer;
	public bool perishSonged;
	public float perishSongCountDown;
	public bool seeded;
	public GameObject seededBy;
	public bool taunted;
	public float tauntTimer;
	public bool telekinecticallyLevitating;
	public float telekineticLevitationTimer;
	public bool tormented;
	public bool trapped;
	public bool aquaRinged;
	public bool bracing;
	public bool centerOfAttention;
	public bool defenseCurling;
	public bool rooting;
	public bool magicallyCoated;
	public bool magneticallyLevitating;
	public float magneticLevitationTimer;
	public bool minimized;
	public bool protecting;
	public bool recharging;
	public bool semiInvulnerable;
	public bool hasAStubstitute;
	public GameObject substitute;
	public bool takingAim;
	public List<string> MovesToLearnNames;
	public List<Move> MovesToLearn;
	public List<string> KnownMovesNames;
	public List<Move> KnownMoves;
	public Move lastMoveUsed;
	public _Item equippedItem;
	public bool isInBattle;
	public int origin;
	public int genderRatio;
	public bool isShiny = false;
	public Sprite avatar;
	public LensFlare evolveFalre;
	public float flareGrowDelay = 0.05f;
	public float flareDieDelay = 0.075f;
	public float evolutionFlareSize;
	public string evolvesInto;
	public GameObject mesh;
	public List<GameObject> Enemies;
	public List<int> PokemonToGiveEXPTo;
	public GameObject overHeadInfo;
	public HUD hud;
	
	public enum Genders { NONE, FEMALE, MALE }
	public enum Natures { ADAMANT, BASHFUL, BOLD, BRAVE, CALM, CAREFUL, DOCILE, GENTLE, HARDY, HASTY, IMPISH, JOLLY, LAX, LONELY, MILD, MODEST, NAIVE, NAUGHTY,
		QUIET, QUIRKY, RASH, RELAXED, SASSY, SERIOUS, TIMID }
	public enum LevelingRates { ERRATIC, FAST, FLUCTUATING, MEDIUM_FAST, MEDIUM_SLOW, SLOW }
	public enum BuffsAndDebuffs { NONE, ACC_DWN, ACC_UP, ATK_DWN, ATK_UP, BURNED, CONFUSED, DEF_DWN, DEF_UP, EVA_DWN, EVA_UP, FROZEN, PARALYZED, POISONED,
		SEEDED, SLEEPING, SPATK_DWN, SPATK_UP, SPDEF_DWN, SPDEF_UP, SPD_DWN, SPD_UP }
	
	private StatCalculations statCalculationsScript = new StatCalculations();
	private CalculateXP calculateEXPScript = new CalculateXP();
	private IncreaseExperience increaseEXPScript = new IncreaseExperience();
	private bool fainting;
	private bool evolving;
	private Animator anim;
	private Pokemon target;
	private int thisPokemonsID;
	#endregion
	
	void Start()
	{
		anim = GetComponent<Animator>();
		thisPokemonsID = GetComponent<PhotonView>().viewID;
		InvokeRepeating("RegeneratePP", 1.0f, 1.0f);
 	}
	void Update()
	{
		effectTicker += Time.deltaTime;
		for(int i = 0; i < StatusEffect.Count; i++)
		{
			StatusEffect[i].duration -= Time.deltaTime;
			if(StatusEffect[i].duration <= 0.0f)
			{
				RemoveBuffDebuff(StatusEffect[i].buffOrDebuff, StatusEffect[i].percentage, 0.0f, null);
				StatusEffect.RemoveAt(i);
			}
		}
		if(paralyzed)
		{
			anim.SetBool("Default", false);
			float chance = Random.Range(0.0f, 1.0f);
			if(chance <= 0.25f)
			{
				anim.SetBool("Default", true);
			}

		}
		if(perishSonged)
		{
			perishSongCountDown -= Time.deltaTime;
			if(perishSongCountDown <= 0.0f)
				AdjustHP(-curHP, "current", thisPokemonsID, false);
		}
		if(sleeping)
		{
			sleepTimer -= Time.deltaTime;
			if(sleepTimer <= 0.0f)
			{
				RemoveBuffDebuff(BuffsAndDebuffs.SLEEPING, 0.0f, 0.0f, null);
			}
		}
		if(effectTicker >= 10.0f)
		{
			if(aquaRinged)
			{
				int amount = (int)((float)curHP * 0.0625f);
				if(amount < 1)
					amount = 1;
				AdjustHP(amount, "current", thisPokemonsID, false);
			}
			if(burned)
			{
				int amount = (int)((float)curHP * 0.125f);
				if(amount < 1)
					amount = 1;
				AdjustHP(-amount, "current", thisPokemonsID, false);
			}
			if(cursed)
			{
				int amount = (int)((float)curHP * 0.25f);
				if(amount < 1)
					amount = 1;
				AdjustHP(-amount, "current", thisPokemonsID, false);
			}
			if(nightmared)
			{
				int amount = (int)((float)curHP * 0.25f);
				if(amount < 1)
					amount = 1;
				AdjustHP(-amount, "current", thisPokemonsID, false);
			}
			if(poisoned)
			{
				int amount = (int)((float)curHP * 0.125f);
				if(amount < 1)
					amount = 1;
				AdjustHP(-amount, "current", thisPokemonsID, false);
			}
			if(rooting)
			{
				int amount = (int)((float)curHP * 0.0625f);
				if(amount < 1)
					amount = 1;
				AdjustHP(amount, "current", thisPokemonsID, false);
			}
			if(seeded)
			{
				int amount = (int)((float)curHP * 0.125f);
				if(amount < 1)
					amount = 1;
				AdjustHP(-amount, "current", thisPokemonsID, false);
				seededBy.GetComponent<PhotonView>().RPC("AdjustHP", PhotonTargets.AllBuffered, amount, "current", thisPokemonsID, false);
			}
			effectTicker = 0.0f;
		}
	}
	public void Frozen()
	{
		GetComponent<PokemonInput>().enabled = false;
		GetComponent<WildPokemonAI>().state = WildPokemonAI.State.DontMove;
	}
	public void Thawed()
	{
		if(isCaptured)
		{
			GetComponent<PokemonInput>().enabled = true;
		}
		else
		{
			GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Battle;
		}
	}
	public void Sleep()
	{
		GetComponent<PokemonInput>().enabled = false;
		GetComponent<WildPokemonAI>().state = WildPokemonAI.State.DontMove;
		anim.SetBool("Sleep", sleeping);
		sleepTimer = 30.0f;
	}
	public void WakeUp()
	{
		anim.SetBool("Sleep", sleeping);
		if(nightmared)
			nightmared = false;
		if(isCaptured)
		{
			GetComponent<PokemonInput>().enabled = true;
		}
		else
		{
			GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Battle;
		}
	}
	[RPC]
	public void Flinch()
	{
		StartCoroutine("Flinching");
	}
	[RPC]
	public void StartWildPokemonBattle(int opponent)
	{
		Enemies = new List<GameObject>();
		GameObject enemy = PhotonView.Find(opponent).gameObject;
		Enemies.Add(enemy);
		Pokemon targetPokemon = PhotonView.Find(opponent).gameObject.GetComponent<Pokemon>();
		target = targetPokemon;
		isInBattle = true;
		anim.SetBool("InBattle", true);
		if(!isCaptured)
		{
			GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Battle;
		}
	}
	[RPC]
	public void EndWildPokemonBattle()
	{
		isInBattle = false;
		anim.SetBool("InBattle", false);
		if(!isCaptured && isAlive)
		{
			GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Idle;
		}
		target = null;
	}
	[RPC]
	public void AdjustHP(int adj, string currentOrMax, int attacker, bool critical)
	{
		if(currentOrMax == "max")
		{
			curMaxHP += adj;
			if(curMaxHP < 0)
			{
				curMaxHP = 0;
			}
			if(curMaxHP > maxHP)
			{
				curMaxHP = maxHP;
			}
		}
		if(currentOrMax == "current")
		{
			if(adj < 0 && adj >= -curHP)
			{
				if(bracing)
					adj = (-curHP + 1);
				bracing = false;
			}
			curHP += adj;
			Vector3 here = overHeadInfo.transform.position;
			GameObject floatDmg = PhotonNetwork.Instantiate("Floating_Damage", here, Quaternion.identity, 0) as GameObject;
			if(adj < 0)
			{
				floatDmg.GetComponent<FloatingDamage>().color = Color.red;
				floatDmg.GetComponent<FloatingDamage>().amount = Mathf.Abs((float)adj).ToString();
				floatDmg.GetComponent<FloatingDamage>().crit = critical;
				if(sleeping)
				{
					RemoveBuffDebuff(BuffsAndDebuffs.SLEEPING, 0.0f, 0.0f, null);
				}
			}
			if(adj > 0)
			{
				floatDmg.GetComponent<FloatingDamage>().color = Color.green;
				floatDmg.GetComponent<FloatingDamage>().amount = adj.ToString();
				floatDmg.GetComponent<FloatingDamage>().crit = critical;
			}
			if(curHP < 0){
				curHP = 0;
			}
			if(curHP > curMaxHP){
				curHP = curMaxHP;
			}
			if(curHP == 0)
			{
				StartCoroutine(Faint());
			}
		}
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
		}
		overHeadInfo.GetComponent<OverHeadInformation>().AdjustHP((float)curHP, (float)curMaxHP);
	}
	[RPC]
	public void AdjustPP(int adj, string currentOrMax, int attacker)
	{
		if(currentOrMax == "max")
		{
			curMaxPP += adj;
			GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
			if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
			{
				PokemonToGiveEXPTo.Add(attacker);
			}
			if(curMaxPP < 0){
				curMaxPP = 0;
			}
			if(curMaxPP > maxPP){
				curMaxPP = maxPP;
			}
		}
		if(currentOrMax == "current")
		{
			curPP += adj;
			GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
			if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
			{
				PokemonToGiveEXPTo.Add(attacker);
			}
			if(curPP < 0){
				curPP = 0;
			}
			if(curPP > curMaxPP){
				curPP = curMaxPP;
			}
		}
	}
	[RPC]
	public void AddStatusEffect(BuffsAndDebuffs buffAndDebuff, float percentage, float duration, int attacker)
	{
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		StatusEffect effect = new StatusEffect(buffAndDebuff, percentage, duration);
		hud.playerPokemonPortrait.SpawnStatusEffectIcon(effect);
		AddBuffDebuff(buffAndDebuff, percentage, duration, attackingPokemon);
		StatusEffect.Add(effect);
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
		}
	}
	public void AddBuffDebuff(BuffsAndDebuffs buffOrDebuff, float percentage, float duration, GameObject attacker)
	{
		if(buffOrDebuff == BuffsAndDebuffs.NONE)
			return;
		if(buffOrDebuff == BuffsAndDebuffs.ACC_DWN)
		{
			accuracy = accuracy - percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ACC_UP)
		{
			accuracy = accuracy + percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.EVA_DWN)
		{
			evasion = evasion - percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.EVA_UP)
		{
			evasion = evasion + percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ATK_DWN)
		{
			curATK = (int)(curATK - ((float)maxATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.ATK_UP)
		{
			curATK = (int)(curATK + ((float)maxATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.DEF_DWN)
		{
			curDEF = (int)(curDEF - ((float)maxDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.DEF_UP)
		{
			curDEF = (int)(curDEF + ((float)maxDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPATK_DWN)
		{
			curSPATK = (int)(curSPATK - ((float)maxSPATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPATK_UP)
		{
			curSPATK = (int)(curSPATK + ((float)maxSPATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPDEF_DWN)
		{
			curSPDEF = (int)(curSPDEF - ((float)maxSPDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPDEF_UP)
		{
			curSPDEF = (int)(curSPDEF + ((float)maxSPDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPD_DWN)
		{
			curSPD = (int)(curSPD - ((float)maxSPD * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPD_UP)
		{
			curSPD = (int)(curSPD + ((float)maxSPD * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.BURNED)
		{
			burned = true;
		}
		if(buffOrDebuff == BuffsAndDebuffs.FROZEN)
		{
			frozen = true;
		}
		if(buffOrDebuff == BuffsAndDebuffs.PARALYZED)
		{
			paralyzed = true;
		}
		if(buffOrDebuff == BuffsAndDebuffs.POISONED)
		{
			poisoned = true;
		}
		if(buffOrDebuff == BuffsAndDebuffs.SEEDED)
		{
			seeded = true;
			seededBy = attacker;
		}
		if(buffOrDebuff == BuffsAndDebuffs.SLEEPING)
		{
			sleeping = true;
			Sleep();
		}
	}
	public void RemoveBuffDebuff(BuffsAndDebuffs buffOrDebuff, float percentage, float duration, GameObject attacker)
	{
		if(buffOrDebuff == BuffsAndDebuffs.ACC_DWN)
		{
			accuracy = accuracy + percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ACC_UP)
		{
			accuracy = accuracy - percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ACC_DWN)
		{
			evasion = evasion + percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ACC_UP)
		{
			evasion = evasion - percentage;
		}
		if(buffOrDebuff == BuffsAndDebuffs.ATK_DWN)
		{
			curATK = (int)(curATK + ((float)maxATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.ATK_UP)
		{
			curATK = (int)(curATK - ((float)maxATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.DEF_DWN)
		{
			curDEF = (int)(curDEF + ((float)maxDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.DEF_UP)
		{
			curDEF = (int)(curDEF - ((float)maxDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPATK_DWN)
		{
			curSPATK = (int)(curSPATK + ((float)maxSPATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPATK_UP)
		{
			curSPATK = (int)(curSPATK - ((float)maxSPATK * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPDEF_DWN)
		{
			curSPDEF = (int)(curSPDEF + ((float)maxSPDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPDEF_UP)
		{
			curSPDEF = (int)(curSPDEF - ((float)maxSPDEF * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPD_DWN)
		{
			curSPD = (int)(curSPD + ((float)maxSPD * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.SPD_UP)
		{
			curSPD = (int)(curSPD - ((float)maxSPD * percentage));
		}
		if(buffOrDebuff == BuffsAndDebuffs.BURNED)
		{
			burned = false;
		}
		if(buffOrDebuff == BuffsAndDebuffs.FROZEN)
		{
			frozen = false;
		}
		if(buffOrDebuff == BuffsAndDebuffs.PARALYZED)
		{
			paralyzed = false;
		}
		if(buffOrDebuff == BuffsAndDebuffs.POISONED)
		{
			poisoned = false;
		}
		if(buffOrDebuff == BuffsAndDebuffs.SEEDED)
		{
			seeded = false;
			seededBy = null;
		}
		if(buffOrDebuff == BuffsAndDebuffs.SLEEPING)
		{
			sleeping = false;
			WakeUp();
		}
	}
	[RPC]
	public void AdjustCurrentEXP(bool faintedIsCaptured, int faintedBaseEXP, int faintedLevel)
	{
		bool luckyEgg = false;
		if(equippedItem.name == "Lucky Egg")
		{
			luckyEgg = true;
		}
		int increase = increaseEXPScript.AddExperience(faintedIsCaptured, isFromTrade, faintedBaseEXP, luckyEgg, faintedLevel, level, evolveLevel);
		StartCoroutine(IncreaseEXP(increase));

	}

	[RPC]
	public void SetDead()
	{
		isAlive = false;
		timeOfDeath = Time.time;
		//_ReSpawner.deadPokemon.Add(this);
		foreach(Component c in GetComponent<WildPokemonAI>().ThingsToDisable)
		{
			c.gameObject.SetActive(false);
		}
	}
	
	private void RegeneratePP()
	{
		int increase = (int)((float)curMaxPP * 0.05f);
		if(increase > 10)
			increase = 10;
		curPP += increase;
		if(curPP > curMaxPP)
			curPP = curMaxPP;
	}
	#region SETUP
	[RPC]
	public void SetupPokemonFirstTime()
	{
		Enemies = new List<GameObject>();
		PokemonToGiveEXPTo = new List<int>();
		SetupIV();
		SetupShininess();
		SetupGender();
		SetupNature();
		SetupNewStats();
		SetupMoves();
		isAlive = true;
		isSetup = true;
		overHeadInfo.SetActive(true);
	}
	[RPC]
	public void SetupSetupPokemon()
	{
		Enemies = new List<GameObject>();
		PokemonToGiveEXPTo = new List<int>();
		SetupExistingStats();
		SetupMoves();
		isAlive = true;
		isSetup = true;
		overHeadInfo.SetActive(true);
	}
	private void SetupIV()
	{
		hpIV = UnityEngine.Random.Range(0,32);
		atkIV = UnityEngine.Random.Range(0,32);
		defIV = UnityEngine.Random.Range(0,32);
		spatkIV = UnityEngine.Random.Range(0,32);
		spdefIV = UnityEngine.Random.Range(0,32);
		spdIV = UnityEngine.Random.Range(0,32);
	}
	private void SetupShininess()
	{
		if(defIV == 10 && spdIV == 10 && spatkIV == 10)
		{
			if(atkIV == 2 || atkIV == 3 || atkIV == 6 || atkIV == 7 || atkIV == 10 || atkIV == 11 || atkIV == 14 || atkIV == 15){
				isShiny = true;
			}
		}
		if(isShiny)
		{
			Renderer[] rendererArray = GetComponentsInChildren<Renderer>();
			for(int r = 0; r < rendererArray.Length; r++){
				string materialName = rendererArray[r].material.name;
				materialName = materialName.Substring(0, materialName.Length - 11);
				rendererArray[r].material = Resources.Load<Material>("Models/Pokemon/Materials/" + materialName + "S");
			}
		}
	}
	private void SetupGender()
	{
		if(atkIV > genderRatio)
		{
			gender = Genders.MALE;
		}
		else if(atkIV <= genderRatio)
		{
			gender = Genders.FEMALE;
		}
		if(genderRatio == 0)
			gender = Genders.NONE;
	}
	private void SetupNature()
	{
		System.Array natures = System.Enum.GetValues (typeof(Natures));
		nature = (Natures)natures.GetValue (UnityEngine.Random.Range(0,24));
	}
	private void SetupNewStats()
	{
		maxHP = statCalculationsScript.CalculateHP (baseHP, level, hpIV, hpEV);
		curMaxHP = maxHP;
		maxPP = statCalculationsScript.CalculatePP (basePP, level);
		curMaxPP = maxPP;
		maxATK = statCalculationsScript.CalculateStat (baseATK, level, atkIV, atkEV, nature, StatCalculations.StatTypes.ATTACK);
		maxDEF = statCalculationsScript.CalculateStat (baseDEF, level, defIV, defEV, nature, StatCalculations.StatTypes.DEFENSE);
		maxSPATK = statCalculationsScript.CalculateStat (baseSPATK, level, spatkIV, spatkEV, nature, StatCalculations.StatTypes.SPECIALATTACK);
		maxSPDEF = statCalculationsScript.CalculateStat (baseSPDEF, level, spdefIV, spdefEV, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
		maxSPD = statCalculationsScript.CalculateStat (baseSPD, level, spdIV, spdEV, nature, StatCalculations.StatTypes.SPEED);
		curHP = curMaxHP;
		curPP = curMaxPP;
		curATK = maxATK;
		curDEF = maxDEF;
		curSPATK = maxSPATK;
		curSPDEF = maxSPDEF;
		curSPD = maxSPD;
		evasion = 1.0f;
		accuracy = 1.0f;
		lastRequiredEXP = calculateEXPScript.CalculateCurrentXP(level - 1, levelingRate);
		currentEXP = calculateEXPScript.CalculateCurrentXP(level, levelingRate);
		nextRequiredEXP = calculateEXPScript.CalculateRequiredXP(level, levelingRate);
	}
	private void SetupExistingStats()
	{
		maxHP = statCalculationsScript.CalculateHP (baseHP, level, hpIV, hpEV);
		curMaxHP = maxHP;
		maxPP = statCalculationsScript.CalculatePP (basePP, level);
		curMaxPP = maxPP;
		maxATK = statCalculationsScript.CalculateStat (baseATK, level, atkIV, atkEV, nature, StatCalculations.StatTypes.ATTACK);
		maxDEF = statCalculationsScript.CalculateStat (baseDEF, level, defIV, defEV, nature, StatCalculations.StatTypes.DEFENSE);
		maxSPATK = statCalculationsScript.CalculateStat (baseSPATK, level, spatkIV, spatkEV, nature, StatCalculations.StatTypes.SPECIALATTACK);
		maxSPDEF = statCalculationsScript.CalculateStat (baseSPDEF, level, spdefIV, spdefEV, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
		maxSPD = statCalculationsScript.CalculateStat (baseSPD, level, spdIV, spdEV, nature, StatCalculations.StatTypes.SPEED);
		curPP = curMaxPP;
		curATK = maxATK;
		curDEF = maxDEF;
		curSPATK = maxSPATK;
		curSPDEF = maxSPDEF;
		curSPD = maxSPD;
		evasion = 1.0f;
		accuracy = 1.0f;
		lastRequiredEXP = calculateEXPScript.CalculateCurrentXP(level - 1, levelingRate);
		nextRequiredEXP = calculateEXPScript.CalculateRequiredXP(level, levelingRate);
	}
	private void SetupMoves()
	{
		MovesToLearn = new List<Move>();
		KnownMoves = new List<Move>();
		foreach(string name in MovesToLearnNames)
		{
			MovesToLearn.Add(GetComponent(name) as Move);
		}
		foreach(string name in KnownMovesNames)
		{
			KnownMoves.Add(GetComponent(name) as Move);
		}
		List<Move> TempList = new List<Move>();
		foreach(Move move in MovesToLearn)
		{
			if(level >= move.levelLearned)
			{
				if(!KnownMoves.Contains(move))
				{
					KnownMovesNames.Add(move.moveName.ToString().Replace(" ","_"));
					KnownMoves.Add(move);
					TempList.Add(move);
				}
				else
				{
					TempList.Add(move);
				}
			}
		}
		foreach(Move move in TempList)
		{
			if(MovesToLearn.Contains(move))
			{
				MovesToLearnNames.Remove(move.moveName.ToString().Replace(" ","_"));
				MovesToLearn.Remove(move);
			}
		}

	}
	#endregion


	private IEnumerator IncreaseEXP(int increase)
	{
		int target = currentEXP + increase;
		while(currentEXP != target)
		{
			currentEXP = (int)Mathf.Lerp(currentEXP, target, Time.deltaTime);
			if(currentEXP >= nextRequiredEXP)
			{
				level += 1;
				lastRequiredEXP = nextRequiredEXP;
				nextRequiredEXP = calculateEXPScript.CalculateRequiredXP(level, levelingRate);
				maxHP = statCalculationsScript.CalculateHP (baseHP, level, hpIV, hpEV);
				curMaxHP = maxHP;
				maxPP = statCalculationsScript.CalculatePP (basePP, level);
				curMaxPP = maxPP;
				maxATK = statCalculationsScript.CalculateStat (baseATK, level, atkIV, atkEV, nature, StatCalculations.StatTypes.ATTACK);
				maxDEF = statCalculationsScript.CalculateStat (baseDEF, level, defIV, defEV, nature, StatCalculations.StatTypes.DEFENSE);
				maxSPATK = statCalculationsScript.CalculateStat (baseSPATK, level, spatkIV, spatkEV, nature, StatCalculations.StatTypes.SPECIALATTACK);
				maxSPDEF = statCalculationsScript.CalculateStat (baseSPDEF, level, spdefIV, spdefEV, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
				maxSPD = statCalculationsScript.CalculateStat (baseSPD, level, spdIV, spdEV, nature, StatCalculations.StatTypes.SPEED);
				SetupMoves();
				if(level == evolveLevel && !evolving)
				{
					evolving = true;
					StartCoroutine(Evolve());
				}
			}
			yield return null;
		}
	}
	private IEnumerator Faint()
	{
		GetComponent<PokemonInput>().enabled = false;
		GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Dead;
		anim.SetBool("Faint", true);
		foreach(int pokemon in PokemonToGiveEXPTo)
		{
			GameObject thePokemon = PhotonView.Find(pokemon).gameObject;
			if(thePokemon.GetComponent<PhotonView>().owner == PhotonNetwork.player)
				thePokemon.GetComponent<PhotonView>().RPC("AdjustCurrentEXP", PhotonTargets.AllBuffered, isCaptured, baseEXPYield, level);
		}
		yield return new WaitForSeconds(3.0f);
		if(!isCaptured)
		{
			GetComponent<PhotonView>().RPC("SetDead", PhotonTargets.AllBuffered);
			target.gameObject.GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
			GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
		}
		else
		{
			GetComponent<PokemonInput>().SwapToPlayer();
		}
		yield return null;
	}
	private IEnumerator Flinching()
	{
		anim.SetBool("Default", true);
		yield return new WaitForSeconds(3.0f);
		anim.SetBool("Default", false);
	}
	#region EVOLUTION
	private IEnumerator Evolve()
	{
		GetComponent<PokemonInput>().enabled = false;
		animation.Play("Default");
		while(animation.isPlaying)
		{
			yield return null;
		}
		foreach(Material material in mesh.GetComponent<SkinnedMeshRenderer>().materials)
		{
			StartCoroutine(ChangeToWhite(material));
		}
		yield return new WaitForSeconds(1);
		animation.Play("Evolve");
		while(animation.isPlaying)
		{
			yield return null;
		}
		StartCoroutine(IncreaseFlare());
		while(evolveFalre.brightness < evolutionFlareSize - 1f)
		{
			yield return null;
		}
		GameObject evolved_form = PhotonNetwork.Instantiate(evolvesInto, transform.position, transform.rotation, 0) as GameObject;
		Material[] evolved_materials = evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().materials;
		evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		foreach(Material material in evolved_materials)
		{
			material.SetFloat("_Blend", 1f);
		}
		evolved_form.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		int poke = evolved_form.GetComponent<PhotonView>().viewID;
		GetComponent<PhotonView>().RPC("SetActivePokemon", PhotonTargets.AllBuffered, poke);
		GetComponent<PokemonInput>().myCamera.GetComponent<CameraController>().SetTarget(evolved_form.transform);
		SkinnedMeshRenderer componenets = GetComponentInChildren<SkinnedMeshRenderer>();
		componenets.enabled = false;
		while(evolveFalre.brightness > 0f){
			yield return null;
		}
		foreach(Material material in evolved_materials){
			StartCoroutine(ChangeToColor(material));
		}
		yield return new WaitForSeconds(1);
		evolved_form.GetComponent<Pokemon>().GiveStatsToEvolvedForm(isSetup, isCaptured, trainer, trainersName, nickName, isFromTrade, level, gender, nature,
		                                                            hpIV, atkIV, defIV, spatkIV, spdefIV, spdIV, hpEV, atkEV, defEV, spatkEV, spdefEV,
		                                                            spdEV, KnownMovesNames, lastMoveUsed, equippedItem, isInBattle, origin, isShiny);
		evolved_form.GetComponent<Pokemon>().SetupSetupPokemon();
		evolved_form.GetComponent<PokemonInput>().enabled = true;
		PhotonNetwork.Destroy(gameObject);
		yield return null;
	}
	private IEnumerator ChangeToWhite(Material mat)
	{
		float counter = mat.GetFloat("_Blend");
		while(counter != 1f){
			float increase = mat.GetFloat("_Blend") + Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter += increase;
			yield return null;
		}
	}
	private IEnumerator IncreaseFlare()
	{
		float increase = flareGrowDelay + Time.deltaTime;
		while(evolveFalre.brightness < evolutionFlareSize){
			evolveFalre.brightness = evolveFalre.brightness + increase;
			yield return null;
		}
		float decrease = flareDieDelay + Time.deltaTime;
		while(evolveFalre.brightness > 0f){
			evolveFalre.brightness = evolveFalre.brightness - decrease;
			yield return null;
		}
	}
	private IEnumerator ChangeToColor(Material mat)
	{
		float counter = mat.GetFloat("_Blend");
		while(counter != 0f){
			float increase = mat.GetFloat("_Blend") - Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter -= increase;
			yield return null;
		}
	}
	private void GiveStatsToEvolvedForm(bool thisIsSetup, bool thisIsCaptured, GameObject thisTrainer, string thisTrainersName, string thisNickName,
	                                    bool thisIsFromTrade, int thisLevel, Genders thisGender, Natures thisNature, int thisHPIV, int thisATKIV,
	                                    int thisDEFIV, int thisSPATKIV, int thisSPDEFIV, int thisSPDIV, int thisHPEV, int thisATKEV, int thisDEFEV,
	                                    int thisSPATKEV, int thisSPDEFEV, int thisSPDEV, List<string> ThisKnownMoves, Move thisLastMoveUsed,
	                                    _Item thisEquippedItem, bool thisIsInBattle, int thisOrigin, bool thisIsShiny)
	{
		isSetup = thisIsSetup;
		isCaptured = thisIsCaptured;
		trainer = thisTrainer;
		trainersName = thisTrainersName;
		nickName = thisNickName;
		isFromTrade = thisIsFromTrade;
		level = thisLevel;
		gender = thisGender;
		nature = thisNature;
		hpIV = thisHPIV;
		atkIV = thisATKIV;
		defIV = thisDEFIV;
		spatkIV = thisSPATKIV;
		spdefIV = thisSPDEFIV;
		spdIV = thisSPDIV;
		hpEV = thisHPEV;
		atkEV = thisATKEV;
		defEV = thisDEFEV;
		spatkEV = thisSPATKEV;
		spdefEV = thisSPDEFEV;
		spdEV = thisSPDEV;
		KnownMovesNames = ThisKnownMoves;
		lastMoveUsed = thisLastMoveUsed;
		equippedItem = thisEquippedItem;
		isInBattle = thisIsInBattle;
		origin = thisOrigin;
		isShiny = thisIsShiny;
	}
	#endregion
}
