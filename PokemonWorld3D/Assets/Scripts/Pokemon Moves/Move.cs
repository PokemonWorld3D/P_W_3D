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
public class Move : ISerializable, System.IEquatable<Move>
{
	public string move_name;
	public string description;
	public int level_learned;
	public PokemonTypes.Types type;
	public MoveCategoriesList category;
	public ContestTypesList contest_category;
	public int pp_cost;
	public int power;
	public float accuracy;
	public float recoil_damage;
	public bool high_crit_chance;
	public bool flinch;
	public float flinch_chance;
	public bool makes_contact;
	public bool affected_by_protect;
	public bool affected_by_magic_coat;
	public bool affected_by_snatch;
	public bool affected_by_kings_rock;
	[XmlArray]
	public List<StatusEffect> status_effects = new List<StatusEffect> ();
	public int status_effect_success_rate;

	public enum MoveCategoriesList{
		PHYSICAL,
		SPECIAL,
		STATUS
	}
	public enum ContestTypesList{
		COOL,
		BEAUTY,
		CUTE,
		SMART,
		TOUGH
	}

	public bool Equals (Move other){
		return this.move_name == other.move_name;
	}

	public Move(string this_name, string this_description, int this_level_learned, PokemonTypes.Types this_type, MoveCategoriesList this_category,
	            ContestTypesList this_contest_type, int this_pp_cost, int this_power, float this_accuracy, float this_recoil_damage, bool this_high_crit_chance,
	            bool this_flinch, int this_flinch_chance, bool this_makes_contact, bool this_affected_by_protect, bool this_affected_by_magic_coat,
	            bool this_affected_by_snatch, bool this_affected_by_kings_rock, List<StatusEffect> this_status_effects, int this_status_effect_success_rate){
		move_name = this_name;
		description = this_description;
		level_learned = this_level_learned;
		type = this_type;
		category = this_category;
		contest_category = this_contest_type;
		pp_cost = this_pp_cost;
		power = this_power;
		accuracy = this_accuracy;
		recoil_damage = this_recoil_damage;
		high_crit_chance = this_high_crit_chance;
		flinch = this_flinch;
		flinch_chance = this_flinch_chance;
		makes_contact = this_makes_contact;
		affected_by_protect = this_affected_by_protect;
		affected_by_magic_coat = this_affected_by_magic_coat;
		affected_by_snatch = this_affected_by_snatch;
		affected_by_kings_rock = this_affected_by_kings_rock;
		status_effects = this_status_effects;
		status_effect_success_rate = this_status_effect_success_rate;
	}

	public Move(){

	}

	protected Move(SerializationInfo info, StreamingContext context){
		move_name = info.GetString("move_name");
		description = info.GetString("description");
		level_learned = info.GetInt32("level_learned");
		type = (PokemonTypes.Types)info.GetByte("type");
		category = (MoveCategoriesList)info.GetByte("category");
		contest_category = (ContestTypesList)info.GetByte("contest_category");
		pp_cost = info.GetInt32("pp_cost");
		power = info.GetInt32("power");
		accuracy = (float)info.GetInt32("accuracy");
		recoil_damage = (float)info.GetInt32("recoil_damage");
		high_crit_chance = info.GetBoolean("high_crit_chance");
		flinch = info.GetBoolean("flinch");
		flinch_chance = (float)info.GetInt32("flinch_chance");
		makes_contact = info.GetBoolean("makes_contact");
		affected_by_protect = info.GetBoolean("affected_by_protect");
		affected_by_magic_coat = info.GetBoolean("affected_by_magic_coat");
		affected_by_snatch = info.GetBoolean("affected_by_snatch");
		affected_by_kings_rock = info.GetBoolean("affected_by_kings_rock");
//		moveStatusEffects = statusEffects;
		status_effect_success_rate = info.GetInt32("status_effect_success_rate");
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context){
		info.AddValue("move_name", move_name);
		info.AddValue("description", description);
		info.AddValue("level_learned", level_learned);
		info.AddValue("type", type);
		info.AddValue("category", category);
		info.AddValue("contest_category", contest_category);
		info.AddValue("pp_cost", pp_cost);
		info.AddValue("power", power);
		info.AddValue("accuracy", accuracy);
		info.AddValue("recoil_damage", recoil_damage);
		info.AddValue("high_crit_chance", high_crit_chance);
		info.AddValue("flinch", flinch);
		info.AddValue("flinch_chance", flinch_chance);
		info.AddValue("makes_contact", makes_contact);
		info.AddValue("affected_by_protect", affected_by_protect);
		info.AddValue("affected_by_magic_coat", affected_by_magic_coat);
		info.AddValue("affected_by_snatch", affected_by_snatch);
		info.AddValue("affected_by_kings_rock", affected_by_kings_rock);
		info.AddValue("status_effects", status_effects);
		info.AddValue("status_effect_success_rate", status_effect_success_rate);
	}
	
}
