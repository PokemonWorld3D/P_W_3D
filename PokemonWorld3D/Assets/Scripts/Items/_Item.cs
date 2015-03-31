using UnityEngine;
using System.Collections;

[System.Serializable]
public class _Item {

	public string name;
	public string description;
	public int cost;
	public int worth;
	public ItemsTypes itemType;
	public ItemTypes type;

	public enum ItemTypes{
		AESTHETIC,
		BERRYANDAPRICORN,
		ITEM,
		MEDICINE,
		OTHER
	}
	public enum ItemsTypes{
		NONE,
		ESCAPE,
		EVOLUTION_STONE,
		EXCHANGEABLE,
		FLUTE,
		FOSSIL,
		HELD,
		LEGENDARY_ARTIFACT,
		REPEL,
		SHARD,
		VALUABLE
	}

	public _Item(string newName, string newDescription, int newCost, int newWorth, ItemsTypes newItemType, ItemTypes newType){
		name = newName;
		description = newDescription;
		cost = newCost;
		worth = newWorth;
		itemType = newItemType;
		type = newType;
	}

	public _Item(){

	}

}
