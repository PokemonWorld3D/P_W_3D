using UnityEngine;
using System.Collections;

public class Psychic : Move
{
	public Psychic()
	{
		aoe = true;
		moveName = "Psychic";
		description = "The target is hit by a strong telekinetic force. This may also lower the target's Special Defense stat.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.SMART;
		ppCost = 10;
		power = 90;
		accuracy = 1.0f;
		affectedByProtect = true;
		coolDown = 10.0f;
	}
}