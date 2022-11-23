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
		actionsPanel.AbilitySelected += playerCharacter.StartAbility;

		ShowPanel();

        playerCharacter.Animator.SetTrigger("Activate");
    }

    public override void Exit() {
        //unsubscribe to the ability events
        AbilityBase.EndCharacterTurn -= EndCharacterTurn;
		actionsPanel.AbilitySelected -= playerCharacter.StartAbility;

		HidePanel();
    }


    //work around functions to allow subclasses to call the events
    protected void StartPlayerTurns() { PlayerTurnsStarted?.Invoke(); }
    protected void EndPlayerTurns() { PlayerTurnsEnded?.Invoke(); }

    private void EndCharacterTurn() {
        var enemies = FindObjectsOfType<EnemyCharacter>();
        foreach(var e in enemies) {
            if(!e.Dead) {
                //an enemy is alive, the game continues
                NextTurn();
                return;
            }
        }

        //all enemies are dead, go to win state
        StateMachine.ChangeState<WinState>();
    }


	public void ShowPanel() {
		actionsPanel.SetActions(playerCharacter.CharData);
		actionsPanel.gameObject.SetActive(true);
	}

	public void HidePanel() { actionsPanel.gameObject.SetActive(false); }
}