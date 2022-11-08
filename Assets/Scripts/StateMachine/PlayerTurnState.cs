using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

    [SerializeField] private InputController input;
    [SerializeField] private PlayerCharacter playerCharacter;

    public static event Action PlayerTurnStarted;
    public static event Action PlayerTurnsEnded;

    protected abstract void NextTurn();

    public override void Enter() {
        //base.Enter();

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
    protected void StartPlayerTurns() { PlayerTurnStarted?.Invoke(); }
    protected void EndPlayerTurns() { PlayerTurnsEnded?.Invoke(); }


    private void EndCharacterTurn() {
        //TODO: check win condition
        NextTurn();
    }


    public void UseAbility1() {
        playerCharacter.CharData.Ability1.UseAbility(playerCharacter);
    }

    public void UseAbility2() {
        playerCharacter.CharData.Ability2.UseAbility(playerCharacter);
    }

    public void UseAttack() {
        playerCharacter.CharData.BasicAttack.UseAbility(playerCharacter);
    }

    public void UseDodge() {
        playerCharacter.CharData.BasicDodge.UseAbility(playerCharacter);
    }
}