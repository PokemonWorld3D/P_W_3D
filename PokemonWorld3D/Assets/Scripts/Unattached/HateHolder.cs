using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HateHolder
{
	public GameObject pokemon;
	public Pokemon this_pokemon;
	public int amount_of_hate;

	public HateHolder(GameObject new_pokemon, Pokemon new_this_pokemon, int new_hate_amount)
	{
		pokemon = new_pokemon;
		this_pokemon = new_this_pokemon;
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