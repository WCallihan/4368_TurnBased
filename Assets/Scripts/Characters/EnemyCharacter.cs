using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : CharacterControllerBase {

    private UIController uiController;
    private bool grappled = false;

    public bool Grappled => grappled;

    public event Action EnemyCharacterTurnOver;

    private void OnEnable() {
        PlayerTurnState.PlayerTurnsStarted += ResetGrappled;
    }

    private void OnDisable() {
        PlayerTurnState.PlayerTurnsStarted -= ResetGrappled;
    }

    private void Start() {
        uiController = FindObjectOfType<UIController>();
    }

    public IEnumerator PrepAttack() {
        uiController.DisplayActionTaken($"{charData.Name} is choosing a target...");
        yield return new WaitForSecondsRealtime(2);

        Animator.SetTrigger("UseAbility");
    }

    //called by the attack animation at the right frame
    public void ApplyAttack() {
        var target = ChooseTarget();
        float damage = ((charData.AttackStat) * 50) / 100;

        //if the enemy is grappled, distribute the damage between them and the target
        if (grappled) {
			float halvedDamage = damage / 2;
            TakeDamage(halvedDamage);
            target.TakeDamage(halvedDamage);
			uiController.DisplayActionTaken($"{charData.Name} attacked {target.CharData.Name} and themselves for {halvedDamage} damage");
		} else {
            target.TakeDamage(damage);
			uiController.DisplayActionTaken($"{charData.Name} attacked {target.CharData.Name} for {damage} damage");
		}

        EnemyCharacterTurnOver?.Invoke();
    }

    //randomly choose a target scaled based on their hit chance
    private PlayerCharacter ChooseTarget() {
        PlayerCharacter[] playerCharacters = FindObjectsOfType<PlayerCharacter>();
        List<PlayerCharacter> possibleTargets = new List<PlayerCharacter>();
        //make a new list with all non-dead player characters
        foreach (var p in playerCharacters) {
            if (!p.Dead) possibleTargets.Add(p);
        }

        int hitChancePool = 0;
        List<int> hitChances = new List<int>();

        //add all the hit chances into one pool and put the increments in an array corresponding to each character
        for (int i = 0; i < possibleTargets.Count; i++) {
            hitChancePool += possibleTargets[i].HitChance;
            hitChances.Add(hitChancePool);
        }

        //randomly choose a number in the pool, the higher the character's hit chance, the more likely that this number is within its range
        int rand = UnityEngine.Random.Range(0, hitChancePool + 1);

        //find which range the random number lies between and return that target
        for (int i = 0; i < hitChances.Count; i++) {
            if (rand <= hitChances[i]) {
                return possibleTargets[i];
            }
        }

        return null; //this should throw an error later, which is good
    }

    public void SetGrappled(bool grapple) {
        grappled = grapple;
        //TODO: update UI
    }

    //reset the grappled status at the beginning of the player's turns
    private void ResetGrappled() {
        SetGrappled(false);
    }
}