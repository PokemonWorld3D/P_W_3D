using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HateHolder
{
	public GameObject player;
	public Pokemon pokemon;
	public int amount_of_hate;

	public HateHolder(GameObject new_player, Pokemon new_pokemon, int new_hate_amount)
	{
		player = new_player;
		pokemon = new_pokemon;
		amount_of_hate = new_hate_amount;
	}

	private class HateComparer : IComparer<HateHolder>
	{
		public int Compare(HateHolder x, HateHolder y)
		{
			return ((new CaseInsensitiveComparer()).Compare(((HateHolder)x).amount_of_hate, ((HateHolder)y).amount_of_hate));
		}
	}
}