using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public class PlayerPokemonData : ISerializable
{
	public bool isCaptured;
	public string pokemonName;
	public string nickName;
	public bool isFromTrade;
	public int level;
	public Pokemon.Genders gender;
	public Pokemon.Natures nature;
	public int curHP;
	public int hpEV;
	public int atkEV;
	public int defEV;
	public int spatkEV;
	public int spdef_ev;
	public int spdefEV;
	public int spdEV;
	public int hpIV;
	public int atkIV;
	public int defIV;
	public int spatkIV;
	public int spdefIV;
	public int spdIV;
	public int currentEXP;
	[XmlArray]
	public List<string> MovesToLearnNames;
	[XmlArray]
	public List<string> KnownMovesNames;
	public string equippedItemName;
	public int origin;

	public PlayerPokemonData()
	{

	}
	
	public PlayerPokemonData(bool newIsCaptured, string newPokemonName, string newNickName, bool newIsFromTrade,
	                          int newLevel, Pokemon.Genders newSex, Pokemon.Natures newNature, int newCurHP, int newHpEV, int newAtkEV, int newDefEV,
	                          int newSpatkEV, int newSpdefEV, int newSpdEV, int newHpIV, int newAtkIV, int newDefIV, int newSpatkIV, int newSpdefIV, int newSpdIV,
	                          int newCurrentXP,
	                          List<string> newMovesToLearn, List<string> newPokemonsMoves, string newEquippedItemName, int newOrigin)
	{
		isCaptured = newIsCaptured;
		pokemonName = newPokemonName;
		nickName = newNickName;
		isFromTrade = newIsFromTrade;
		level = newLevel;
		gender = newSex;
		nature = newNature;
		curHP = newCurHP;
		hpEV = newHpEV;
		atkEV = newAtkEV;
		defEV = newDefEV;
		spatkEV = newSpatkEV;
		spdef_ev = newSpdefEV;
		spdefEV = newSpdEV;
		spdEV = newSpdEV;
		hpIV = newHpIV;
		atkIV = newAtkIV;
		defIV = newDefIV;
		spatkIV = newSpatkIV;
		spdefIV = newSpdefIV;
		spdIV = newSpdIV;
		currentEXP = newCurrentXP;
		MovesToLearnNames = newMovesToLearn;
		KnownMovesNames = newPokemonsMoves;
		equippedItemName = newEquippedItemName;
		origin = newOrigin;
	}
	

	protected PlayerPokemonData(SerializationInfo info, StreamingContext context)
	{
		isCaptured = info.GetBoolean("isCapted");
		pokemonName = info.GetString("pokemonName");
		nickName = info.GetString("nickName");
		isFromTrade = info.GetBoolean("isFromTrade");
		level = info.GetInt32("level");
		gender = (Pokemon.Genders)info.GetByte("sex");
		nature = (Pokemon.Natures)info.GetByte("nature");
		curHP = info.GetInt32("curHP");
		hpEV = info.GetInt32("hpEV");
		atkEV = info.GetInt32("atkEV");
		defEV = info.GetInt32("defEV");
		spatkEV = info.GetInt32("spatkEV");
		spdef_ev = info.GetInt32("spdefEV");
		spdefEV = info.GetInt32("spdEV");
		hpIV = info.GetInt32("hpIV");
		atkIV = info.GetInt32("atkIV");
		defIV = info.GetInt32("defIV");
		spatkIV = info.GetInt32("spatkIV");
		spdefIV = info.GetInt32("spdefIV");
		spdIV = info.GetInt32("spdIV");
		currentEXP = info.GetInt32("currentEXP");
		//MovesToLearnNames = info.get
		//pokemonsMoves = info.
		equippedItemName = info.GetString("equippedItemName");
		origin = info.GetInt32("origin");
	}
	
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("isCaptured", isCaptured);
		info.AddValue("pokemonName", pokemonName);
		info.AddValue("nickName", nickName);
		info.AddValue("isFromTrade", isFromTrade);
		info.AddValue("level", level);
		info.AddValue("sex", gender);
		info.AddValue("nature", nature);
		info.AddValue("curHP", curHP);
		info.AddValue("hpEV", hpEV);
		info.AddValue("atkEV", atkEV);
		info.AddValue("defEV", defEV);
		info.AddValue("spatkEV", spatkEV);
		info.AddValue("spdefEV", spdef_ev);
		info.AddValue("spdEV", spdefEV);
		info.AddValue("hpIV", hpIV);
		info.AddValue("atkIV", atkIV);
		info.AddValue("defIV", defIV);
		info.AddValue("spatkIV", spatkIV);
		info.AddValue("spdefIV", spdefIV);
		info.AddValue("spdIV", spdIV);
		info.AddValue("currentEXP", currentEXP);
		info.AddValue("MovesToLearnNames", MovesToLearnNames);
		info.AddValue("KnownMovesNames", KnownMovesNames);
		info.AddValue("equippedItemName", equippedItemName);
		info.AddValue("origin", origin);
	}
}
