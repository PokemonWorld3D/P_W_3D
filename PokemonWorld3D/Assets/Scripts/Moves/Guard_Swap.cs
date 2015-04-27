using UnityEngine;
using System.Collections;

public class Guard_Swap : Move
{
	public Guard_Swap()
	{
		moveName = "Guard Swap";
		description = "The user employs its psychic power to switch changes to its Defense and Special Defense stats with the target.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.CUTE;
		ppCost = 10;
		affectedByProtect = true;
		coolDown = 120.0f;
	}
}