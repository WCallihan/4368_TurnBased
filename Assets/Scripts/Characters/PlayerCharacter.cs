using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterControllerBase {

	private AbilityBase abilityToUse;
	private int baseHitChance;
    private int hitChance = 100;

    public int HitChance => hitChance;

	public static event Action<CharacterControllerBase, float> PlayerHitChanceChange;

	private void OnEnable() {
        PlayerTurnState.PlayerTurnsStarted += ResetHitChance;
    }

    private void OnDisable() {
        PlayerTurnState.PlayerTurnsStarted -= ResetHitChance;	
	}

    public void SetBaseHitChance(int chance) {
        baseHitChance = chance;
        ResetHitChance();
    }

    public void ChangeHitChance(int chance) {
        hitChance += chance;
		PlayerHitChanceChange?.Invoke(this, chance);
    }

    //reset hit chance at the beginning of the player's turns
    private void ResetHitChance() {
        hitChance = baseHitChance;
    }

	public void StartAbility(AbilityBase ability) {
		abilityToUse = ability;
		abilityToUse.UseAbility(this, Animator);
	}

	//called in as an animation event in the UseAbility animation
	public void ApplyAbility() {
		abilityToUse.ApplyAbility();
	}
}