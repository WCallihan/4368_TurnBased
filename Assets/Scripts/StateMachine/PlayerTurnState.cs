using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

	[SerializeField] private ActionsPanel actionsPanel;
	[SerializeField] private PlayerCharacter playerCharacter;

	public static event Action PlayerTurnsStarted;
    public static event Action PlayerTurnsEnded;

    protected abstract void NextTurn();

    public override void Enter() {
        //skip everything if the character is dead
        if(playerCharacter.Dead) {
            NextTurn();
            return;
        }

        //subscribe to the ability events
        AbilityBase.EndCharacterTurn += EndCharacterTurn;
		actionsPanel.AbilitySelected += OnAbilitySelected;

		ShowPanel();

        playerCharacter.Animator.SetTrigger("Activate");
    }

    public override void Exit() {
        //unsubscribe to the ability events
        AbilityBase.EndCharacterTurn -= EndCharacterTurn;
		actionsPanel.AbilitySelected -= OnAbilitySelected;

		HidePanel();
    }


    //work around functions to allow subclasses to call the events
    protected void StartPlayerTurns() { PlayerTurnsStarted?.Invoke(); }
    protected void EndPlayerTurns() { PlayerTurnsEnded?.Invoke(); }

    private void EndCharacterTurn() {
		StartCoroutine(EndCharacterTurnWait());
    }

	private IEnumerator EndCharacterTurnWait() {
		//add a short wait after each turn
		yield return new WaitForSeconds(1);

		var enemies = FindObjectsOfType<EnemyCharacter>();
		foreach(var e in enemies) {
			if(!e.Dead) {
				//an enemy is alive, the game continues
				NextTurn();
				yield break;
			}
		}

		//all enemies are dead, go to win state
		StateMachine.ChangeState<WinState>();
	}


	private void OnAbilitySelected(AbilityBase ability) {
		playerCharacter.StartAbility(ability);
		HidePanel();
	}


	public void ShowPanel() {
		actionsPanel.SetActions(playerCharacter.CharData);
		actionsPanel.ActivatePanel();
	}

	public void HidePanel() { actionsPanel.DeactivatePanel(); }
}