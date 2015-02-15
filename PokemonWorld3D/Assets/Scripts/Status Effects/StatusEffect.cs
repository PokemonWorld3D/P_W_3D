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
public class StatusEffect : ISerializable{
	
	public string statusEffectName;
	public int stagesToChange;
	public StatusEffectCategories statusEffectCategory;
	public string changeAbilityTo;
	
	public enum StatusEffectCategories{
		NON_VOLATILE,
		VOLATILE,
		IN_BATTLE_ONLY,
		NONE
	}

	public StatusEffect(string name, int change, StatusEffectCategories category, string newAbility){
		statusEffectName = name;
		stagesToChange = change;
		statusEffectCategory = category;
		changeAbilityTo = newAbility;
	}
	
	public StatusEffect(){
		
	}

	protected StatusEffect(SerializationInfo info, StreamingContext context){
		statusEffectName = info.GetString("statusEffectName");
		stagesToChange = info.GetInt32("stagesToChange");
		statusEffectCategory = (StatusEffectCategories)info.GetByte("statusEffectCategory");
		changeAbilityTo = info.GetString("changeAbilityTo");
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context){
		info.AddValue("statusEffectName", statusEffectName);
		info.AddValue("stagesToChange", stagesToChange);
		info.AddValue("statusEffectCategory", statusEffectCategory);
		info.AddValue("changeAbilityTo", changeAbilityTo);
	}

}
