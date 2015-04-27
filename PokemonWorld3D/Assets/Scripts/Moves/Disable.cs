using UnityEngine;
using System.Collections;

public class Disable : Move
{
	public Disable()
	{
		aoe = true;
		moveName = "Disable";
		description = "For 20 seconds, this move prevents the target from using the move it last used.";
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.SMART;
		ppCost = 20;
		accuracy = 1.0f;
		affectedByProtect = true;
		affectedByMagicCoat = true;
		coolDown = 120.0f;
	}
}
