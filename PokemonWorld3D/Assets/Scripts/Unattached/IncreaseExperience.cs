﻿using UnityEngine;
using System.Collections;

public class IncreaseExperience {
	
	private float a;
	private float t;
	private int baseEXP;
	private float e;
	private int level;
	//private int f;
	private float v;
	private int s;

	public int AddExperience(GameObject faintedPKMN, GameObject winningPKMN){
		//f = 1.2 if the pkmn calculating exp for has 2 or more affection hearts or 1 if less than 2 affection hearts
		if(faintedPKMN.CompareTag("WildPokemon")){
			a = 1.0f;
		}else{
			a = 1.5f;
		}
		if(winningPKMN.GetComponent<Pokemon>().is_from_trade){
			t = 1.5f;
		}else{
			t = 1.0f;
		}
		baseEXP = faintedPKMN.GetComponent<Pokemon>().base_exp_yield;
		if(winningPKMN.GetComponent<Pokemon>().equipped_item.name == "Lucky Egg"){
			e = 1.5f; 
		}else{
			e = 1.0f;
		}
		level = faintedPKMN.GetComponent<Pokemon>().level;
		if(winningPKMN.GetComponent<Pokemon>().level > winningPKMN.GetComponent<Pokemon>().evolve_level){
			v = 1.2f;
		}else{
			v = 1.0f;
		}
		s = 1;
		return (int)Mathf.Abs (a * t * baseEXP * e * level * /*f * */v) / (7 * s);
	}

	public int AddExperienceShare(GameObject faintedPKMN, GameObject winningPKMN){
		 //f = 1.2 if the pkmn calculating exp for has 2 or more affection hearts or 1 if less than 2 affection hearts
		s = 2;
		if(faintedPKMN.CompareTag("WildPokemon")){
			a = 1.0f;
		}else{
			a = 1.5f;
		}
		if(winningPKMN.GetComponent<Pokemon>().is_from_trade){
			t = 1.5f;
		}else{
			t = 1.0f;
		}
		baseEXP = faintedPKMN.GetComponent<Pokemon>().base_exp_yield;
		if(winningPKMN.GetComponent<Pokemon>().equipped_item.name == "Lucky Egg"){
			e = 1.5f; 
		}else{
			e = 1.0f;
		}
		level = faintedPKMN.GetComponent<Pokemon>().level;
		//f =
		if(winningPKMN.GetComponent<Pokemon>().level > winningPKMN.GetComponent<Pokemon>().evolve_level){
			v = 1.2f;
		}else{
			v = 1.0f;
		}
		return (int)Mathf.Abs (a * t * baseEXP * e * level * /*f * */v) / (7 * s);
	}
	
}
