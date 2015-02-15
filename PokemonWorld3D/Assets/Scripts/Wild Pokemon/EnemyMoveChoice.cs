using UnityEngine;
using System.Collections;

public class EnemyMoveChoice {

	private Move chosenMove;
	private int randomTemp;

	public Move ChooseEnemyMove(Pokemon enemy){
		if(enemy.pokemonsMoves.Count == 1){
			chosenMove = enemy.pokemonsMoves[0];
		}
		if(enemy.pokemonsMoves.Count == 2){
			randomTemp = Random.Range(0,1);
			if(randomTemp == 0){
				chosenMove = enemy.pokemonsMoves[0];
			}
			if(randomTemp == 1){
				chosenMove = enemy.pokemonsMoves[1];
			}
		}
		if(enemy.pokemonsMoves.Count == 3){
			randomTemp = Random.Range(0,2);
			if(randomTemp == 0){
				chosenMove = enemy.pokemonsMoves[0];
			}
			if(randomTemp == 1){
				chosenMove = enemy.pokemonsMoves[1];
			}
			if(randomTemp == 2){
				chosenMove = enemy.pokemonsMoves[2];
			}
		}
		if(enemy.pokemonsMoves.Count == 4){
			randomTemp = Random.Range(0,3);
			if(randomTemp == 0){
				chosenMove = enemy.pokemonsMoves[0];
			}
			if(randomTemp == 1){
				chosenMove = enemy.pokemonsMoves[1];
			}
			if(randomTemp == 2){
				chosenMove = enemy.pokemonsMoves[2];
			}
			if(randomTemp == 3){
				chosenMove = enemy.pokemonsMoves[3];
			}
		}
		return chosenMove;
	}

}
