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
		Hate_List.Sort(delegate(HateHolder x, HateHolder y) { return x.amount_of_hate.CompareTo(y.amount_of_hate); });
		target = Hate_List[0].player;
	}

	public void IncreaseHate(GameObject player, Pokemon pokemon, int hate_increase)
	{
		for(int i = 0; i < Hate_List.Count; i++)
		{
			if(Hate_List[i].player = player)
			{
				Hate_List[i].amount_of_hate += hate_increase;
				return;
			}
		}
		Hate_List.Add(new HateHolder(player, pokemon, hate_increase));
	}
}
