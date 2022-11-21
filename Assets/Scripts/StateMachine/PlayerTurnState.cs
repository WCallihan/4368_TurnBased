using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

    [SerializeField] private InputController input;
    [SerializeField] private PlayerCharacter playerCharacter;

    public static event Action PlayerTurnsStarted;
    public static event Action PlayerTurnsEnded;

    protected abstract void NextTurn();

    public override void Enter() {
        //base.Enter();

        //skip everything if the character is dead
        if(playerCharacter.Dead) {
            NextTurn();
            return;
        }

        //subscribe to input events
        input.PressedAbility1 += UseAbility1;
        input.PressedAbility2 += UseAbility2;
        input.PressedAttack += UseAttack;
        input.PressedDodge += UseDodge;
        //subscribe to the action panel events
        ActionsPanel.Ability1Selected += UseAbility1;
        ActionsPanel.Ability2Selected += UseAbility2;
        ActionsPanel.AttackSelected += UseAttack;
        ActionsPanel.DodgeSelected += UseDodge;
        //subscribe to the ability event
        AbilityBase.EndCharacterTurn += EndCharacterTurn;

        playerCharacter.ShowPanel();

        playerCharacter.Animator.SetTrigger("Activate");
    }

    public override void Exit() {
        //base.Exit();

        //ubsubscribe from input events
        input.PressedAbility1 -= UseAbility1;
        input.PressedAbility2 -= UseAbility2;
        input.PressedAttack -= UseAttack;
        input.PressedDodge -= UseDodge;
        //unsubscribe to the action panel events
        ActionsPanel.Ability1Selected -= UseAbility1;
        ActionsPanel.Ability2Selected -= UseAbility2;
        ActionsPanel.AttackSelected -= UseAttack;
        ActionsPanel.DodgeSelected -= UseDodge;
        //unsubscribe to the ability event
        AbilityBase.EndCharacterTurn -= EndCharacterTurn;

        playerCharacter.HidePanel();
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


    public void UseAbility1() {
        playerCharacter.Animator.SetTrigger("UseAbility");
        playerCharacter.CharData.Ability1.UseAbility(playerCharacter);
    }

    public void UseAbility2() {
        playerCharacter.Animator.SetTrigger("UseAbility");
        playerCharacter.CharData.Ability2.UseAbility(playerCharacter);
    }

    public void UseAttack() {
        playerCharacter.Animator.SetTrigger("UseAbility");
        playerCharacter.CharData.BasicAttack.UseAbility(playerCharacter);
    }

    public void UseDodge() {
        playerCharacter.Animator.SetTrigger("UseAbility");
        playerCharacter.CharData.BasicDodge.UseAbility(playerCharacter);
    }
}