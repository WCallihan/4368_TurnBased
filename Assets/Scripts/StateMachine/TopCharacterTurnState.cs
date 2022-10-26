using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player turn state for the top most character on the team
public class TopCharacterTurnState : PlayerTurnState {

    public override void Enter() {
        base.Enter();
        Debug.Log("Entering Top Character Turn State");
        StartPlayerTurn();
    }

    //FOR TESTING ONLY
    public override void Tick() {
        base.Tick();
        if(Input.GetKeyDown(KeyCode.Q)) characterTurnOver = true;
    }

    public override void Exit() {
        base.Exit();
        Debug.Log("Exiting Top Character Turn State");
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<MiddleCharacterTurnState>();
    }
}