using UnityEngine;
using System.Collections;

public class DamageCalculation {

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
	
	public int CalculateAttackDamage(int movePower, bool moveCrit, PokemonTypes.Types moveType, int level, int attackersATK, int targetsDEF,
	                                 PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                                 PokemonTypes.Types targetType02, int attackersBaseSPD){
		baseDamage = movePower;
		SetModifier(moveType, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, moveCrit);
		return (int)((((2 * level + 10) / (float)250) * ((float)attackersATK / (float)targetsDEF) * baseDamage + 2) * modifier);
	}
	
	public int CalculateSpecialAttackDamage(int movePower, bool moveCrit, PokemonTypes.Types moveType, int level, int attackersSPATK, int targetsSPDEF,
	                                        PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                                        PokemonTypes.Types targetType02, int attackersBaseSPD){
		baseDamage = movePower;
		SetModifier(moveType, attackersType01, attackersType02, targetType01, targetType02, attackersBaseSPD, moveCrit);
		return (int)((((2 * level + 10) / (float)250) * ((float)attackersSPATK / (float)targetsSPDEF) * baseDamage + 2) * modifier);
	}

	private void SetModifier(PokemonTypes.Types moveType, PokemonTypes.Types attackersType01, PokemonTypes.Types attackersType02, PokemonTypes.Types targetType01,
	                         PokemonTypes.Types targetType02, int attackersbaseSPD,
	                         bool moveCrit){
		//Other is dependant on equipped items, abilities, and field advantages.
		stab1 = DetermineSTAB01(moveType, attackersType01);
		stab2 = DetermineSTAB02(moveType, attackersType02);
		te1 = 1; //DetermineTypeEffectiveness01(moveType, targetType01);
		te2 = 1; //DetermineTypeEffectiveness02(moveType, targetType02);
		if(DetermineCritical(attackersbaseSPD, moveCrit)){
			crit = 1.5f;
		}else{
			crit = 1.0f;
		}
		modifier = ((stab1 * stab2) * (te1 * te2) * crit * /*other*/ Random.Range(0.85f, 1.0f));
	}
	private float DetermineSTAB01(PokemonTypes.Types moveType, PokemonTypes.Types pokemonType01){
		if(moveType == pokemonType01){
			return 1.5f;
		}else{
			return 1.0f;
		}
	}
	private float DetermineSTAB02(PokemonTypes.Types moveType, PokemonTypes.Types pokemonType02){
		if(moveType == pokemonType02){
			return 1.5f;
		}else{
			return 1.0f;
		}
	}
	private bool DetermineCritical(int baseSpeed, bool moveCrit){
		if(moveCrit){
			chance = (baseSpeed / 64);
		}else{
			chance = (baseSpeed / 512);
		}
		random = Random.Range(1, 101);
		if(random <= chance){
			return  true;
		}else{
			return false;
		}
	}
	/*private float DetermineTypeEffectiveness01(BasePokemon.TypesList atkType, BasePokemon.TypesList pkmnType01){
		return (float) (TurnBasedCombatStateMachine.typeToTypeDamageRatios[(int)atkType, (int)pkmnType01]);
	}
	private float DetermineTypeEffectiveness02(BasePokemon.TypesList atkType, BasePokemon.TypesList pkmnType02){
		return (float) (TurnBasedCombatStateMachine.typeToTypeDamageRatios[(int)atkType, (int)pkmnType02]);
	}*/

}
