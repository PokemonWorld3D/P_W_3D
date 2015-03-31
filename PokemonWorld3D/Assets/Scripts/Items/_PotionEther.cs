using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class _PotionEther {
	
	public string name;
	public int iD;
	public string description;
	public Sprite icon;
	public GameObject model;
	public int hpRestoreAmount;
	public int ppRestoreAmount;
	public int cost;
	public int worth;
	
	
	public _PotionEther(string newName, int newID, string newDescription, int newHP, int newPP, int newCost, int newWorth){
		name = newName;
		iD = newID;
		description = newDescription;
		icon = Resources.Load<Sprite>("Sprites/Potions & Ethers/" + name);
		hpRestoreAmount = newHP;
		ppRestoreAmount = newPP;
		cost = newCost;
		worth = newWorth;
	}
	
	public _PotionEther(){
		
	}
	
}