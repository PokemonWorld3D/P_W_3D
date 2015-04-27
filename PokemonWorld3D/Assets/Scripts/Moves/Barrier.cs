using UnityEngine;
using System.Collections;

public class Barrier : Move
{
	public Barrier()
	{
		selfTargeting = true;
		moveName = "Barrier";
		description = "The user throws up a sturdy wall that sharply raises its Defense stat.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.COOL;
		ppCost = 20;
		affectedBySnatch = true;
		coolDown = 60.0f;
	}
}