using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/StatusAbility")]
public class StatusAbility : AbilityBase {

    [SerializeField] private ChangableStatus statusToChange;

    protected override void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target) {
        switch(statusToChange) {
            case ChangableStatus.HitChance:
                ApplyHitChanceChange(user, target.GetComponent<PlayerCharacter>());
                break;
            case ChangableStatus.Shielding:
                ApplyShielding(user, target.GetComponent<PlayerCharacter>());
                break;
            case ChangableStatus.Grappled:
                ApplyGrapple(user, target.GetComponent<EnemyCharacter>());
                break;
        }
		//characterAnimator.SetTrigger("UseAbility");
		AbilityOver();
    }

    protected override void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets) {
        if(statusToChange != ChangableStatus.Shielding) {
            Debug.LogError("This is only meant for Josh's Inspiration unless something changes.");
            return;
        }

        List<PlayerCharacter> playerTargets = new List<PlayerCharacter>();
        foreach (var t in targets) {
            playerTargets.Add(t.GetComponent<PlayerCharacter>());
        }
        ApplyShielding(user, playerTargets);
		//characterAnimator.SetTrigger("UseAbility");
		AbilityOver();
    }

    private void ApplyHitChanceChange(CharacterControllerBase user, PlayerCharacter target) {
        uiController.DisplayActionTaken($"{user.CharData.Name} decreased their hit chance by {Power}%");
        target.ChangeHitChance((int)Power);
    }

    private void ApplyShielding(CharacterControllerBase user, PlayerCharacter target) {
        float shielding = CalcShielding(user);
        uiController.DisplayActionTaken($"{user.CharData.Name} applied {shielding} shielding to {target.CharData.Name}");
        target.SetShielding(shielding);
    }

    private void ApplyShielding(CharacterControllerBase user, List<PlayerCharacter> targets) {
        float shielding = CalcShielding(user);
        uiController.DisplayActionTaken($"{user.CharData.Name} applied {shielding} shielding to all allies");
        foreach(var t in targets) {
            t.SetShielding(shielding);
        }
    }

    private float CalcShielding(CharacterControllerBase user) {
        return (user.GetStat(StatToUse) * Power) / 100;
    }

    private void ApplyGrapple(CharacterControllerBase user, EnemyCharacter target) {
        uiController.DisplayActionTaken($"{user.CharData.Name} grappled {target.CharData.Name}");
        target.SetGrappled(true);
    }
}