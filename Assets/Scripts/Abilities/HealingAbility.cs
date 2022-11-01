using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HealingAbility")]
public class HealingAbility : AbilityBase {

    protected override void ApplyAbility(PlayerCharacter user, CharacterController target) {
        float healingAmount = CalcHealing(user);
        target.Heal(healingAmount);
        AbilityOver();
    }

    protected override void ApplyAbility(PlayerCharacter user, List<CharacterController> targets) {
        float healingAmount = CalcHealing(user);
        foreach (var t in targets) {
            t.Heal(healingAmount);
        }
        AbilityOver();
    }

    //uses a similar equation to Pokemon
    private float CalcHealing(PlayerCharacter user) {
        return (user.GetStat(CharacterStat.Healing) * Power) / 100;
    }
}