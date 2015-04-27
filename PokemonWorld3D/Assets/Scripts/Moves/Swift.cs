using UnityEngine;
using System.Collections;

public class Swift : Move
{
    public ParticleSystem swift;

	public Swift()
	{
		aoe = true;
		moveName = "Swift";
		description = "Star-shaped rays are shot at the opposing Pokémon. This attack never misses.";
		type = PokemonTypes.Types.NORMAL;
		category = MoveCategoriesList.SPECIAL;
		contestCategory = ContestTypesList.COOL;
		ppCost = 20;
		power = 60;
		accuracy = 1.0f;
		affectedByProtect = true;
		affectedByKingsRock = true;
		coolDown = 15.0f;
	}

    public void StartSwift()
    {
        swift.Play();
    }
    public void FinishSwift()
    {
        MoveResults();
        GetComponent<Animator>().SetBool(moveName, false);
        GetComponent<PokemonInput>().NotAttacking();
        GetComponent<WildPokemonAI>().usingMove = false;
    }
}
