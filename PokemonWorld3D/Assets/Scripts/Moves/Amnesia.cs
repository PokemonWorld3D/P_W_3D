using UnityEngine;
using System.Collections;

public class Amnesia : Move
{
	public Amnesia()
	{
		selfTargeting = true;
		moveName = "Amnesia";
		description = "The user temporarily empties its mind to forget its concerns. This sharply raises the user's Special Defense stat.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.CUTE;
		ppCost = 20;
		affectedBySnatch = true;
		coolDown = 60.0f;
	}
}