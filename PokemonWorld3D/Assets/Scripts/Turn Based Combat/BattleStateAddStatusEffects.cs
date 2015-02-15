using UnityEngine;
using System.Collections;

public class BattleStateAddStatusEffects {


	public string CheckAbilityForStatusEffects(Move usedMove, StatusEffect effect, Pokemon user, Pokemon target){
		string dialogueText = "";
		if(effect.statusEffectName == "Accuracy Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.accuracyStage > -6){
					target.accuracyStage += effect.stagesToChange;
					if(target.accuracyStage < -6){
						target.accuracyStage = -6;
					}
					target.accuracy = ChangeAccEvaTo(target.accuracyStage);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "accuracy");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s accuracy won't go any lower!";
				}
			}
		}
		if(effect.statusEffectName == "Accuracy Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.accuracyStage < 6){
					target.accuracyStage += effect.stagesToChange;
					if(target.accuracyStage > 6){
						target.accuracyStage = 6;
					}
					target.accuracy = ChangeAccEvaTo(target.accuracyStage);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "accuracy");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s accuracy won't go any higher!";
				}
			}
		}
		if(effect.statusEffectName == "Aqua Ring"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.aquaRing){
					user.aquaRing = true;
					dialogueText = "\n" + user.pokemonName + " has been surrounded by a ring of water.";
				}
			}
		}
		if(effect.statusEffectName == "Attack Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.atkStage > -6){
					target.atkStage += effect.stagesToChange;
					if(target.atkStage < -6){
						target.atkStage = -6;
					}
					target.curATK = ChangeStatTo(target.atkStage, target.maxATK);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "attack");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s attack won't go any lower!";
				}
			}
		}
		if(effect.statusEffectName == "Attack Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.atkStage < 6){
					target.atkStage += effect.stagesToChange;
					if(target.atkStage > 6){
						target.atkStage = 6;
					}
					target.curATK = ChangeStatTo(target.atkStage, target.maxATK);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "attack");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s attack won't go any higher!";;
				}
			}
		}
		if(effect.statusEffectName == "Badly Poison"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.BADLY_POISONED){
					if(target.type01 != BasePokemon.TypesList.POISON && target.type02 != BasePokemon.TypesList.POISON && target.type01 != BasePokemon.TypesList.STEEL
					   && target.type02 != BasePokemon.TypesList.STEEL && target.ability01 != "Immunity" && target.ability02 != "Immunity"){
						target.statusCondition = BasePokemon.NonVolatileStatusConditionList.BADLY_POISONED;
						dialogueText = "\n" + target.pokemonName + " has become badly poisoned!";
					}
				}
			}
		}
		if(effect.statusEffectName == "Bracing"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.bracing){
					user.bracing = true;
				}
			}
		}
		if(effect.statusEffectName == "Burn"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.BURNED){
					if(target.type01 != BasePokemon.TypesList.FIRE && target.type02 != BasePokemon.TypesList.FIRE && target.ability01 != "Water Veil"
					   && target.ability02 != "Water Veil"){
						target.statusCondition = BasePokemon.NonVolatileStatusConditionList.BURNED;
						dialogueText = "\n" + target.pokemonName + " has been burned.";
					}
				}
			}
		}
		if(effect.statusEffectName == "Confusion"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.confusion){
					if(target.ability01 != "Own Tempo" && target.ability02 != "Own Tempo"){
						target.confusion = true;
						target.confusionTurns = Random.Range(1,5);
						dialogueText = "\n" + target.pokemonName + " has become confused.";
					}
				}
			}
		}
		if(effect.statusEffectName == "Curse"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.curse){
					target.curse = true;
					dialogueText = "\n" + target.pokemonName + " was cursed.";
				}
			}
		}
		if(effect.statusEffectName == "Defense Curl"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.defenseCurl){
					user.defenseCurl = true;
				}
			}
		}
		if(effect.statusEffectName == "Defense Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.defStage > -6){
					target.defStage += effect.stagesToChange;
					if(target.defStage < -6){
						target.defStage = -6;
					}
					target.curDEF = ChangeStatTo(target.defStage, target.maxDEF);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "defense");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s defense won't go any lower!";
				}
			}
		}
		if(effect.statusEffectName == "Defense Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.defStage < 6){
					target.defStage += effect.stagesToChange;
					if(target.defStage > 6){
						target.defStage = 6;
					}
					target.curDEF = ChangeStatTo(target.defStage, target.maxDEF);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "defense");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s defense won't go any higher!";;
				}
			}
		}
		if(effect.statusEffectName == "Embargo"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.embargo){
					target.embargo = true;
					target.embargoTurns = 5;
				}
			}
		}
		if(effect.statusEffectName == "Evasion Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.evasionStage > -6){
					target.evasionStage += effect.stagesToChange;
					if(target.evasionStage < -6){
						target.evasionStage = -6;
					}
					target.evasion = ChangeAccEvaTo(target.evasionStage);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "evasion");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s evasion won't go any lower!";
				}
			}
		}
		if(effect.statusEffectName == "Evasion Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.evasionStage < 6){
					target.evasionStage += effect.stagesToChange;
					if(target.evasionStage > 6){
						target.evasionStage = 6;
					}
					target.evasion = ChangeAccEvaTo(target.evasionStage);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "evasion");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s evasion won't go any higher!";
				}
			}
		}
		if(effect.statusEffectName == "Flinch"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.ability01 != "Inner Focus" && target.ability02 != "Inner Focus"){
					target.flinch = true;
				}
			}
		}
		if(effect.statusEffectName == "Freeze"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.FROZEN){
					if(target.type01 != BasePokemon.TypesList.ICE && target.type02 != BasePokemon.TypesList.ICE){
						target.statusCondition = BasePokemon.NonVolatileStatusConditionList.FROZEN;
						dialogueText = "\n" + target.pokemonName + " has been frozen.";
					}
				}
			}
		}
		if(effect.statusEffectName == "Glowing"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.glowing){
					user.glowing = true;
				}
			}
		}
		if(effect.statusEffectName == "Heal Block"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.healBlock){
					target.healBlock = true;
					target.healBlockTurns = 5;
					dialogueText = "\n" + target.pokemonName + " seems to have become unable to heal itself.";
				}
			}
		}
		if(effect.statusEffectName == "Infatuation"){
			if(TryToApplyStatusEffect(usedMove)){
				if(user.sex != target.sex){
					if(!target.infatuation && target.ability01 != "Oblivious" && target.ability02 != "Oblivious"){
						target.infatuation = true;
						dialogueText = "\n" + target.pokemonName + " has become infatuated with " + user.pokemonName + ".";
					}
				}
			}
		}
		if(effect.statusEffectName == "Magic Coat"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.magicCoat){
					user.magicCoat = true;
					dialogueText = "\n" + user.pokemonName + " has become covered in a coat of magic.";
				}
			}
		}
		if(effect.statusEffectName == "Magnetic Levitation"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.magneticLevitation && user.equippedItem.name != "Iron Ball" && !user.rooting){
					user.magneticLevitation = true;
					user.magneticLevitationTurns = 5;
				}
			}
		}
		if(effect.statusEffectName == "Minimize"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.minimize){
					user.minimize = true;
				}
			}
		}
		if(effect.statusEffectName == "Nightmare"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition == BasePokemon.NonVolatileStatusConditionList.SLEEP && !target.nightmare){
					target.nightmare = true;
					dialogueText = "\n" + target.pokemonName + " seems to be having nightmares in its sleep.";
				}
			}
		}
		if(effect.statusEffectName == "Paralyze"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.PARALYZED){
					if(target.type01 != BasePokemon.TypesList.ELECTRIC && target.type02 != BasePokemon.TypesList.ELECTRIC){
						target.statusCondition = BasePokemon.NonVolatileStatusConditionList.PARALYZED;
						dialogueText = "\n" + target.pokemonName + " has become paralyzed.";
					}
				}
			}
		}
		if(effect.statusEffectName == "Partially Trapped"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.partiallyTrapped && target.type01 != BasePokemon.TypesList.GHOST && target.type02 != BasePokemon.TypesList.GHOST){
					target.partiallyTrapped = true;
					if(user.equippedItem.name != "Grip Claw"){
						target.partiallyTrappedTurns = Random.Range(4,6);
					}else{
						target.partiallyTrappedTurns = 7;
					}
				}
			}
		}
		if(effect.statusEffectName == "Perish Song"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.perishSong && target.ability01 != "Soundproof" && target.ability02 != "Soundproof"){
					target.perishSong = true;
					target.perishSongTurnCountDown = 3;
					dialogueText = "\n" + target.pokemonName + " has heard the song of its impending doom.";
				}
			}
		}
		if(effect.statusEffectName == "Poison"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.POISONED){
					if(target.type01 != BasePokemon.TypesList.POISON && target.type02 != BasePokemon.TypesList.POISON && target.type01 != BasePokemon.TypesList.STEEL
					   && target.type02 != BasePokemon.TypesList.STEEL && target.ability01 != "Immunity" && target.ability02 != "Immunity"){
						target.statusCondition = BasePokemon.NonVolatileStatusConditionList.POISONED;
						dialogueText = "\n" + target.pokemonName + " has become poisoned!";
					}
				}
			}
		}
		if(effect.statusEffectName == "Protection"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.protection){
					user.protection = true;
					dialogueText = "\n" + user.pokemonName + " has protected itself.";
				}
			}
		}
		if(effect.statusEffectName == "Recharging"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.recharging){
					user.recharging = true;
				}
			}
		}
		if(effect.statusEffectName == "Rooting"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.rooting){
					if(user.telekineticLevitation){
						user.telekineticLevitation = false;
					}
					if(user.magneticLevitation){
						user.magneticLevitation = false;
					}
					user.rooting = true;
					dialogueText = "\n" + user.pokemonName + " has become rooted to the ground.";
				}
			}
		}
		if(effect.statusEffectName == "Seeding"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.seeding){
					target.seeding = true;
					dialogueText = "\n" + target.pokemonName + " has been seeded.";
					}
				}
		}
		if(effect.statusEffectName == "Semi-invulnerable"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.semiInvulnerable){
					user.semiInvulnerable = true;
				}
			}
		}
		if(effect.statusEffectName == "Sleep"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.statusCondition != BasePokemon.NonVolatileStatusConditionList.SLEEP){
					target.statusCondition = BasePokemon.NonVolatileStatusConditionList.SLEEP;
					target.sleepTurns = Random.Range(1,4);
					dialogueText = "\n" + target.pokemonName + " has fallen asleep";
				}
			}
		}
		if(effect.statusEffectName == "Special Attack Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spatkStage > -6){
					target.spatkStage += effect.stagesToChange;
					if(target.spatkStage < -6){
						target.spatkStage = -6;
					}
					target.curSPATK = ChangeStatTo(target.spatkStage, target.maxSPATK);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "special attack");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s special attack won't go any lower!";
				}
			}
		}
		if(effect.statusEffectName == "Special Attack Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spatkStage < 6){
					target.spatkStage += effect.stagesToChange;
					if(target.spatkStage > 6){
						target.spatkStage = 6;
					}
					target.curSPATK = ChangeStatTo(target.spatkStage, target.maxSPATK);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "special attack");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s special attack won't go any higher!";;
				}
			}
		}
		if(effect.statusEffectName == "Special Defense Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spdefStage < 6){
					target.spdefStage += effect.stagesToChange;
					if(target.spdefStage > 6){
						target.spdefStage = 6;
					}
					target.curSPDEF = ChangeStatTo(target.spdefStage, target.maxSPDEF);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "special defense");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s special defense won't go any lower!";;
				}
			}
		}
		if(effect.statusEffectName == "Special Defense Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spdefStage < 6){
					target.spdefStage += effect.stagesToChange;
					if(target.spdefStage > 6){
						target.spdefStage = 6;
					}
					target.curSPDEF = ChangeStatTo(target.spdefStage, target.maxSPDEF);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "special defense");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s special defense won't go any higher!";;
				}
			}
		}
		if(effect.statusEffectName == "Speed Down"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spdStage < 6){
					target.spdStage += effect.stagesToChange;
					if(target.spdStage > 6){
						target.spdStage = 6;
					}
					target.curSPD = ChangeStatTo(target.spdStage, target.maxSPD);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "speed");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s speed won't go any lower!";;
				}
			}
		}
		if(effect.statusEffectName == "Speed Up"){
			if(TryToApplyStatusEffect(usedMove)){
				if(target.spdStage < 6){
					target.spdStage += effect.stagesToChange;
					if(target.spdStage > 6){
						target.spdStage = 6;
					}
					target.curSPD = ChangeStatTo(target.spdStage, target.maxSPD);
					dialogueText = StatChangeDialogue(effect.stagesToChange, target.pokemonName, "speed");
				}else{
					dialogueText = "\n" + target.pokemonName + "'s speed won't go any higher!";;
				}
			}
		}
		if(effect.statusEffectName == "Substitute"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.substitute){
					user.substitute = true;
					user.substituteHP = (int)((float)user.curMaxHP * 0.25f);
				}
			}
		}
		if(effect.statusEffectName == "Taking Aim"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.takingAim){
					user.takingAim = true;
					dialogueText = "\n" + user.pokemonName + " took aim.";
				}
			}
		}
		if(effect.statusEffectName == "Taking In Sunlight"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.takingInSunlight){
					user.takingInSunlight = true;
				}
			}
		}
		if(effect.statusEffectName == "Taunt"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.taunt && target.ability01 != "Aroma Veil" && target.ability02 != "Aroma Veil"){
					target.taunt = true;
					target.tauntTurns = 3;
					dialogueText = "\n" + target.pokemonName + " was taunted by " + user.pokemonName + ".";
				}
			}
		}
		if(effect.statusEffectName == "Telekinetic Levitation"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.telekineticLevitation && user.equippedItem.name != "Iron Ball" && !user.rooting){
					user.telekineticLevitation = true;
					user.telekineticLevitationTurns = 3;
				}
			}
		}
		if(effect.statusEffectName == "Thaw"){
			if(TryToApplyStatusEffect(usedMove)){
				user.statusCondition = BasePokemon.NonVolatileStatusConditionList.NONE;
				if(target.statusCondition == BasePokemon.NonVolatileStatusConditionList.FROZEN){
					target.statusCondition = BasePokemon.NonVolatileStatusConditionList.NONE;
					dialogueText = "\n Both" + user.pokemonName + target.pokemonName + " were thawed out!";
				}else{
					dialogueText = "\n" + user.pokemonName + " thawed out!";
				}
			}
		}
		if(effect.statusEffectName == "Torment"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.torment){
					target.torment = true;
					dialogueText = "\n" + target.pokemonName + " was tormented by " + user.pokemonName + "."; 
				}
			}
		}
		if(effect.statusEffectName == "Traped"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!target.trapped && target.type01 != BasePokemon.TypesList.GHOST && target.type02 != BasePokemon.TypesList.GHOST){
					target.trapped = true;
				}
			}
		}
		if(effect.statusEffectName == "Withdrawing"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.withdrawing){
					user.withdrawing = true;
				}
			}
		}
		if(effect.statusEffectName == "Whipping Up A Whirlwind"){
			if(TryToApplyStatusEffect(usedMove)){
				if(!user.whippingUpAWhirlwind){
					user.whippingUpAWhirlwind = true;
				}
			}
		}
		return dialogueText;
	}

	private bool TryToApplyStatusEffect(Move usedMove){
		int randomTemp = Random.Range(1, 101);
		if(randomTemp <= usedMove.status_effect_success_rate){
			return true;
		}
		return false;
	}
	private int ChangeStatTo(int statStage, int maxStat){
		if(statStage <= -6){
			return (int)((float)maxStat * 0.25f);
		}else if(statStage == -5){
			return (int)((float)maxStat * 0.2857142857f);
		}else if(statStage == -4){
			return (int)((float)maxStat * 0.3333333333f);
		}else if(statStage == -3){
			return (int)((float)maxStat * 0.4f);
		}else if(statStage == -2){
			return (int)((float)maxStat * 0.5f);
		}else if(statStage == -1){
			return (int)((float)maxStat * 0.6666666667f);
		}else if(statStage == 1){
			return (int)((float)maxStat * 1.5f);
		}else if(statStage == 2){
			return (int)((float)maxStat * 2f);
		}else if(statStage == 3){
			return (int)((float)maxStat * 2.5f);
		}else if(statStage == 4){
			return (int)((float)maxStat * 3f);
		}else if(statStage == 5){
			return (int)((float)maxStat * 3.5f);
		}else if(statStage >= 6){
			return (int)((float)maxStat * 4f);
		}else{
			return (int)((float)maxStat * 1f);
		}

	}
	private float ChangeAccEvaTo(int statStage){
		if(statStage <= -6){
			return 0.3333333333f;
		}else if(statStage == -5){
			return 0.375f;
		}else if(statStage == -4){
			return 0.4285714286f;
		}else if(statStage == -3){
			return 0.5f;
		}else if(statStage == -2){
			return 0.6f;
		}else if(statStage == -1){
			return 0.75f;
		}else if(statStage == 1){
			return 1.3333333333f;
		}else if(statStage == 2){
			return 1.6666666667f;
		}else if(statStage == 3){
			return 2f;
		}else if(statStage == 4){
			return 2.3333333333f;
		}else if(statStage == 5){
			return 2.6666666667f;
		}else if(statStage == 6){
			return 3f;
		}else{
			return 1f;
		}
	}
	private string StatChangeDialogue(int stagesChanged, string pokemonName, string statToChange){
		if(stagesChanged <= -3){
			return "\n" + pokemonName + "'s" + " " + statToChange + " severely fell!";
		}else if(stagesChanged == -2){
			return "\n" + pokemonName + "'s" + " " + statToChange + " harshly fell!";
		}else if(stagesChanged == -1){
			return "\n" + pokemonName + "'s" + " " + statToChange + " fell!";
		}else if(stagesChanged == 1){
			return "\n" + pokemonName + "'s" + " " + statToChange + " rose!";
		}else if(stagesChanged == 2){
			return "\n" + pokemonName + "'s" + " " + statToChange + " rose sharply!";
		}else if(stagesChanged >= 3){
			return "\n" + pokemonName + "'s" + " " + statToChange + " rose drastically!";
		}else{
			return "";
		}
	}

}
