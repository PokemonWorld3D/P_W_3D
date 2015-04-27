using UnityEngine;
using System.Collections;

public class Power_Swap : Move
{
	public Power_Swap()
	{
		moveName = "Power Swap";
		description = "The user employs its psychic power to switch changes to its Attack and Special Attack stats with the target.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.BEAUTY;
		ppCost = 10;
		affectedByProtect = true;
		coolDown = 120.0f;
	}
}
