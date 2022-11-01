using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityTarget { Self, OneAlly, AllAllies, OneEnemy, AllEnemies }

public abstract class AbilityBase : ScriptableObject {

    [SerializeField] private string abilityName;
    [SerializeField] private string abilityDescription;
    [SerializeField] private AbilityTarget target;
    [SerializeField] private CharacterStat statToUse;
    [SerializeField] private float power;

    public string AbilityName => abilityName;
    public string AbilityDescription => abilityDescription;
    public AbilityTarget Target => target;
    public CharacterStat Stat => statToUse;
    public float Power => power;

    private PlayerCharacter user;

    public static event Action<bool> StartTargetSelection;
    public static event Action EndCharacterTurn;

    public void UseAbility(PlayerCharacter abilityUser) {
        user = abilityUser;
        switch(target) {
            case AbilityTarget.Self:
                ApplyAbility(user, user);
                break;
            case AbilityTarget.OneAlly:
                SelectSingleTarget(true);
                break;
            case AbilityTarget.OneEnemy:
                SelectSingleTarget(false);
                break;
            case AbilityTarget.AllAllies:
                SelectMultipleTargets(true);
                break;
            case AbilityTarget.AllEnemies:
                SelectMultipleTargets(false);
                break;
        }
    }
    
    private void SelectSingleTarget(bool allies) {
        //invoke static event to tell UI to turn on target selection UI
        StartTargetSelection?.Invoke(allies);
        //subscribe to CharacterController static event to see when they're clicked
        CharacterController.CharacterTargeted += SetSingleTarget;
    }

    private void SetSingleTarget(CharacterController singleTarget) {
        //apply the ability to the target
        ApplyAbility(user, singleTarget);
        //unsubscribe from the event just to be safe
        CharacterController.CharacterTargeted -= SetSingleTarget;
    }

    private void SelectMultipleTargets(bool allies) {
        List<CharacterController> multipleTargets = new List<CharacterController>();
        //get all character controllers in the scene
        CharacterController[] potentialTargets = FindObjectsOfType<CharacterController>();
        //check if the character controller is on the right layer, and then add to targets if so
        foreach (var t in potentialTargets) {
            if((t.gameObject.layer == LayerMask.NameToLayer("PlayerCharacter")) == allies) {
                multipleTargets.Add(t);
            }
        }
        ApplyAbility(user, multipleTargets);
    }

    //used for abilities that target one character
    protected abstract void ApplyAbility(PlayerCharacter user, CharacterController target);

    //used for abilities that target multiple characters
    protected abstract void ApplyAbility(PlayerCharacter user, List<CharacterController> targets);

    //helper function to allow subclasses to call the event
    protected void AbilityOver() { EndCharacterTurn?.Invoke(); }
}