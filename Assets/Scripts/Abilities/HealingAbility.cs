using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/HealingAbility")]
public class HealingAbility : AbilityBase {

    protected override void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target) {
        float healingAmount = CalcHealing(user);
        target.Heal(healingAmount);
        uiController.DisplayActionTaken($"{user.CharData.Name} healed {target.CharData.Name} with {AbilityName} for {healingAmount} hit points");
        AbilityOver();
    }

    protected override void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets) {
        float healingAmount = CalcHealing(user);
        foreach (var t in targets) {
            t.Heal(healingAmount);
        }
        uiController.DisplayActionTaken($"{user.CharData.Name} healed all allies with {AbilityName} for {healingAmount} hit points");
        AbilityOver();
    }

    //uses a similar equation to Pokemon
    private float CalcHealing(CharacterControllerBase user) {
        return (user.GetStat(StatToUse) * Power) / 100;
    }
}