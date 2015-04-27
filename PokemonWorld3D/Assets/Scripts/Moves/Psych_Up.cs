using UnityEngine;
using System.Collections;

public class Psych_Up : Move
{
	public Psych_Up()
	{
		selfTargeting = true;
		moveName = "Psych Up";
		description = "The user hypnotizes itself into copying any stat change made by the target.";
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.SMART;
		ppCost = 10;
		affectedBySnatch = true;
		coolDown = 120.0f;
	}
}
