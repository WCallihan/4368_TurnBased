using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

    [SerializeField] private InputController input;
    [SerializeField] private PlayerCharacter character;

    public static event Action PlayerTurnStarted;
    public static event Action PlayerTurnEnded;

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

        character.ShowPanel();
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

        character.HidePanel();
    }


    //work around functions to allow subclasses to call the events
    protected void StartPlayerTurn() { PlayerTurnStarted?.Invoke(); }
    protected void EndPlayerTurn() { PlayerTurnEnded?.Invoke(); }

    private void EndCharacterTurn() {
        //TODO: check win condition
        NextTurn();
    }


    public void UseAbility1() {
        character.CharData.Ability1.UseAbility(character);
    }

    public void UseAbility2() {
        character.CharData.Ability2.UseAbility(character);
    }

    public void UseAttack() {
        //TODO: call (static?) attack script usage
    }

    public void UseDodge() {
        //TODO: call (static?) dodge script usage
    }
}