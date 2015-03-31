using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MewtwoAI : MonoBehaviour
{
	public List<HateHolder> Hate_List;
	public GameObject target;

	void Start()
	{
		Hate_List = new List<HateHolder>();
	}
	void Update()
	{
		Hate_List.Sort(delegate(HateHolder x, HateHolder y) { return x.amountOfHate.CompareTo(y.amountOfHate); });
		target = Hate_List[0].pokemon;
	}

	public void IncreaseHate(GameObject pokemon, Pokemon this_pokemon, int hate_increase)
	{
		for(int i = 0; i < Hate_List.Count; i++)
		{
			if(Hate_List[i].pokemon = pokemon)
			{
				Hate_List[i].amountOfHate += hate_increase;
				return;
			}
		}
		Hate_List.Add(new HateHolder(pokemon, this_pokemon, hate_increase));
	}
}
