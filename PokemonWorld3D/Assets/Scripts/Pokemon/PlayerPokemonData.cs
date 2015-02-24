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

[System.Serializable]
public class PlayerPokemonData : ISerializable {
	
	public bool is_setup;
	public bool is_captured;
	public string trainers_name;
	public string pokemon_name;
	public string nick_name;
	public bool is_from_trade;
	public int level;
	public BasePokemon.SexesList gender;
	public BasePokemon.NaturesList nature;
	public int max_hp;
	public int cur_max_hp;
	public int max_atk;
	public int max_def;
	public int max_spatk;
	public int max_spdef;
	public int max_spd;
	public int cur_hp;
	public int cur_atk;
	public int cur_def;
	public int cur_spatk;
	public int cur_spdef;
	public int cur_spd;
	public int hp_ev;
	public int atk_ev;
	public int def_ev;
	public int spatk_ev;
	public int spdef_ev;
	public int spd_ev;
	public int hp_iv;
	public int atk_iv;
	public int def_iv;
	public int spatk_iv;
	public int spdef_iv;
	public int spd_iv;
	public int last_required_exp;
	public int current_exp;
	public int next_required_exp;
	public BasePokemon.NonVolatileStatusConditionList status_condition;
	[XmlArray]
	public List<string> moves_to_learn;
	[XmlArray]
	public List<string> known_moves;
	public Move last_move_used;
	public Item equipped_item;
	public int origin;
	public bool is_shiny;

	public PlayerPokemonData(){

	}
	
	public PlayerPokemonData(bool newIsSetup, bool newIsCaptured, string newTrainersName, string newPokemonName, string newNickName, bool newIsFromTrade,
	                            int newLevel,
	                         	BasePokemon.SexesList newSex, BasePokemon.NaturesList newNature, int newMaxHP, int newCurMaxHP, int newMaxATK, int newMaxDEF,
	                        	int newMaxSPATK, int newMaxSPDEF, int newMaxSPD, int newCurHP, int newCurATK, int newCurDEF, int newCurSPATK, int newCurSPDEF,
	                            int newCurSPD, int newHpEV, int newAtkEV, int newDefEV, int newSpatkEV, int newSpdefEV, int newSpdEV, int newHpIV, int newAtkIV,
	                            int newDefIV, int newSpatkIV, int newSpdefIV, int newSpdIV, int newLastRequiredXP, int newCurrentXP, int newNextRequiredXP,
	                            BasePokemon.NonVolatileStatusConditionList newStatusCondition, List<string> newMovesToLearn, List<string> newPokemonsMoves,
	                            Move newLastMoveUsed, Item newEquippedItem, int newOrigin, bool newIsShiny)
	{
		is_setup = newIsSetup;
		is_captured = newIsCaptured;
		trainers_name = newTrainersName;
		pokemon_name = newPokemonName;
		nick_name = newNickName;
		is_from_trade = newIsFromTrade;
		level = newLevel;
		gender = newSex;
		nature = newNature;
		max_hp = newMaxHP;
		cur_max_hp = newCurMaxHP;
		max_atk = newMaxATK;
		max_def = newMaxDEF;
		max_spatk = newMaxSPATK;
		max_spdef = newMaxSPDEF;
		max_spd = newMaxSPD;
		cur_hp = newCurHP;
		cur_atk = newCurATK;
		cur_def = newCurDEF;
		cur_spatk = newCurSPATK;
		cur_spdef = newCurSPDEF;
		cur_spd = newCurSPD;
		hp_ev = newHpEV;
		atk_ev = newAtkEV;
		def_ev = newDefEV;
		spatk_ev = newSpatkEV;
		spdef_ev = newSpdefEV;
		spd_ev = newSpdEV;
		hp_iv = newHpIV;
		atk_iv = newAtkIV;
		def_iv = newDefIV;
		spatk_iv = newSpatkIV;
		spdef_iv = newSpdefIV;
		spd_iv = newSpdIV;
		last_required_exp = newLastRequiredXP;
		current_exp = newCurrentXP;
		next_required_exp = newNextRequiredXP;
		status_condition = newStatusCondition;
		moves_to_learn = newMovesToLearn;
		known_moves = newPokemonsMoves;
		last_move_used = newLastMoveUsed;
		equipped_item = newEquippedItem;
		origin = newOrigin;
		is_shiny = newIsShiny;
	}
	

	protected PlayerPokemonData(SerializationInfo info, StreamingContext context){
		is_setup = info.GetBoolean("isSetup");
		is_captured = info.GetBoolean("isCapted");
		trainers_name = info.GetString("trainersName");
		pokemon_name = info.GetString("pokemon_name");
		nick_name = info.GetString("nickName");
		is_from_trade = info.GetBoolean("isFromTrade");
		level = info.GetInt32("level");
		gender = (BasePokemon.SexesList)info.GetByte("sex");
		nature = (BasePokemon.NaturesList)info.GetByte("nature");
		max_hp = info.GetInt32("maxHP");
		cur_max_hp = info.GetInt32("curMaxHP");
		max_atk = info.GetInt32("maxATK");
		max_def = info.GetInt32("maxDEF");
		max_spatk = info.GetInt32("maxSPATK");
		max_spdef = info.GetInt32("maxSPDEF");
		max_spd = info.GetInt32("maxSPD");
		cur_hp = info.GetInt32("curHP");
		cur_atk = info.GetInt32("curATK");
		cur_def = info.GetInt32("curDEF");
		cur_spatk = info.GetInt32("curSPATK");
		cur_spdef = info.GetInt32("curSPDEF");
		cur_spd = info.GetInt32("curSPD");
		hp_ev = info.GetInt32("hpEV");
		atk_ev = info.GetInt32("atkEV");
		def_ev = info.GetInt32("defEV");
		spatk_ev = info.GetInt32("spatkEV");
		spdef_ev = info.GetInt32("spdefEV");
		spd_ev = info.GetInt32("spdEV");
		hp_iv = info.GetInt32("hpIV");
		atk_iv = info.GetInt32("atkIV");
		def_iv = info.GetInt32("defIV");
		spatk_iv = info.GetInt32("spatkIV");
		spdef_iv = info.GetInt32("spdefIV");
		spd_iv = info.GetInt32("spdIV");
		last_required_exp = info.GetInt32("lastRequiredXP");
		current_exp = info.GetInt32("currentXP");
		next_required_exp = info.GetInt32("nextRequiredXP");
		status_condition = (BasePokemon.NonVolatileStatusConditionList)info.GetByte("statusCondition");
//		movesToLearn = info.
//		pokemonsMoves = info.
//		equippedItem = info.GetValue("baseItem", BaseItem);
                        //		lastMoveUsed = info.
		origin = info.GetInt32("origin");
		is_shiny = info.GetBoolean("isShiny");
	}
	
	public void GetObjectData(SerializationInfo info, StreamingContext context){
		info.AddValue("isSetup", is_setup);
		info.AddValue("isCaptured", is_captured);
		info.AddValue("trainersName", trainers_name);
		info.AddValue("pokemon_name", pokemon_name);
		info.AddValue("nickName", nick_name);
		info.AddValue("isFromTrade", is_from_trade);
		info.AddValue("level", level);
		info.AddValue("sex", gender);
		info.AddValue("nature", nature);
		info.AddValue("maxHP", max_hp);
		info.AddValue("curMaxHP", cur_max_hp);
		info.AddValue("maxATK", max_atk);
		info.AddValue("maxDEF", max_def);
		info.AddValue("maxSPATK", max_spatk);
		info.AddValue("maxSPDEF", max_spdef);
		info.AddValue("maxSPD", max_spd);
		info.AddValue("curHP", cur_hp);
		info.AddValue("curATK", cur_atk);
		info.AddValue("curDEF", cur_def);
		info.AddValue("curSPATK", cur_spatk);
		info.AddValue("curSPDEF", cur_spdef);
		info.AddValue("curSPD", cur_spd);
		info.AddValue("hpEV", hp_ev);
		info.AddValue("atkEV", atk_ev);
		info.AddValue("defEV", def_ev);
		info.AddValue("spatkEV", spatk_ev);
		info.AddValue("spdefEV", spdef_ev);
		info.AddValue("spdEV", spd_ev);
		info.AddValue("hpIV", hp_iv);
		info.AddValue("atkIV", atk_iv);
		info.AddValue("defIV", def_iv);
		info.AddValue("spatkIV", spatk_iv);
		info.AddValue("spdefIV", spdef_iv);
		info.AddValue("spdIV", spd_iv);
		info.AddValue("lastRequiredXP", last_required_exp);
		info.AddValue("currentXP", current_exp);
		info.AddValue("nextRequiredXP", next_required_exp);
		info.AddValue("statusCondition", status_condition);
		info.AddValue("movesToLearn", moves_to_learn);
		info.AddValue("pokemonsMoves", known_moves);
		info.AddValue("lastMoveUsed", last_move_used);
		info.AddValue("equippedItem", equipped_item);
		info.AddValue("origin", origin);
		info.AddValue("isShiny", is_shiny);
	}
}
