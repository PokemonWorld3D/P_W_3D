using UnityEngine;
using System.Collections;

public class Aura_Sphere : Move
{
	public Aura_Sphere()
	{
		moveName = "Aura Sphere";
		description = "The user lets loose a blast of aura power from deep within its body at the target. This attack never misses.";
		type = PokemonTypes.Types.FIGHTING;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.BEAUTY;
		ppCost = 20;
		power = 80;
		accuracy = 1.0f;
		affectedByProtect = true;
		affectedByKingsRock = true;
		coolDown = 10.0f;
	}
}