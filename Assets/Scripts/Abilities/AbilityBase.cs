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
    public CharacterStat StatToUse => statToUse;
    public float Power => power;

    private CharacterControllerBase user;
    protected UIController uiController;

    public static event Action<bool> StartTargetSelection;
    public static event Action EndCharacterTurn;

    public void UseAbility(CharacterControllerBase abilityUser) {
        uiController = FindObjectOfType<UIController>();
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
        CharacterControllerBase.CharacterTargeted += SetSingleTarget;
    }

    private void SetSingleTarget(CharacterControllerBase singleTarget) {
        //apply the ability to the target
        ApplyAbility(user, singleTarget);
        //unsubscribe from the event just to be safe
        CharacterControllerBase.CharacterTargeted -= SetSingleTarget;
    }

    private void SelectMultipleTargets(bool allies) {
        //get all approprriate controllers
        CharacterControllerBase[] targetsArray;
        if(allies) {
            targetsArray = FindObjectsOfType<PlayerCharacter>();
        } else {
            targetsArray = FindObjectsOfType<EnemyCharacter>();
        }
        //convert to list and apply the ability
        List<CharacterControllerBase> multipleTargets = new List<CharacterControllerBase>(targetsArray);
        ApplyAbility(user, multipleTargets);
    }

    //used for abilities that target one character
    protected abstract void ApplyAbility(CharacterControllerBase user, CharacterControllerBase target);

    //used for abilities that target multiple characters
    protected abstract void ApplyAbility(CharacterControllerBase user, List<CharacterControllerBase> targets);

    //helper function to allow subclasses to call the event
    protected void AbilityOver() {
        EndCharacterTurn?.Invoke();
    }
}