using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Pokemon : MonoBehaviour {
#region Variables
	public bool isAlive;
	public float timeOfDeath;
	public bool isCaptured;
	public int number;
	public string pokemonName;
	public string nickName;
	public string description;
	public bool isFromTrade;
	public int level;
	public int evolveLevel;
	public BasePokemon.TypesList type01;
	public BasePokemon.TypesList type02;
	public BasePokemon.SexesList sex;
	public BasePokemon.NaturesList nature;
	public string ability01;
	public string ability02;
	public int baseHP;
	public int baseATK;
	public int baseDEF;
	public int baseSPATK;
	public int baseSPDEF;
	public int baseSPD;
	public int maxHP;
	public int curMaxHP;
	public int maxATK;
	public int maxDEF;
	public int maxSPATK;
	public int maxSPDEF;
	public int maxSPD;
	public int curHP;
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
	public BasePokemon.LevelingRatesList levelingRate;
	public int lastRequiredXP;
	public int currentXP;
	public int nextRequiredXP;
	public int hpEVYield;
	public int atkEVYield;
	public int defEVYield;
	public int spatkEVYield;
	public int spdefEVYield;
	public int spdEVYield;
	public int baseFriendship;
	public int catchRate;
	public BasePokemon.NonVolatileStatusConditionList statusCondition;
	public int badlyPoisonedTurnCounter;
	public int sleepTurns;
	public bool confusion;
	public int confusionTurns;
	public bool curse;
	public bool embargo;
	public int embargoTurns;
	public bool encore;
	public int encoreTurns;
	public bool flinch;
	public bool healBlock;
	public int healBlockTurns;
	public bool identification;
	public bool infatuation;
	public bool nightmare;
	public bool partiallyTrapped;
	public int partiallyTrappedTurns;
	public bool perishSong;
	public int perishSongTurnCountDown;
	public bool seeding;
	public bool taunt;
	public int tauntTurns;
	public bool telekineticLevitation;
	public int telekineticLevitationTurns;
	public bool torment;
	public bool trapped;
	public bool aquaRing;
	public bool bracing;
	public bool centerOfAttention;
	public bool defenseCurl;
	public bool glowing;
	public bool rooting;
	public bool magicCoat;
	public bool magneticLevitation;
	public int magneticLevitationTurns;
	public bool minimize;
	public bool protection;
	public bool recharging;
	public bool semiInvulnerable;
	public bool substitute;
	public int substituteHP;
	public bool takingAim;
	public bool takingInSunlight;
	public bool withdrawing;
	public bool whippingUpAWhirlwind;
	public List<Move> movesToLearn = new List<Move>();
	public List<Move> pokemonsMoves = new List<Move>();
	public Move lastMoveUsed;
	public Item equippedItem;
	public bool isInBattle;
	public int origin;
	public int genderRatio;
	public bool isShiny;
	public float evolutionFlareSize;
	public GameObject evolvesInto;
	
	private StatCalculations statCalculationsScript = new StatCalculations();
	private CalculateXP calculateXPScript = new CalculateXP();
#endregion

	void Start(){
		isAlive = true;
		nickName = null;
		if(maxHP == 0){
			SetupPokemon();
		}
		confusion = false;
		curse = false;
		embargo = false;
		encore = false;
		flinch = false;
		healBlock = false;
		identification = false;
		infatuation = false;
		nightmare = false;
		partiallyTrapped = false;
		perishSong = false;
		seeding = false;
		taunt = false;
		telekineticLevitation = false;
		torment = false;
		trapped = false;
		aquaRing = false;
		bracing = false;
		centerOfAttention = false;
		defenseCurl = false;
		glowing = false;
		rooting = false;
		magicCoat = false;
		magneticLevitation = false;
		minimize = false;
		protection = false;
		recharging = false;
		semiInvulnerable = false;
		substitute = false;
		takingAim = false;
		takingInSunlight = false;
		withdrawing = false;
		whippingUpAWhirlwind = false;
		isInBattle = false;
	}
	
	private void SetupIV(){
		hpIV = UnityEngine.Random.Range(0,32);
		atkIV = UnityEngine.Random.Range(0,32);
		defIV = UnityEngine.Random.Range(0,32);
		spatkIV = UnityEngine.Random.Range(0,32);
		spdefIV = UnityEngine.Random.Range(0,32);
		spdIV = UnityEngine.Random.Range(0,32);
 	}
	private void ChoosePokemonSex(){
		if(atkIV > genderRatio){
			sex = BasePokemon.SexesList.MALE;
		}else if(atkIV <= genderRatio){
			sex = BasePokemon.SexesList.FEMALE;
		}
   	}
	private void ChoosePokemonNature(){
		System.Array natures = System.Enum.GetValues (typeof(BasePokemon.NaturesList));
		nature = (BasePokemon.NaturesList)natures.GetValue (UnityEngine.Random.Range(0,24));
	}
	private void SetupStats(){
		maxHP = statCalculationsScript.CalculateHP (baseHP, level, hpIV, hpEV);
		curMaxHP = maxHP;
		maxATK = statCalculationsScript.CalculateStat (baseATK, level, atkIV, atkEV, nature, StatCalculations.StatTypes.ATTACK);
		maxDEF = statCalculationsScript.CalculateStat (baseDEF, level, defIV, defEV, nature, StatCalculations.StatTypes.DEFENSE);
		maxSPATK = statCalculationsScript.CalculateStat (baseSPATK, level, spatkIV, spatkEV, nature, StatCalculations.StatTypes.SPECIALATTACK);
		maxSPDEF = statCalculationsScript.CalculateStat (baseSPDEF, level, spdefIV, spdefEV, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
		maxSPD = statCalculationsScript.CalculateStat (baseSPD, level, spdIV, spdEV, nature, StatCalculations.StatTypes.SPEED);
		curHP = maxHP;
		curATK = maxATK;
		curDEF = maxDEF;
		curSPATK = maxSPATK;
		curSPDEF = maxSPDEF;
		curSPD = maxSPD;
		evasion = 1.0f;
		accuracy = 1.0f;
		lastRequiredXP = calculateXPScript.CalculateCurrentXP(level - 1, levelingRate);
		currentXP = calculateXPScript.CalculateCurrentXP(level, levelingRate);
		nextRequiredXP = calculateXPScript.CalculateRequiredXP(level, levelingRate);
	}

	public void SetupPokemon(){
		SetupIV();
		DetermineShininess();
		ChoosePokemonSex();
		ChoosePokemonNature();
		SetupStats();
		List<Move> tempList = new List<Move>();
		foreach(Move move in movesToLearn){
			if(level >= move.level_learned){
				if(!pokemonsMoves.Contains(move)){
					if(pokemonsMoves.Count == 4){
						pokemonsMoves.RemoveAt(0);
						pokemonsMoves.Add(move);
						tempList.Add(move);
					}else if(pokemonsMoves.Count < 4){
						pokemonsMoves.Add(move);
						tempList.Add(move);
					}
				}
			}
		}
		foreach(Move move in tempList){
			if(movesToLearn.Contains(move)){
				movesToLearn.Remove(move);
			}
		}
	}
	public void DetermineShininess(){
		if(defIV == 10 && spdIV == 10 && spatkIV == 10){
			if(atkIV == 2 || atkIV == 3 || atkIV == 6 || atkIV == 7 || atkIV == 10 || atkIV == 11 || atkIV == 14 || atkIV == 15){
				isShiny = true;
			}
		}else{
			isShiny = false;
		}
		if(isShiny){
			Renderer[] rendererArray = GetComponentsInChildren<Renderer>();
			for(int r = 0; r < rendererArray.Length; r++){
				string materialName = rendererArray[r].material.name;
				materialName = materialName.Substring(0, materialName.Length - 11);
				rendererArray[r].material = Resources.Load<Material>("Models/Pokemon/Materials/" + materialName + "S");
			}
		}
 	}
	public void AdjustCurrentHP(int adj){
		curHP += adj;
		if(curHP < 0){
			curHP = 0;
		}
		if(curHP > curMaxHP){
			curHP = curMaxHP;
		}
 	}
	public string AdjustEXP(int adj){
		string textToReturn;
		currentXP += adj;
		if(currentXP >= nextRequiredXP){
			level += 1;
			textToReturn = pokemonName + " reached level " + level + "!";
			lastRequiredXP = nextRequiredXP;
			nextRequiredXP = calculateXPScript.CalculateRequiredXP(level, levelingRate);
			maxHP = statCalculationsScript.CalculateHP (baseHP, level, hpIV, hpEV);
			maxATK = statCalculationsScript.CalculateStat (baseATK, level, atkIV, atkEV, nature, StatCalculations.StatTypes.ATTACK);
			maxDEF = statCalculationsScript.CalculateStat (baseDEF, level, defIV, defEV, nature, StatCalculations.StatTypes.DEFENSE);
			maxSPATK = statCalculationsScript.CalculateStat (baseSPATK, level, spatkIV, spatkEV, nature,StatCalculations.StatTypes.SPECIALATTACK);
			maxSPDEF = statCalculationsScript.CalculateStat (baseSPDEF, level, spdefIV, spdefEV, nature, StatCalculations.StatTypes.SPECIALDEFENSE);
			maxSPD = statCalculationsScript.CalculateStat (baseSPD, level, spdIV, spdEV, nature, StatCalculations.StatTypes.SPEED);
			List<Move> tempList = new List<Move>();
			foreach(Move move in movesToLearn){
				if(level >= move.level_learned){
					if(!pokemonsMoves.Contains(move)){
						if(pokemonsMoves.Count == 4){
							pokemonsMoves.RemoveAt(0);
							textToReturn += "\n" + pokemonName + " has forgotten " + pokemonsMoves[0].move_name + "...";
							pokemonsMoves.Add(move);
							textToReturn += "\n...and" + pokemonName + " has learned " + move.move_name + "!";
							tempList.Add(move);
						}else if(pokemonsMoves.Count < 4){
							pokemonsMoves.Add(move);
							textToReturn += "\n" + pokemonName + " has learned " + move.move_name + "!";
							tempList.Add(move);
						}
					}
				}
			}
			foreach(Move move in tempList){
				if(movesToLearn.Contains(move)){
					movesToLearn.Remove(move);
				}
			}
			return textToReturn;
		}
		return null;
	}
	public void SetDead(){
		isAlive = false;
		timeOfDeath = Time.time;
		
		ReSpawner.deadPokemon.Add(this);
		
		this.gameObject.SetActive(false);
	}
	private void GiveStatsToEvolvedForm(Pokemon pokemon, Pokemon evolvedForm){
		int origin = pokemon.origin;
		PlayerPokemonData tempPokemon = new PlayerPokemonData(pokemon.isAlive, pokemon.timeOfDeath, pokemon.isCaptured, pokemon.number, pokemon.pokemonName,
		                                                      pokemon.nickName, pokemon.description, pokemon.isFromTrade, pokemon.level, pokemon.evolveLevel,
		                                                      pokemon.type01, pokemon.type02, pokemon.sex, pokemon.nature, pokemon.ability01, pokemon.ability02,
		                                                      pokemon.baseHP, pokemon.baseATK, pokemon.baseDEF, pokemon.baseSPATK, pokemon.baseSPDEF,
		                                                      pokemon.baseSPD, pokemon.maxHP, pokemon.curMaxHP, pokemon.maxATK, pokemon.maxDEF, pokemon.maxSPATK,
		                                                      pokemon.maxSPDEF, pokemon.maxSPD, pokemon.curHP, pokemon.curATK, pokemon.atkStage, pokemon.curDEF,
		                                                      pokemon.defStage, pokemon.curSPATK, pokemon.spatkStage, pokemon.curSPDEF, pokemon.spdefStage,
		                                                      pokemon.curSPD, pokemon.spdStage, pokemon.evasion, pokemon.evasionStage, pokemon.accuracy,
		                                                      pokemon.accuracyStage, pokemon.hpEV, pokemon.atkEV, pokemon.defEV, pokemon.spatkEV, pokemon.spdEV,
		                                                      pokemon.spdEV, pokemon.hpIV, pokemon.atkIV, pokemon.defIV, pokemon.spatkIV, pokemon.spdefIV,
		                                                      pokemon.spdIV, pokemon.baseEXPYield, pokemon.levelingRate, pokemon.lastRequiredXP, pokemon.currentXP,
		                                                      pokemon.nextRequiredXP, pokemon.hpEVYield, pokemon.atkEVYield, pokemon.defEVYield, pokemon.spatkEVYield,
		                                                      pokemon.spdefEVYield, pokemon.spdEVYield, pokemon.baseFriendship, pokemon.catchRate,
		                                                      pokemon.statusCondition, pokemon.badlyPoisonedTurnCounter, pokemon.sleepTurns, pokemon.confusion,
		                                                      pokemon.confusionTurns, pokemon.curse, pokemon.embargo, pokemon.embargoTurns, pokemon.encore,
		                                                      pokemon.encoreTurns, pokemon.flinch, pokemon.healBlock, pokemon.healBlockTurns, pokemon.identification,
		                                                      pokemon.infatuation, pokemon.nightmare, pokemon.partiallyTrapped, pokemon.partiallyTrappedTurns,
		                                                      pokemon.perishSong, pokemon.perishSongTurnCountDown, pokemon.seeding, pokemon.taunt,
		                                                      pokemon.tauntTurns, pokemon.telekineticLevitation, pokemon.telekineticLevitationTurns,
		                                                      pokemon.torment, pokemon.trapped, pokemon.aquaRing, pokemon.bracing, pokemon.centerOfAttention,
		                                                      pokemon.defenseCurl, pokemon.glowing, pokemon.rooting, pokemon.magicCoat, pokemon.magneticLevitation,
		                                                      pokemon.magneticLevitationTurns, pokemon.minimize, pokemon.protection, pokemon.recharging,
		                                                      pokemon.semiInvulnerable, pokemon.substitute, pokemon.substituteHP, pokemon.takingAim,
		                                                      pokemon.takingInSunlight, pokemon.withdrawing, pokemon.whippingUpAWhirlwind, pokemon.movesToLearn,
		                                                      pokemon.pokemonsMoves, pokemon.lastMoveUsed, pokemon.equippedItem, pokemon.isInBattle, pokemon.origin,
		                                                      pokemon.genderRatio, pokemon.isShiny);
		#region Give Stats To The Pokemon
		evolvedForm.isAlive = tempPokemon.isAlive;
		evolvedForm.timeOfDeath = tempPokemon.timeOfDeath;
		evolvedForm.isCaptured = tempPokemon.isCaptured;
		evolvedForm.nickName = tempPokemon.nickName;
		evolvedForm.isFromTrade = tempPokemon.isFromTrade;
		evolvedForm.level = tempPokemon.level;
		evolvedForm.sex = tempPokemon.sex;
		evolvedForm.nature = tempPokemon.nature;
		evolvedForm.maxHP = tempPokemon.maxHP;
		evolvedForm.curMaxHP = tempPokemon.curMaxHP;
		evolvedForm.maxATK = tempPokemon.maxATK;
		evolvedForm.maxDEF = tempPokemon.maxDEF;
		evolvedForm.maxSPATK = tempPokemon.maxSPATK;
		evolvedForm.maxSPDEF = tempPokemon.maxSPDEF;
		evolvedForm.maxSPD = tempPokemon.maxSPD;
		evolvedForm.curHP = tempPokemon.curHP;
		evolvedForm.curATK = tempPokemon.curATK;
		evolvedForm.atkStage = tempPokemon.atkStage;
		evolvedForm.curDEF = tempPokemon.curDEF;
		evolvedForm.defStage = tempPokemon.defStage;
		evolvedForm.curSPATK = tempPokemon.curSPATK;
		evolvedForm.spatkStage = tempPokemon.spatkStage;
		evolvedForm.curSPDEF = tempPokemon.curSPDEF;
		evolvedForm.spdefStage = tempPokemon.spdefStage;
		evolvedForm.curSPD = tempPokemon.curSPD;
		evolvedForm.spdStage = tempPokemon.spdStage;
		evolvedForm.evasion = tempPokemon.evasion;
		evolvedForm.evasionStage = tempPokemon.evasionStage;
		evolvedForm.accuracy = tempPokemon.accuracy;
		evolvedForm.accuracyStage = tempPokemon.accuracyStage;
		evolvedForm.hpEV = tempPokemon.hpEV;
		evolvedForm.atkEV = tempPokemon.atkEV;
		evolvedForm.defEV = tempPokemon.defEV;
		evolvedForm.spatkEV = tempPokemon.spatkEV;
		evolvedForm.spdefEV = tempPokemon.spdefEV;
		evolvedForm.spdEV = tempPokemon.spdEV;
		evolvedForm.hpIV = tempPokemon.hpIV;
		evolvedForm.atkIV = tempPokemon.atkIV;
		evolvedForm.defIV = tempPokemon.defIV;
		evolvedForm.spatkIV = tempPokemon.spatkIV;
		evolvedForm.spdefIV = tempPokemon.spdefIV;
		evolvedForm.spdIV = tempPokemon.spdIV;
		evolvedForm.lastRequiredXP = tempPokemon.lastRequiredXP;
		evolvedForm.currentXP = tempPokemon.currentXP;
		evolvedForm.statusCondition = tempPokemon.statusCondition;
		evolvedForm.badlyPoisonedTurnCounter = tempPokemon.badlyPoisonedTurnCounter;
		evolvedForm.sleepTurns = tempPokemon.sleepTurns;
		evolvedForm.confusion = tempPokemon.confusion;
		evolvedForm.confusionTurns = tempPokemon.confusionTurns;
		evolvedForm.curse = tempPokemon.curse;
		evolvedForm.embargo = tempPokemon.embargo;
		evolvedForm.embargoTurns = tempPokemon.embargoTurns;
		evolvedForm.encore = tempPokemon.encore;
		evolvedForm.encoreTurns = tempPokemon.encoreTurns;
		evolvedForm.flinch = tempPokemon.flinch;
		evolvedForm.healBlock = tempPokemon.healBlock;
		evolvedForm.healBlockTurns = tempPokemon.healBlockTurns;
		evolvedForm.identification = tempPokemon.identification;
		evolvedForm.infatuation = tempPokemon.infatuation;
		evolvedForm.nightmare = tempPokemon.nightmare;
		evolvedForm.partiallyTrapped = tempPokemon.partiallyTrapped;
		evolvedForm.partiallyTrappedTurns = tempPokemon.partiallyTrappedTurns;
		evolvedForm.perishSong = tempPokemon.perishSong;
		evolvedForm.perishSongTurnCountDown = tempPokemon.perishSongTurnCountDown;
		evolvedForm.seeding = tempPokemon.seeding;
		evolvedForm.taunt = tempPokemon.taunt;
		evolvedForm.tauntTurns = tempPokemon.tauntTurns;
		evolvedForm.telekineticLevitation = tempPokemon.telekineticLevitation;
		evolvedForm.telekineticLevitationTurns = tempPokemon.telekineticLevitationTurns;
		evolvedForm.torment = tempPokemon.torment;
		evolvedForm.trapped = tempPokemon.trapped;
		evolvedForm.aquaRing = tempPokemon.aquaRing;
		evolvedForm.bracing = tempPokemon.bracing;
		evolvedForm.centerOfAttention = tempPokemon.centerOfAttention;
		evolvedForm.defenseCurl = tempPokemon.defenseCurl;
		evolvedForm.glowing = tempPokemon.glowing;
		evolvedForm.rooting = tempPokemon.rooting;
		evolvedForm.magicCoat = tempPokemon.magicCoat;
		evolvedForm.magneticLevitation = tempPokemon.magneticLevitation;
		evolvedForm.magneticLevitationTurns = tempPokemon.magneticLevitationTurns;
		evolvedForm.minimize = tempPokemon.minimize;
		evolvedForm.protection = tempPokemon.protection;
		evolvedForm.recharging = tempPokemon.recharging;
		evolvedForm.semiInvulnerable = tempPokemon.semiInvulnerable;
		evolvedForm.substitute = tempPokemon.substitute;
		evolvedForm.substituteHP = tempPokemon.substituteHP;
		evolvedForm.takingAim = tempPokemon.takingAim;
		evolvedForm.takingInSunlight = tempPokemon.takingInSunlight;
		evolvedForm.withdrawing = tempPokemon.withdrawing;
		evolvedForm.whippingUpAWhirlwind = tempPokemon.whippingUpAWhirlwind;
		evolvedForm.pokemonsMoves = tempPokemon.pokemonsMoves;
		evolvedForm.lastMoveUsed = tempPokemon.lastMoveUsed;
		evolvedForm.equippedItem = tempPokemon.equippedItem;
		evolvedForm.isInBattle = tempPokemon.isInBattle;
		evolvedForm.origin = tempPokemon.origin;
		evolvedForm.isShiny = tempPokemon.isShiny;
		#endregion
		#region Give Stats To The Roster
		PlayersPokemon roster = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayersPokemon>();
		roster.pokemonRoster[origin].isAlive = evolvedForm.isAlive;
		roster.pokemonRoster[origin].timeOfDeath = evolvedForm.timeOfDeath;
		roster.pokemonRoster[origin].isCaptured = evolvedForm.isCaptured;
		roster.pokemonRoster[origin].number = evolvedForm.number;
		roster.pokemonRoster[origin].pokemonName = evolvedForm.pokemonName;
		roster.pokemonRoster[origin].nickName = evolvedForm.nickName;
		roster.pokemonRoster[origin].description = evolvedForm.description;
		roster.pokemonRoster[origin].isFromTrade = evolvedForm.isFromTrade;
		roster.pokemonRoster[origin].level = evolvedForm.level;
		roster.pokemonRoster[origin].evolveLevel = evolvedForm.evolveLevel;
		roster.pokemonRoster[origin].type01 = evolvedForm.type01;
		roster.pokemonRoster[origin].type02 = evolvedForm.type02;
		roster.pokemonRoster[origin].sex = evolvedForm.sex;
		roster.pokemonRoster[origin].nature = evolvedForm.nature;
		roster.pokemonRoster[origin].ability01 = evolvedForm.ability01;
		roster.pokemonRoster[origin].ability02 = evolvedForm.ability02;
		roster.pokemonRoster[origin].baseHP = evolvedForm.baseHP;
		roster.pokemonRoster[origin].baseATK = evolvedForm.baseATK;
		roster.pokemonRoster[origin].baseDEF = evolvedForm.baseDEF;
		roster.pokemonRoster[origin].baseSPATK = evolvedForm.baseSPATK;
		roster.pokemonRoster[origin].baseSPDEF = evolvedForm.baseSPDEF;
		roster.pokemonRoster[origin].baseSPD = evolvedForm.baseSPD;
		roster.pokemonRoster[origin].maxHP = evolvedForm.maxHP;
		roster.pokemonRoster[origin].curMaxHP = evolvedForm.curMaxHP;
		roster.pokemonRoster[origin].maxATK = evolvedForm.maxATK;
		roster.pokemonRoster[origin].maxDEF = evolvedForm.maxDEF;
		roster.pokemonRoster[origin].maxSPATK = evolvedForm.maxSPATK;
		roster.pokemonRoster[origin].maxSPDEF = evolvedForm.maxSPDEF;
		roster.pokemonRoster[origin].maxSPD = evolvedForm.maxSPD;
		roster.pokemonRoster[origin].curHP = evolvedForm.curHP;
		roster.pokemonRoster[origin].curATK = evolvedForm.curATK;
		roster.pokemonRoster[origin].atkStage = evolvedForm.atkStage;
		roster.pokemonRoster[origin].curDEF = evolvedForm.curDEF;
		roster.pokemonRoster[origin].defStage = evolvedForm.defStage;
		roster.pokemonRoster[origin].curSPATK = evolvedForm.curSPATK;
		roster.pokemonRoster[origin].spatkStage = evolvedForm.spatkStage;
		roster.pokemonRoster[origin].curSPDEF = evolvedForm.curSPDEF;
		roster.pokemonRoster[origin].spdefStage = evolvedForm.spdefStage;
		roster.pokemonRoster[origin].curSPD = evolvedForm.curSPD;
		roster.pokemonRoster[origin].spdStage = evolvedForm.spdStage;
		roster.pokemonRoster[origin].evasion = evolvedForm.evasion;
		roster.pokemonRoster[origin].evasionStage = evolvedForm.evasionStage;
		roster.pokemonRoster[origin].accuracy = evolvedForm.accuracy;
		roster.pokemonRoster[origin].accuracyStage = evolvedForm.accuracyStage;
		roster.pokemonRoster[origin].hpEV = evolvedForm.hpEV;
		roster.pokemonRoster[origin].atkEV = evolvedForm.atkEV;
		roster.pokemonRoster[origin].defEV = evolvedForm.defEV;
		roster.pokemonRoster[origin].spatkEV = evolvedForm.spatkEV;
		roster.pokemonRoster[origin].spdefEV = evolvedForm.spdefEV;
		roster.pokemonRoster[origin].spdEV = evolvedForm.spdEV;
		roster.pokemonRoster[origin].hpIV = evolvedForm.hpIV;
		roster.pokemonRoster[origin].atkIV = evolvedForm.atkIV;
		roster.pokemonRoster[origin].defIV = evolvedForm.defIV;
		roster.pokemonRoster[origin].spatkIV = evolvedForm.spatkIV;
		roster.pokemonRoster[origin].spdefIV = evolvedForm.spdefIV;
		roster.pokemonRoster[origin].spdIV = evolvedForm.spdIV;
		roster.pokemonRoster[origin].baseEXPYield = evolvedForm.baseEXPYield;
		roster.pokemonRoster[origin].levelingRate = evolvedForm.levelingRate;
		roster.pokemonRoster[origin].lastRequiredXP = evolvedForm.lastRequiredXP;
		roster.pokemonRoster[origin].currentXP = evolvedForm.currentXP;
		roster.pokemonRoster[origin].nextRequiredXP = evolvedForm.nextRequiredXP;
		roster.pokemonRoster[origin].hpEVYield = evolvedForm.hpEVYield;
		roster.pokemonRoster[origin].atkEVYield = evolvedForm.atkEVYield;
		roster.pokemonRoster[origin].defEVYield = evolvedForm.defEVYield;
		roster.pokemonRoster[origin].spatkEVYield = evolvedForm.spatkEVYield;
		roster.pokemonRoster[origin].spdefEVYield = evolvedForm.spdefEVYield;
		roster.pokemonRoster[origin].spdEVYield = evolvedForm.spdEVYield;
		roster.pokemonRoster[origin].baseFriendship = evolvedForm.baseFriendship;
		roster.pokemonRoster[origin].catchRate = evolvedForm.catchRate;
		roster.pokemonRoster[origin].statusCondition = evolvedForm.statusCondition;
		roster.pokemonRoster[origin].badlyPoisonedTurnCounter = evolvedForm.badlyPoisonedTurnCounter;
		roster.pokemonRoster[origin].sleepTurns = evolvedForm.sleepTurns;
		roster.pokemonRoster[origin].confusion = evolvedForm.confusion;
		roster.pokemonRoster[origin].confusionTurns = evolvedForm.confusionTurns;
		roster.pokemonRoster[origin].curse = evolvedForm.curse;
		roster.pokemonRoster[origin].embargo = evolvedForm.embargo;
		roster.pokemonRoster[origin].embargoTurns = evolvedForm.embargoTurns;
		roster.pokemonRoster[origin].encore = evolvedForm.encore;
		roster.pokemonRoster[origin].encoreTurns = evolvedForm.encoreTurns;
		roster.pokemonRoster[origin].flinch = evolvedForm.flinch;
		roster.pokemonRoster[origin].healBlock = evolvedForm.healBlock;
		roster.pokemonRoster[origin].healBlockTurns = evolvedForm.healBlockTurns;
		roster.pokemonRoster[origin].identification = evolvedForm.identification;
		roster.pokemonRoster[origin].infatuation = evolvedForm.infatuation;
		roster.pokemonRoster[origin].nightmare = evolvedForm.nightmare;
		roster.pokemonRoster[origin].partiallyTrapped = evolvedForm.partiallyTrapped;
		roster.pokemonRoster[origin].partiallyTrappedTurns = evolvedForm.partiallyTrappedTurns;
		roster.pokemonRoster[origin].perishSong = evolvedForm.perishSong;
		roster.pokemonRoster[origin].perishSongTurnCountDown = evolvedForm.perishSongTurnCountDown;
		roster.pokemonRoster[origin].seeding = evolvedForm.seeding;
		roster.pokemonRoster[origin].taunt = evolvedForm.taunt;
		roster.pokemonRoster[origin].tauntTurns = evolvedForm.tauntTurns;
		roster.pokemonRoster[origin].telekineticLevitation = evolvedForm.telekineticLevitation;
		roster.pokemonRoster[origin].telekineticLevitationTurns = evolvedForm.telekineticLevitationTurns;
		roster.pokemonRoster[origin].torment = evolvedForm.torment;
		roster.pokemonRoster[origin].trapped = evolvedForm.trapped;
		roster.pokemonRoster[origin].aquaRing = evolvedForm.aquaRing;
		roster.pokemonRoster[origin].bracing = evolvedForm.bracing;
		roster.pokemonRoster[origin].centerOfAttention = evolvedForm.centerOfAttention;
		roster.pokemonRoster[origin].defenseCurl = evolvedForm.defenseCurl;
		roster.pokemonRoster[origin].glowing = evolvedForm.glowing;
		roster.pokemonRoster[origin].rooting = evolvedForm.rooting;
		roster.pokemonRoster[origin].magicCoat = evolvedForm.magicCoat;
		roster.pokemonRoster[origin].magneticLevitation = evolvedForm.magneticLevitation;
		roster.pokemonRoster[origin].magneticLevitationTurns = evolvedForm.magneticLevitationTurns;
		roster.pokemonRoster[origin].minimize = evolvedForm.minimize;
		roster.pokemonRoster[origin].protection = evolvedForm.protection;
		roster.pokemonRoster[origin].recharging = evolvedForm.recharging;
		roster.pokemonRoster[origin].semiInvulnerable = evolvedForm.semiInvulnerable;
		roster.pokemonRoster[origin].substitute = evolvedForm.substitute;
		roster.pokemonRoster[origin].substituteHP = evolvedForm.substituteHP;
		roster.pokemonRoster[origin].takingAim = evolvedForm.takingAim;
		roster.pokemonRoster[origin].takingInSunlight = evolvedForm.takingInSunlight;
		roster.pokemonRoster[origin].withdrawing = evolvedForm.withdrawing;
		roster.pokemonRoster[origin].whippingUpAWhirlwind = evolvedForm.whippingUpAWhirlwind;
		roster.pokemonRoster[origin].movesToLearn = evolvedForm.movesToLearn;
		roster.pokemonRoster[origin].pokemonsMoves = evolvedForm.pokemonsMoves;
		roster.pokemonRoster[origin].lastMoveUsed = evolvedForm.lastMoveUsed;
		roster.pokemonRoster[origin].equippedItem = evolvedForm.equippedItem;
		roster.pokemonRoster[origin].isInBattle = evolvedForm.isInBattle;
		roster.pokemonRoster[origin].origin = evolvedForm.origin;
		roster.pokemonRoster[origin].genderRatio = evolvedForm.genderRatio;
		roster.pokemonRoster[origin].isShiny = evolvedForm.isShiny;
		#endregion
	}
	
	public IEnumerator Evolve(){
		Renderer[] renderersArray = this.gameObject.GetComponentsInChildren<Renderer>();
		List<Material> materialsList = new List<Material>();
		foreach(Renderer renderer in renderersArray){
			for(int i = 0; i < renderer.materials.Length; i++){
				if(renderer.materials[i].shader.name == "Toon/Basic Blender"){
					materialsList.Add(renderer.materials[i]);
				}
			}
		}
		GameObject armature = this.gameObject.transform.GetChild(1).gameObject;
		LensFlare evolveFlare = this.gameObject.transform.GetChild(2).gameObject.GetComponent<LensFlare>();
		armature.SetActive(false);
		evolveFlare.enabled = true;
		foreach(Material material in materialsList){
			StartCoroutine(ChangeToWhite(material));
		}
		yield return new WaitForSeconds(1);
		animation.Play(pokemonName + "_Evolve");
		yield return new WaitForSeconds(.5f);
		StartCoroutine(IncreaseFlare(evolveFlare));
		while(animation.isPlaying){
			yield return null;
		}
		GameObject evolvedForm = Instantiate(evolvesInto, this.transform.position, this.transform.rotation) as GameObject;
		Renderer[] evolvedFormsRenderersArray = evolvedForm.gameObject.GetComponentsInChildren<Renderer>();
		List<Material> evolvedFormsMaterialsList = new List<Material>();
		evolvedForm.SetActive(false);
		foreach(Renderer renderer in evolvedFormsRenderersArray){
			for(int i = 0; i < renderer.materials.Length; i++){
				if(renderer.materials[i].shader.name == "Toon/Basic Blender"){
					evolvedFormsMaterialsList.Add(renderer.materials[i]);
				}
			}
		}
		foreach(Material material in evolvedFormsMaterialsList){
			material.SetFloat("_Blend", 1f);
		}
		evolvedForm.SetActive(true);
		GameObject componenets = this.gameObject.transform.GetChild(0).gameObject;
		componenets.SetActive(false);
		while(evolveFlare.brightness > 0f){
			yield return null;
		}
		foreach(Material material in evolvedFormsMaterialsList){
			StartCoroutine(ChangeToColor(material));
		}
		yield return new WaitForSeconds(1);
		GiveStatsToEvolvedForm(this, evolvedForm.GetComponent<Pokemon>());
		GameObject hud = GameObject.FindGameObjectWithTag("HUD");
		GameObject diagBox = hud.gameObject.transform.GetChild(0).GetChild(2).gameObject;
		GameObject diag = diagBox.gameObject.transform.GetChild(0).gameObject;
		Text diagText = diag.GetComponent<Text>();
		diagBox.SetActive(true);
		diag.SetActive(true);
		diagText.text = pokemonName + " has evolved into " + evolvedForm.GetComponent<Pokemon>().pokemonName + "!";
		while(!Input.GetKeyDown(KeyCode.Space)){
			yield return null;
		}
		diagBox.SetActive(false);
		diag.SetActive(false);
		Destroy(this.gameObject);
		yield return null;
	}
	private IEnumerator ChangeToWhite(Material mat){
		float counter = mat.GetFloat("_Blend");
		while(counter != 1f){
			float increase = mat.GetFloat("_Blend") + Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter += increase;
			yield return null;
		}
	}
	private IEnumerator ChangeToColor(Material mat){
		float counter = mat.GetFloat("_Blend");
		while(counter != 0f){
			float increase = mat.GetFloat("_Blend") - Time.deltaTime;
			mat.SetFloat("_Blend", increase);
			counter -= increase;
			yield return null;
		}
	}
	private IEnumerator IncreaseFlare(LensFlare flare){
		float increase = 0.05f + Time.deltaTime;
		while(flare.brightness < 1f){
			flare.brightness = flare.brightness + increase;
			yield return null;
		}
		float decrease = 0.075f + Time.deltaTime;
		while(flare.brightness > 0f){
			flare.brightness = flare.brightness - decrease;
			yield return null;
		}
	}

}
