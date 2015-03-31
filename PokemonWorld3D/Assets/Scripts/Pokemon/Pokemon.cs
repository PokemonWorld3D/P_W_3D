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
	public int atkStage;
	public int defStage;
	public int spatkStage;
	public int spdefStage;
	public int spdStage;
	public int evasionStage;
	public int accuracyStage;
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
	public StatusConditions statusCondition;
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
	public bool glowing;
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
	public bool takingInSunlight;
	public bool withdrawing;
	public bool whippingUpAWhirlwind;
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
	
	public enum Genders { NONE, FEMALE, MALE }
	public enum Natures { ADAMANT, BASHFUL, BOLD, BRAVE, CALM, CAREFUL, DOCILE, GENTLE, HARDY, HASTY, IMPISH, JOLLY, LAX, LONELY, MILD, MODEST, NAIVE, NAUGHTY,
		QUIET, QUIRKY, RASH, RELAXED, SASSY, SERIOUS, TIMID }
	public enum LevelingRates { ERRATIC, FAST, FLUCTUATING, MEDIUM_FAST, MEDIUM_SLOW, SLOW }
	public enum StatusConditions { NONE, BADLY_POISONED, BURNED, FROZEN, PARALYZED, POISONED, SLEEPING }
	public enum Stats { HITPOINTS, POWERPOINTS, ATTACK, DEFENSE, SPECIALATTACK, SPECIALDEFENSE, SPEED }
	public enum AccEva { EVASION, ACCURACY }
	
	private StatCalculations statCalculationsScript = new StatCalculations();
	private CalculateXP calculateEXPScript = new CalculateXP();
	private IncreaseExperience increaseEXPScript = new IncreaseExperience();
	private bool fainting;
	private bool evolving;
	private Animator anim;
	private Pokemon target;
	#endregion
	
	void Start()
	{
		anim = GetComponent<Animator>();
		InvokeRepeating("RegeneratePP", 1.0f, 1.0f);
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
	public void AdjustCurrentMaxHP(int adj, int attacker)
	{
		curMaxHP += adj;
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
		}
		if(curMaxHP < 0)
		{
			curMaxHP = 0;
		}
		if(curMaxHP > maxHP)
		{
			curMaxHP = maxHP;
		}
	}
	[RPC]
	public void AdjustCurrentHP(int adj, int attacker)
	{
		curHP += adj;
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
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
	[RPC]
	public void AdjustCurrentMaxPP(int adj, int attacker)
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
	[RPC]
	public void AdjustCurrentPP(int adj, int attacker)
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
	[RPC]
	public void AdjustCurrentStat(Stats stat, int adj, int attacker)
	{
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
		}
		if(stat == Stats.ATTACK)
		{
			atkStage += adj;
			if(atkStage < -6){
				atkStage = -6;
			}
			if(atkStage > 6){
				atkStage = 6;
			}
			curATK = ChangeStatTo(atkStage, maxATK);
		}
		if(stat == Stats.DEFENSE)
		{
			defStage += adj;
			if(defStage < -6){
				defStage = -6;
			}
			if(defStage > 6){
				defStage = 6;
			}
			curDEF = ChangeStatTo(defStage, maxDEF);
		}
		if(stat == Stats.SPECIALATTACK)
		{
			spatkStage += adj;
			if(spatkStage < -6){
				spatkStage = -6;
			}
			if(spatkStage > 6){
				spatkStage = 6;
			}
			curSPATK = ChangeStatTo(spatkStage, maxSPATK);
		}
		if(stat == Stats.SPECIALDEFENSE)
		{
			spdefStage += adj;
			if(spdefStage < -6){
				spdefStage = -6;
			}
			if(spdefStage > 6){
				spdefStage = 6;
			}
			curSPDEF = ChangeStatTo(spdefStage, maxSPDEF);
		}
		if(stat == Stats.SPEED)
		{
			spdStage += adj;
			if(spdStage < -6){
				spdStage = -6;
			}
			if(spdStage > 6){
				spdStage = 6;
			}
			curSPD = ChangeStatTo(spdStage, maxSPD);
		}
	}
	[RPC]
	public void AdjustCurrentAccEva(AccEva stat, int adj, int attacker){
		if(stat == AccEva.ACCURACY)
		{
			accuracyStage += adj;
			if(accuracyStage < -6){
				accuracyStage = -6;
			}
			if(accuracyStage > 6){
				accuracyStage = 6;
			}
			accuracy = ChangeAccEvaTo(accuracyStage);
		}
		if(stat == AccEva.EVASION)
		{
			evasionStage += adj;
			if(evasionStage < -6){
				evasionStage = -6;
			}
			if(evasionStage > 6){
				evasionStage = 6;
			}
			evasion = ChangeAccEvaTo(evasionStage);
		}
	}
	[RPC]
	public void AdjustStatusCondition(StatusConditions condition, int attacker)
	{
		GameObject attackingPokemon = PhotonView.Find(attacker).gameObject;
		if(attackingPokemon != gameObject && attackingPokemon.GetComponent<Pokemon>().isCaptured && !PokemonToGiveEXPTo.Contains(attacker))
		{
			PokemonToGiveEXPTo.Add(attacker);
		}
		statusCondition = condition;
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
		if(level == evolveLevel && !evolving)
		{
			evolving = true;
			StartCoroutine(Evolve());
		}
	}
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
		curPP += 1;
		if(curPP > curMaxPP)
			curPP = curMaxPP;
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
	private int ChangeStatTo(int statStage, int maxStat)
	{
		if(statStage <= -6)
		{
			return (int)((float)maxStat * 0.25f);
		}
		else if(statStage == -5)
		{
			return (int)((float)maxStat * 0.2857142857f);
		}
		else if(statStage == -4)
		{
			return (int)((float)maxStat * 0.3333333333f);
		}
		else if(statStage == -3)
		{
			return (int)((float)maxStat * 0.4f);
		}
		else if(statStage == -2)
		{
			return (int)((float)maxStat * 0.5f);
		}
		else if(statStage == -1)
		{
			return (int)((float)maxStat * 0.6666666667f);
		}
		else if(statStage == 1)
		{
			return (int)((float)maxStat * 1.5f);
		}
		else if(statStage == 2)
		{
			return (int)((float)maxStat * 2f);
		}
		else if(statStage == 3)
		{
			return (int)((float)maxStat * 2.5f);
		}
		else if(statStage == 4)
		{
			return (int)((float)maxStat * 3f);
		}
		else if(statStage == 5)
		{
			return (int)((float)maxStat * 3.5f);
		}
		else if(statStage >= 6)
		{
			return (int)((float)maxStat * 4f);
		}
		else
		{
			return (int)((float)maxStat * 1f);
		}	
	}
	private float ChangeAccEvaTo(int statStage)
	{
		if(statStage <= -6)
		{
			return 0.3333333333f;
		}
		else if(statStage == -5)
		{
			return 0.375f;
		}
		else if(statStage == -4)
		{
			return 0.4285714286f;
		}
		else if(statStage == -3)
		{
			return 0.5f;
		}
		else if(statStage == -2)
		{
			return 0.6f;
		}
		else if(statStage == -1)
		{
			return 0.75f;
		}
		else if(statStage == 1)
		{
			return 1.3333333333f;
		}
		else if(statStage == 2)
		{
			return 1.6666666667f;
		}
		else if(statStage == 3)
		{
			return 2f;
		}
		else if(statStage == 4)
		{
			return 2.3333333333f;
		}
		else if(statStage == 5)
		{
			return 2.6666666667f;
		}
		else if(statStage == 6)
		{
			return 3f;
		}else
		{
			return 1f;
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
	private IEnumerator IncreaseEXP(int increase)
	{
		int target = currentEXP + increase;
		while(currentEXP < target)
		{
			currentEXP += 1;
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
			}
			yield return null;
		}
	}
	private IEnumerator Faint()
	{
		GetComponent<WildPokemonAI>().state = WildPokemonAI.State.Dead;
		anim.SetBool("Fainting", true);
		foreach(int pokemon in PokemonToGiveEXPTo)
		{
			GameObject thePokemon = PhotonView.Find(pokemon).gameObject;
			if(thePokemon.GetComponent<PhotonView>().owner == PhotonNetwork.player)
				thePokemon.GetComponent<PhotonView>().RPC("AdjustCurrentEXP", PhotonTargets.AllBuffered, isCaptured, baseEXPYield, level);
		}
		yield return new WaitForSeconds(5.0f);
		if(!isCaptured)
		{
			GetComponent<PhotonView>().RPC("SetDead", PhotonTargets.AllBuffered);
			target.gameObject.GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
			GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
		}
		else
		{
			GetComponent<PokemonInput>().SwapToPlayer();
			target.gameObject.GetComponent<PhotonView>().RPC("EndWildPokemonBattle", PhotonTargets.AllBuffered);
		}
		yield return null;
	}
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
		trainer.GetComponent<PlayerCharacter>().SetActivePokemon(evolved_form);
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
}
