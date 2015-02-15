using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Medicine : Item {
	
	public Sprite icon;
	public GameObject model;
	public int hpRestoreAmount;
	public int ppRestoreAmount;
	public List<BasePokemon.NonVolatileStatusConditionList> statusesToHeal = new List<BasePokemon.NonVolatileStatusConditionList>();


	public Medicine(string newName, string newDescription, int newHP, int newPP, int newCost, int newWorth,
	                List<BasePokemon.NonVolatileStatusConditionList> newStatuses, Item.ItemTypes newType){
		name = newName;
		description = newDescription;
		icon = Resources.Load<Sprite>("Sprites/Medicines/" + name);
		hpRestoreAmount = newHP;
		ppRestoreAmount = newPP;
		cost = newCost;
		worth = newWorth;
		statusesToHeal = newStatuses;
		type = newType;
	}

	public Medicine(){

	}

}
