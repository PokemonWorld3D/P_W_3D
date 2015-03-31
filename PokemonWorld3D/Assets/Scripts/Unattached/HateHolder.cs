using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HateHolder
{
	public GameObject pokemon;
	public Pokemon thisPokemon;
	public int amountOfHate;

	public HateHolder(GameObject thePokemon, Pokemon me, int theAmountOfHate)
	{
		pokemon = thePokemon;
		thisPokemon = me;
		amountOfHate = theAmountOfHate;
	}

	private class HateAmountComparer : IComparer<HateHolder>
	{
		public int Compare(HateHolder x, HateHolder y)
		{
			return ((new CaseInsensitiveComparer()).Compare(((HateHolder)x).amountOfHate, ((HateHolder)y).amountOfHate));
		}
	}
}