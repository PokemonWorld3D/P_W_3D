using UnityEngine;
using System.Collections;

public class Recover : Move
{
	public Recover()
	{
		selfTargeting = true;
		moveName = "Recover";
		description = "Restoring its own cells, the user restores its own HP by half of its max HP.";
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.STATUS;
		contestCategory = ContestTypesList.SMART;
		ppCost = 10;
		affectedBySnatch = true;
		coolDown = 60.0f;
	}
}