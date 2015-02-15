using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStateStart {

	public static GameObject wildPKMN;
	public static GameObject playerPKMN;

	public void PrepareBattle(){
		wildPKMN = GameObject.FindGameObjectWithTag("WildPokemon");
		playerPKMN = GameObject.FindGameObjectWithTag("PlayerPokemon");
	}
	
}
