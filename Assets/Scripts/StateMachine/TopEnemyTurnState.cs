using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemyTurnState : EnemyTurnState {

    public override void Enter() {
        Debug.Log("Entering Top Enemy Turn State");
        StartEnemyTurns();
        base.Enter();
    }

    public override void Exit() {
        Debug.Log("Exiting Top Enemy Turn State");
        base.Exit();
    }

    protected override void NextTurn() {
        Debug.Log("there");
        StateMachine.ChangeState<MiddleEnemyTurnState>();
    }
}