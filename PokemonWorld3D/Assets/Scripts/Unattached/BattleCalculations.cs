using UnityEngine;
using System.Collections;

public class BattleCalculations : MonoBehaviour
{
	public static float[,] typeToTypeDamageRatios;

	private int baseDamage;
	private float stab1;
	private float stab2;
	private int random;
	private float chance;
	private float crit;
	private float typeEffectiveness;
	private float modifier;
	private float te1;
	private float te2;

	void Start()
	{
#region typeToTypeDamageRatios
		typeToTypeDamageRatios = new float[19,19];
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.DARK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.FAIRY] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.FIGHTING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.FLYING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.GHOST] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.GRASS] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.POISON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.PSYCHIC] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.BUG, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.DARK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.FAIRY] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.FIGHTING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.GHOST] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.PSYCHIC] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.STEEL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DARK, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.DRAGON] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.DRAGON, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.DRAGON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.ELECTRIC] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.FLYING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.GRASS] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.GROUND] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.STEEL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ELECTRIC, (int)PokemonTypes.Types.WATER] = 2.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.DARK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.DRAGON] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.FIGHTING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.POISON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FAIRY, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.BUG] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.DARK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.FAIRY] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.FLYING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.GHOST] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.ICE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.NORMAL] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.POISON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.PSYCHIC] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.ROCK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.STEEL] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIGHTING, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.BUG] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.DRAGON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.GRASS] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.ICE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.ROCK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.STEEL] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FIRE, (int)PokemonTypes.Types.WATER] = 0.5f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.BUG] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.ELECTRIC] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.FIGHTING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.GRASS] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.ROCK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.FLYING, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.DARK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.GHOST] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.NORMAL] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.PSYCHIC] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.STEEL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GHOST, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.BUG] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.DRAGON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.FLYING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.GRASS] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.GROUND] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.POISON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.ROCK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GRASS, (int)PokemonTypes.Types.WATER] = 2.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.BUG] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.ELECTRIC] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.FIRE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.FLYING] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.GRASS] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.POISON] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.ROCK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.STEEL] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.GROUND, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.DRAGON] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.FLYING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.GRASS] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.GROUND] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.ICE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ICE, (int)PokemonTypes.Types.WATER] = 0.5f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.STEEL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NONE, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.GHOST] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.ROCK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.NORMAL, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.FAIRY] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.GHOST] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.GRASS] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.GROUND] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.POISON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.ROCK] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.STEEL] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.POISON, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.DARK] = 0.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.FIGHTING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.FIRE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.POISON] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.PSYCHIC] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.PSYCHIC, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.BUG] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.FIGHTING] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.FIRE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.FLYING] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.GROUND] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.ICE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.ROCK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.ROCK, (int)PokemonTypes.Types.WATER] = 1.0f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.DRAGON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.ELECTRIC] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.FAIRY] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.FIRE] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.GRASS] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.GROUND] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.ICE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.ROCK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.STEEL] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.STEEL, (int)PokemonTypes.Types.WATER] = 0.5f;
		
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.BUG] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.DARK] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.DRAGON] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.ELECTRIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.FAIRY] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.FIGHTING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.FIRE] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.FLYING] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.GHOST] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.GRASS] = 0.5f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.GROUND] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.ICE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.NONE] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.NORMAL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.POISON] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.PSYCHIC] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.ROCK] = 2.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.STEEL] = 1.0f;
		typeToTypeDamageRatios[(int)PokemonTypes.Types.WATER, (int)PokemonTypes.Types.WATER] = 0.5f;
#endregion
	}

	public int CalculateAttackDamage(int movePower, bool critHit, PokemonTypes.Types moveType, int level, int attackersATK, int targetsDEF,
	                                 PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                                 PokemonTypes.Types targetType02)
	{
		baseDamage = movePower;
		SetModifier(moveType, attackersType01, attackersType02, targetType01, targetType02, critHit);
		return (int)((((2 * level + 10) / (float)250) * ((float)attackersATK / (float)targetsDEF) * baseDamage + 2) * modifier);
	}
	
	public int CalculateSpecialAttackDamage(int movePower, bool critHit, PokemonTypes.Types moveType, int level, int attackersSPATK, int targetsSPDEF,
	                                        PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                                        PokemonTypes.Types targetType02)
	{
		baseDamage = movePower;
		SetModifier(moveType, attackersType01, attackersType02, targetType01, targetType02, critHit);
		return (int)((((2 * level + 10) / (float)250) * ((float)attackersSPATK / (float)targetsSPDEF) * baseDamage + 2) * modifier);
	}
	/*
	public int CalculateConfusionDamage(int level, int attack, int defense){
		baseDamage = 40;
		return (int)(((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2);
	}

	public int CalculateDefenseCurlDamage(Move usedMove, int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                      BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD){
		baseDamage = usedMove.power * 2;
		SetModifier(usedMove.type, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculateSkyAttackDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                    BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 140;
		SetModifier(BasePokemon.TypesList.FLYING, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculateSolarBeamDamage(int level, int spAttack, int spDefense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                    BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 120;
		SetModifier(BasePokemon.TypesList.GRASS, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)spAttack / (float)spDefense) * baseDamage + 2) * modifier);
	}

	public int CalculateSkullBashDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                    BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 130;
		SetModifier(BasePokemon.TypesList.NORMAL, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculateRazorWindDamage(int level, int spAttack, int spDefense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                    BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 80;
		SetModifier(BasePokemon.TypesList.NORMAL, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)spAttack / (float)spDefense) * baseDamage + 2) * modifier);
	}

	public int CalculateFlyDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                                    BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 90;
		SetModifier(BasePokemon.TypesList.FLYING, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculateBounceDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 85;
		SetModifier(BasePokemon.TypesList.FLYING, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculateSkyDropDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 60;
		SetModifier(BasePokemon.TypesList.FLYING, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}
	public int CalculateDigDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 80;
		SetModifier(BasePokemon.TypesList.GROUND, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}
	public int CalculateDiveDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 80;
		SetModifier(BasePokemon.TypesList.WATER, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}
	public int CalculateShadowForceDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 120;
		SetModifier(BasePokemon.TypesList.GHOST, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}

	public int CalculatePhantomForceDamage(int level, int attack, int defense, BasePokemon.TypesList attackersType01, BasePokemon.TypesList attackersType02,
	                              BasePokemon.TypesList targetType01, BasePokemon.TypesList targetType02, int attackersBaseSPD, Move usedMove){
		baseDamage = 90;
		SetModifier(BasePokemon.TypesList.GHOST, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, usedMove);
		return (int)((((2 * level + 10) / (float)250) * ((float)attack / (float)defense) * baseDamage + 2) * modifier);
	}
*/

	
	private void SetModifier(PokemonTypes.Types moveType, PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                         PokemonTypes.Types targetType02, bool critHit)
	{
		//Other is dependant on equipped items, abilities, and field advantages.
		stab1 = DetermineSTAB01(moveType, attackersType01);
		stab2 = DetermineSTAB02(moveType, attackersType02);
		te1 = DetermineTypeEffectiveness01(moveType, targetType01);
		te2 = DetermineTypeEffectiveness02(moveType, targetType02);
		if(critHit)
		{
			crit = 1.5f;
		}
		else
		{
			crit = 1.0f;
		}
		modifier = ((stab1 * stab2) * (te1 * te2) * crit * /*other*/ Random.Range(0.85f, 1.0f));
	}
	private float DetermineSTAB01(PokemonTypes.Types moveType, PokemonTypes.Types pokemonType01)
	{
		if(moveType == pokemonType01)
		{
			return 1.5f;
		}
		else
		{
			return 1.0f;
		}
	}
	private float DetermineSTAB02(PokemonTypes.Types moveType, PokemonTypes.Types pokemonType02)
	{
		if(moveType == pokemonType02)
		{
			return 1.5f;
		}
		else
		{
			return 1.0f;
		}
	}
	public bool DetermineCritical(int baseSpeed, bool highCritChance)
	{
		if(highCritChance)
		{
			chance = (baseSpeed / 64);
		}
		else
		{
			chance = (baseSpeed / 512);
		}
		float random = Random.Range(1, 101);
		if(random <= chance)
		{
			return  true;
		}
		else
		{
			return false;
		}
	}
	private float DetermineTypeEffectiveness01(PokemonTypes.Types atkType, PokemonTypes.Types pkmnType01)
	{
		return (float) (typeToTypeDamageRatios[(int)atkType, (int)pkmnType01]);
	}
	private float DetermineTypeEffectiveness02(PokemonTypes.Types atkType, PokemonTypes.Types pkmnType02)
	{
		return (float) (typeToTypeDamageRatios[(int)atkType, (int)pkmnType02]);
	}
}
