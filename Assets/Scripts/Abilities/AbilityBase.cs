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
	private List<CharacterControllerBase> targetList = new List<CharacterControllerBase>();
    protected UIController uiController;
	protected Animator characterAnimator;

    public static event Action<bool> StartTargetSelection;
    public static event Action EndCharacterTurn;

    public void UseAbility(CharacterControllerBase abilityUser, Animator animator) {
        uiController = FindObjectOfType<UIController>();
        user = abilityUser;
		characterAnimator = animator;
		targetList.Clear();
        switch(target) {
            case AbilityTarget.Self:
				targetList.Add(user);
				StartAbility();
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
		targetList.Add(singleTarget);
		StartAbility();
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
        targetList = new List<CharacterControllerBase>(targetsArray);
		StartAbility();
    }

	//used to start the animation when the ability is ready to use
	//the animation then calls the function to apply the ability at the right frame
	private void StartAbility() {
		characterAnimator.SetTrigger("UseAbility");
	}

	public void ApplyAbility() {
		if(targetList.Count == 1) ApplyAbility(user, targetList[0]);
		else ApplyAbility(user, targetList);
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