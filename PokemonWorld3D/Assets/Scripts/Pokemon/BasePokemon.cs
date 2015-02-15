using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml.Serialization;

public class BasePokemon  {

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
	public TypesList type01;
	public TypesList type02;
	public SexesList sex;
	public NaturesList nature;
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
	public int atkStage;
	public int curDEF;
	public int defStage;
	public int curSPATK;
	public int spatkStage;
	public int curSPDEF;
	public int spdefStage;
	public int curSPD;
	public int spdStage;
	public float evasion;
	public int evasionStage;
	public float accuracy;
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
	public LevelingRatesList levelingRate;
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
	public NonVolatileStatusConditionList statusCondition;
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

	public enum TypesList{
//		[XmlEnum("1")]
		BUG,
//		[XmlEnum("2")]
		DARK,
//		[XmlEnum("3")]
		DRAGON,
//		[XmlEnum("4")]
		ELECTRIC,
//		[XmlEnum("5")]
		FAIRY,
//		[XmlEnum("6")]
		FIGHTING,
//		[XmlEnum("7")]
		FIRE,
//		[XmlEnum("8")]
		FLYING,
//		[XmlEnum("9")]
		GHOST,
//		[XmlEnum("10")]
		GRASS,
//		[XmlEnum("11")]
		GROUND,
//		[XmlEnum("12")]
		ICE,
//		[XmlEnum("13")]
		NONE,
//		[XmlEnum("14")]
		NORMAL,
//		[XmlEnum("15")]
		POISON,
//		[XmlEnum("16")]
		PSYCHIC,
//		[XmlEnum("17")]
		ROCK,
//		[XmlEnum("18")]
		STEEL,
//		[XmlEnum("19")]
		WATER
	}
	public enum SexesList{
//		[XmlEnum("1")]
		MALE,
//		[XmlEnum("2")]
		FEMALE,
//		[XmlEnum("3")]
		GENDERLESS,
	}
	public enum NaturesList{
//		[XmlEnum("1")]
		ADAMANT,
//		[XmlEnum("2")]
		BASHFUL,
//		[XmlEnum("3")]
		BOLD,
//		[XmlEnum("4")]
		BRAVE,
//		[XmlEnum("5")]
		CALM,
//		[XmlEnum("6")]
		CAREFUL,
//		[XmlEnum("7")]
		DOCILE,
//		[XmlEnum("8")]
		GENTLE,
//		[XmlEnum("9")]
		HARDY,
//		[XmlEnum("10")]
		HASTY,
//		[XmlEnum("11")]
		IMPISH,
//		[XmlEnum("12")]
		JOLLY,
//		[XmlEnum("13")]
		LAX,
//		[XmlEnum("14")]
		LONELY,
//		[XmlEnum("15")]
		MILD,
//		[XmlEnum("16")]
		MODEST,
//		[XmlEnum("17")]
		NAIVE,
//		[XmlEnum("18")]
		NAUGHTY,
//		[XmlEnum("19")]
		QUIET,
//		[XmlEnum("20")]
		QUIRKY,
//		[XmlEnum("21")]
		RASH,
//		[XmlEnum("22")]
		RELAXED,
//		[XmlEnum("23")]
		SASSY,
//		[XmlEnum("24")]
		SERIOUS,
//		[XmlEnum("25")]
		TIMID
	}
	public enum LevelingRatesList{
//		[XmlEnum("1")]
		ERRATIC,
//		[XmlEnum("2")]
		FAST,
//		[XmlEnum("3")]
		FLUCTUATING,
//		[XmlEnum("4")]
		MEDIUM_FAST,
//		[XmlEnum("5")]
		MEDIUM_SLOW,
//		[XmlEnum("6")]
		SLOW
	}
	public enum NonVolatileStatusConditionList{
//		[XmlEnum("1")]
		NONE,
//		[XmlEnum("2")]
		BADLY_POISONED,
//		[XmlEnum("3")]
		BURNED,
//		[XmlEnum("4")]
		FROZEN,
//		[XmlEnum("5")]
		PARALYZED,
//		[XmlEnum("6")]
		POISONED,
		SLEEP
	}
	
	

#region Gettters & Setters
	public bool IsAlive{
		get{return isAlive;}
		set{isAlive = value;}
	}
	public bool IsCaptured{
		get{return isCaptured;}
		set{isCaptured = value;}
	}
	public string PokemonName{
		get{return pokemonName;}
		set{pokemonName = value;}
	}
	public string Description{
		get{return description;}
		set{description = value;}
	}
	public bool IsFromTrade{
		get{return isFromTrade;}
		set{isFromTrade = value;}
	}
	public int Number{
		get{return number;}
		set{number = value;}
	}
	public int Level{
		get{return level;}
		set{level = value;}
	}
	public int EvolveLevel{
		get{return evolveLevel;}
		set{evolveLevel = value;}
	}
	public TypesList Type01{
		get{return type01;}
		set{type01 = value;}
	}
	public TypesList Type02{
		get{return type02;}
		set{type02 = value;}
	}
	public SexesList Sex{
		get{return sex;}
		set{sex = value;}
	}
	public NaturesList Nature{
		get{return nature;}
		set{nature = value;}
	}
	public string Ability01{
		get{return ability01;}
		set{ability01 = value;}
	}
	public string Ability02{
		get{return ability02;}
		set{ability02 = value;}
	}
	public int BaseHP{
		get{return baseHP;}
		set{baseHP = value;}
	}
	public int BaseATK{
		get{return baseATK;}
		set{baseATK = value;}
	}
	public int BaseDEF{
		get{return baseDEF;}
		set{baseDEF = value;}
	}
	public int BaseSPATK{
		get{return baseSPATK;}
		set{baseSPATK = value;}
	}
	public int BaseSPDEF{
		get{return baseSPDEF;}
		set{baseSPDEF = value;}
	}
	public int BaseSPD{
		get{return baseSPD;}
		set{baseSPD = value;}
	}
	public int CurHP{
		get{return curHP;}
		set{curHP = value;}
	}
	public int CurATK{
		get{return curATK;}
		set{curATK = value;}
	}
	public int CurDEF{
		get{return curDEF;}
		set{curDEF = value;}
	}
	public int CurSPATK{
		get{return curSPATK;}
		set{curSPATK = value;}
	}
	public int CurSPDEF{
		get{return curSPDEF;}
		set{curSPDEF = value;}
	}
	public int CurSPD{
		get{return curSPD;}
		set{curSPD = value;}
	}
	public int MaxHP{
		get{return maxHP;}
		set{maxHP = value;}
	}
	public int MaxATK{
		get{return maxATK;}
		set{maxATK = value;}
	}
	public int MaxDEF{
		get{return maxDEF;}
		set{maxDEF = value;}
	}
	public int MaxSPATK{
		get{return maxSPATK;}
		set{maxSPATK = value;}
	}
	public int MaxSPDEF{
		get{return maxSPDEF;}
		set{maxSPDEF = value;}
	}
	public int MaxSPD{
		get{return maxSPD;}
		set{maxSPD = value;}
	}
	public float Evasion{
		get{return evasion;}
		set{evasion = value;}
	}
	public float Accuracy{
		get{return accuracy;}
		set{accuracy = value;}
	}
	public int HPEV{
		get{return hpEV;}
		set{hpEV = value;}
	}
	public int ATKEV{
		get{return atkEV;}
		set{atkEV = value;}
	}
	public int DEFEV{
		get{return defEV;}
		set{defEV = value;}
	}
	public int SPATKEV{
		get{return spatkEV;}
		set{spatkEV = value;}
	}
	public int SPDEFEV{
		get{return spdefEV;}
		set{spdefEV = value;}
	}
	public int SPDEV{
		get{return spdEV;}
		set{spdEV = value;}
	}
	public int HPIV{
		get{return hpIV;}
		set{hpIV = value;}
	}
	public int ATKIV{
		get{return atkIV;}
		set{atkIV = value;}
	}
	public int DEFIV{
		get{return defIV;}
		set{defIV = value;}
	}
	public int SPATKIV{
		get{return spatkIV;}
		set{spatkIV = value;}
	}
	public int SPDEFIV{
		get{return spdefIV;}
		set{spdefIV = value;}
	}
	public int SPDIV{
		get{return spdIV;}
		set{spdIV = value;}
	}
	public int BaseEXPYield{
		get{return baseEXPYield;}
		set{baseEXPYield = value;}
	}
	public LevelingRatesList LevelingRate{
		get{return levelingRate;}
		set{levelingRate = value;}
	}
	public int CurrentXP{
		get{return currentXP;}
		set{currentXP = value;}
	}
	public int NextRequiredXP{
		get{return nextRequiredXP;}
		set{nextRequiredXP = value;}
	}
	public int HPEVYield{
		get{return hpEVYield;}
		set{hpEVYield = value;}
	}
	public int ATKEVYield{
		get{return atkEVYield;}
		set{atkEVYield = value;}
	}
	public int DEFEVYield{
		get{return defEVYield;}
		set{defEVYield = value;}
	}
	public int SPATKEVYield{
		get{return spatkEVYield;}
		set{spatkEVYield = value;}
	}
	public int SPDEFEVYield{
		get{return spdefEVYield;}
		set{spdefEVYield = value;}
	}
	public int SPDEVYield{
		get{return spdEVYield;}
		set{spdEVYield = value;}
	}
	public int BaseFriendship{
		get{return baseFriendship;}
		set{baseFriendship = value;}
	}
	public int CatchRate{
		get{return catchRate;}
		set{catchRate = value;}
	}
	public NonVolatileStatusConditionList StatusCondition{
		get{return statusCondition;}
		set{statusCondition = value;}
	}
	public bool Seeded{
		get{return seeding;}
		set{seeding = value;}
	}
	public Item EquippedItem{
		get{return equippedItem;}
		set{equippedItem = value;}
	}
	public bool IsInBattle{
		get{return isInBattle;}
		set{isInBattle = value;}
	}
	public int Origin{
		get{return origin;}
		set{origin = value;}
	}
	public int GenderRatio{
		get{return genderRatio;}
		set{genderRatio = value;}
	}
	public bool IsShiny{
		get{return isShiny;}
		set{isShiny = value;}
	}
#endregion

	
}