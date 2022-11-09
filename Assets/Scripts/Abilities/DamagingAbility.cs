using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DamagingAbility")]
public class DamagingAbility : AbilityBase {

    protected override void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target) {
        float damageAmount = CalcDamage(user, target);
        uiController.DisplayActionTaken($"{user.CharData.Name} attacked {target.CharData.Name} with {AbilityName} for {damageAmount} damage");
        target.TakeDamage(damageAmount);
        AbilityOver();
    }

    protected override void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets) {
        uiController.DisplayActionTaken($"{user.CharData.Name} attacked all enemies with {AbilityName}");
        foreach (var t in targets) {
            float damageAmount = CalcDamage(user, t);
            t.TakeDamage(damageAmount);
        }
        AbilityOver();
    }

    //uses a similar equation to Pokemon
    private float CalcDamage(CharacterControllerBase user, CharacterControllerBase target) {
        float defense = target.CharData.DefenseStat;
        if(defense == 0) defense = 1;
        return (user.GetStat(StatToUse)/defense * Power) / 100;
    }
}