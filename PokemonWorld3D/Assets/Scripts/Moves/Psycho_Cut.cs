using UnityEngine;
using System.Collections;

public class Psycho_Cut : Move
{
	public Psycho_Cut()
	{
		moveName = "Psycho Cut";
		description = "The user tears at the target with blades formed by psychic power. It has a high critical-hit ratio.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.PHYSICAL;
		contestCategory = ContestTypesList.COOL;
		ppCost = 20;
		power = 70;
		accuracy = 1.0f;
		highCritChance = true;
		affectedByProtect = true;
		affectedByKingsRock = true;
		coolDown = 10.0f;
	}
}
