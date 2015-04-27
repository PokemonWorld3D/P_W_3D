using UnityEngine;
using System.Collections;

public class Confusion : Move
{
	public Confusion()
	{
		aoe = true;
		selfTargeting = false;
		allyTargeting = false;
		moveName = "Confusion";
		description = "The target is hit by a weak telekinetic force. This may also confuse the target.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.SMART;
		ppCost = 25;
		power = 50;
		accuracy = 1.0f;
		affectedByProtect = true;
		coolDown = 15.0f;
	}
}
