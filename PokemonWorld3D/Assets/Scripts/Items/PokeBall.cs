using UnityEngine;
using System.Collections;

[System.Serializable]
public class PokeBall : Item{
	
	public Sprite icon;
	public GameObject model;
	public float catchRate;
	public PokeBallTypes pokeBallType;

	public enum PokeBallTypes{
		CHERISHBALL,
		DIVEBALL,
		DREAMBALL,
		DUSKBALL,
		FASTBALL,
		FRIENDBALL,
		GREATBALL,
		HEALBALL,
		HEAVYBALL,
		LEVELBALL,
		LOVEBALL,
		LUREBALL,
		LUXURYBALL,
		MASTERBALL,
		MOONBALL,
		NESTBALL,
		NETBALL,
		PARKBALL,
		POKEBALL,
		PREMIERBALL,
		QUICKBALL,
		REPEATBALL,
		SAFARIBALL,
		SPORTBALL,
		TIMERBALL,
		ULTRABALL
	}

	public PokeBall(string newName, string newDescription, float newCatchRate, int newCost, int newWorth, PokeBallTypes newPBType, Item.ItemTypes newType){
		name = newName;
		description = newDescription;
		icon = Resources.Load<Sprite>("Sprites/PokeBalls/" + name);
		catchRate = newCatchRate;
		cost = newCost;
		worth = newWorth;
		pokeBallType = newPBType;
		type = newType;
	}

	public PokeBall(){

	}
}
