using UnityEngine;
using System.Collections;

public class Mist : Move
{
	public Mist()
	{
		selfTargeting = true;
		allyTargeting = true;
		moveName = "Mist";
		description = "The user cloaks itself and its allies in a white mist that prevents any of their stats from being lowered for 25 seconds.";
		type = PokemonTypes.Types.ICE;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.BEAUTY;
		ppCost = 30;
		affectedBySnatch = true;
		coolDown = 180.0f;
	}
}