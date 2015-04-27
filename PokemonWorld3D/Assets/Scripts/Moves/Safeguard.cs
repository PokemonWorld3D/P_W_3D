using UnityEngine;
using System.Collections;

public class Safeguard : Move
{
	public Safeguard()
	{
		selfTargeting = true;
		allyTargeting = true;
		moveName = "Safeguard";
		description = "The user creates a protective field that prevents status conditions for 25 seconds.";
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.BEAUTY;
		ppCost = 25;
		affectedBySnatch = true;
		coolDown = 180.0f;
	}
}
