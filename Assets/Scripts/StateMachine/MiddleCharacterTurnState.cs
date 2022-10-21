using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCharacterTurnState : PlayerTurnState {

    public override void Enter() {
        base.Enter();
        Debug.Log("Entering Middle Character Turn State");
    }

    //FOR TESTING ONLY
    public override void Tick() {
        base.Tick();
        if(Input.GetKeyDown(KeyCode.W)) characterTurnOver = true;
    }

    public override void Exit() {
        base.Exit();
        Debug.Log("Exiting Middle Character Turn State");
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<BottomCharacterTurnState>();
    }
}