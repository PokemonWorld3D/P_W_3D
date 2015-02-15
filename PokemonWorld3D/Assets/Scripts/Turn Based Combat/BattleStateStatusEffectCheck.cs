using UnityEngine;
using System.Collections;

public class BattleStateStatusEffectCheck {

	private int damage;

	public string AfterAttackStatusEffects(Pokemon pokemon){
		string afterAttackDialogueText = "";
		if(pokemon.statusCondition == BasePokemon.NonVolatileStatusConditionList.BADLY_POISONED){
			damage = BadlyPoisonDamageCalculation(pokemon);
			if(pokemon.ability01 == "Poison Heal" || pokemon.ability02 == "Poison Heal" && !pokemon.healBlock){
				pokemon.curHP += damage;
				afterAttackDialogueText = "\n" + pokemon.pokemonName + " was healed by its poison!";
			}else{
				pokemon.curHP -= damage;
				afterAttackDialogueText = "\n" + pokemon.pokemonName + " was hurt by its poison!";
			}
			if(pokemon.curHP < 0){
				pokemon.curHP = 0;
			}
			pokemon.badlyPoisonedTurnCounter += 1;
  		}
		if(pokemon.statusCondition == BasePokemon.NonVolatileStatusConditionList.BURNED){
			damage = BurnDamageCalculation(pokemon);
			pokemon.curHP -= damage;
			afterAttackDialogueText = "\n" + pokemon.pokemonName + " was hurt by its burn!";
			if(pokemon.curHP < 0){
				pokemon.curHP = 0;
			}
  		}
		if(pokemon.statusCondition == BasePokemon.NonVolatileStatusConditionList.POISONED){
			damage = PoisonDamageCalculation(pokemon);
			if(pokemon.ability01 == "Poison Heal" || pokemon.ability02 == "Poison Heal" && !pokemon.healBlock){
				pokemon.curHP += damage;
				afterAttackDialogueText = "\n" + pokemon.pokemonName + " was healed by its poison!";
			}else{
				pokemon.curHP -= damage;
				afterAttackDialogueText = "\n" + pokemon.pokemonName + " was hurt by its poison!";
			}
			if(pokemon.curHP < 0){
				pokemon.curHP = 0;
			}
  		}
		if(pokemon.statusCondition == BasePokemon.NonVolatileStatusConditionList.SLEEP){
			pokemon.sleepTurns -= 1;
		}
		if(pokemon.confusion){
			pokemon.confusionTurns -= 1;
		}
		if(pokemon.embargo){
			pokemon.embargoTurns -= 1;
		}
		return afterAttackDialogueText;
	}

	public string EndOfTurnStatusEffects(Pokemon pokemon, Pokemon otherPokemon){
		string endOfTurnDialogueText = "";
		if(pokemon.curse){
			pokemon.curMaxHP = (int)((float)pokemon.curMaxHP * 0.25f);
		}
		if(pokemon.flinch){
			pokemon.flinch = false;
		}
		if(pokemon.healBlock){
			pokemon.healBlockTurns -= 1;
			if(pokemon.healBlockTurns == 0){
				pokemon.healBlock = false;
				endOfTurnDialogueText = "\n" + pokemon.pokemonName + " has regained the ability to heal itself.";
			}
		}
		if(pokemon.nightmare){
			pokemon.maxHP = (int)((float)pokemon.maxHP * 0.25f);
		}
		if(pokemon.partiallyTrapped){
			if(otherPokemon.equippedItem.name != "Binding Band"){
				pokemon.curHP -= (int)((float)pokemon.curMaxHP * 0.125f);
				endOfTurnDialogueText = "\n" + pokemon.pokemonName + " was hurt from being bound by" + otherPokemon.pokemonName + "!";
				pokemon.partiallyTrappedTurns -= 1;
				if(pokemon.partiallyTrappedTurns == 0){
					pokemon.partiallyTrapped = false;
				}
			}else{
				pokemon.curHP -= (int)((float)pokemon.curMaxHP * 0.167f);
				endOfTurnDialogueText = "\n" + pokemon.pokemonName + " was hurt from being bound by" + otherPokemon.pokemonName + "!";
				pokemon.partiallyTrappedTurns -= 1;
				if(pokemon.partiallyTrappedTurns == 0){
					pokemon.partiallyTrapped = false;
				}
			}
		}
		if(pokemon.perishSong){
			if(pokemon.perishSongTurnCountDown != 0){
				pokemon.perishSongTurnCountDown -= 1;
			}else{
				pokemon.curHP = 0;
			}
		}
		if(pokemon.seeding){
			damage = SeedingDamageCalculation(pokemon);
			pokemon.curHP -= damage;
			if(pokemon.curHP < 0){
				pokemon.curHP = 0;
			}
			if(!otherPokemon.healBlock){
				otherPokemon.curHP += damage;
			}
		}
		if(pokemon.taunt){
			pokemon.tauntTurns -= 1;
			if(pokemon.tauntTurns == 0){
				pokemon.taunt = false;
			}
		}
		if(pokemon.telekineticLevitation){
			pokemon.telekineticLevitationTurns -= 1;
			if(pokemon.telekineticLevitationTurns == 0){
				pokemon.telekineticLevitation = false;
			}
		}
		if(pokemon.aquaRing){
			if(pokemon.equippedItem.name != "Big Root"){
				pokemon.curHP += (int)((float)pokemon.curMaxHP * 0.0625f);
				endOfTurnDialogueText = pokemon.pokemonName + " regained health from its Aqua Ring!";
			}else{
				pokemon.curHP += (int)(((float)pokemon.curMaxHP * 0.0625f) * 1.3f);
				endOfTurnDialogueText = pokemon.pokemonName + " regained health from its Aqua Ring!";
			}
		}
		if(pokemon.bracing){
			pokemon.bracing = false;
		}
		if(pokemon.magicCoat){
			pokemon.magicCoat = false;
		}
		if(pokemon.magneticLevitation){
			pokemon.magneticLevitationTurns -= 1;
			if(pokemon.magneticLevitationTurns == 0){
				pokemon.magneticLevitation = false;
			}
		}
		if(pokemon.protection){
			pokemon.protection = false;
		}
		if(pokemon.rooting){
			pokemon.curHP += (int)((float)pokemon.curMaxHP * 0.0625f);
			endOfTurnDialogueText = pokemon.pokemonName + " regained health from being rooted!";
		}
		return endOfTurnDialogueText;
	}

	public int BadlyPoisonDamageCalculation(Pokemon pokemon){
		float multiplier = (int)((float)pokemon.badlyPoisonedTurnCounter / 16);
		return (int)(pokemon.maxHP * multiplier);
	}
	public int BurnDamageCalculation(Pokemon pokemon){
		if(pokemon.ability01 == "Heatproof" || pokemon.ability02 == "Heatproof"){
			return (int)(pokemon.maxHP * 0.0625);
		}else{
			return (int)(pokemon.maxHP * 0.125f);
		}
	}
	public int PoisonDamageCalculation(Pokemon pokemon){
		return (int)(pokemon.maxHP * 0.125f);
	}
	public int SeedingDamageCalculation(Pokemon pokemon){
		return (int)(pokemon.maxHP * 0.125f);
	}

}
