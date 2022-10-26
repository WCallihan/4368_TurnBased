using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

    [SerializeField] private InputController input;
    //TODO: public character scriptable object and/or game object

    public static event Action PlayerTurnStarted;
    public static event Action PlayerTurnEnded;

    protected bool characterTurnOver;

    protected abstract void NextTurn();

    public override void Enter() {
        //base.Enter();

        //subscribe to input events
        input.PressedAbility1 += UseAbility1;
        input.PressedAbility2 += UseAbility2;
        input.PressedAttack += UseAttack;
        input.PressedDodge += UseDodge;
        //TODO: subscribe to the character's events

        characterTurnOver = false;
        //TODO: tell the character to show start their turn or show their action panel
    }

    public override void Tick() {
        //base.Tick();

        //TODO: check for win condition

        if(characterTurnOver) {
            characterTurnOver = false;
            NextTurn();
        }
    }

    public override void Exit() {
        //base.Exit();

        //ubsubscribe from input events
        input.PressedAbility1 -= UseAbility1;
        input.PressedAbility2 -= UseAbility2;
        input.PressedAttack -= UseAttack;
        input.PressedDodge -= UseDodge;
        //TODO: unsubscribe from the character's events

        //TODO: turn off any character panels
    }

    //work around function to allow subclasses to call the events
    protected void StartPlayerTurn() {
        PlayerTurnStarted?.Invoke();
    }
    protected void EndPlayerTurn() {
        PlayerTurnEnded?.Invoke();
    }

    public void UseAbility1() {

    }

    public void UseAbility2() {

    }

    public void UseAttack() {

    }

    public void UseDodge() {

    }
}