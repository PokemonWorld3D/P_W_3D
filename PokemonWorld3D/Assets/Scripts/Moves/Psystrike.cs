using UnityEngine;
using System.Collections;

public class Psystrike : Move
{
	public Psystrike()
	{
		aoe = true;
		moveName = "Psystrike";
		description = "The user materializes an odd psychic wave to attack the target. This attack does physical damage.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.COOL;
		ppCost = 10;
		power = 100;
		accuracy = 1.0f;
		affectedByProtect = true;
		affectedByKingsRock = true;
		coolDown = 180.0f;
	}
}