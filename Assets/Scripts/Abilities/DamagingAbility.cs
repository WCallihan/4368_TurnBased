using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/DamagingAbility")]
public class DamagingAbility : AbilityBase {

    protected override void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target) {
        float damageAmount = CalcDamage(user, target);
        target.TakeDamage(damageAmount);
        AbilityOver();
    }

    protected override void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets) {
        foreach (var t in targets) {
            float damageAmount = CalcDamage(user, t);
            t.TakeDamage(damageAmount);
        }
        AbilityOver();
    }

    //uses a similar equation to Pokemon
    private float CalcDamage(CharacterControllerBase user, CharacterControllerBase target) {
        return (user.GetStat(StatToUse) * Power) / 100;
    }
}