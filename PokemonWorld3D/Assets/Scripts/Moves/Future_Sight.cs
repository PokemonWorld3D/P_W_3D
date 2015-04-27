using UnityEngine;
using System.Collections;

public class Future_Sight : Move
{
	public Future_Sight()
	{
		aoe = true;
		moveName = "Future Sight";
		description = "A hunk of psychic energy attacks the target shortly after this move is used.";
		type = PokemonTypes.Types.PSYCHIC;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.SMART;
		ppCost = 10;
		power = 120;
		accuracy = 1.0f;
		coolDown = 45.0f;
	}
}
