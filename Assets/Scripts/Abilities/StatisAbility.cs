using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/StatisAbility")]
public class StatisAbility : AbilityBase {

    protected override void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target) {
        throw new System.NotImplementedException();
    }

    protected override void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets) {
        throw new System.NotImplementedException();
    }
}