using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class _Medicine : _Item
{	
	public Sprite icon;
	public GameObject model;
	public int hpRestoreAmount;
	public int ppRestoreAmount;


	public _Medicine(string newName, string newDescription, int newHP, int newPP, int newCost, int newWorth,
	                _Item.ItemTypes newType){
		name = newName;
		description = newDescription;
		icon = Resources.Load<Sprite>("Sprites/Medicines/" + name);
		hpRestoreAmount = newHP;
		ppRestoreAmount = newPP;
		cost = newCost;
		worth = newWorth;
		type = newType;
	}

	public _Medicine(){

	}

}
