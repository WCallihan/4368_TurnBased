using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopEnemyTurnState : EnemyTurnState {

    public override void Enter() {
        Debug.Log("Entering Top Enemy Turn State");
        base.Enter();
        StartEnemyTurns();
    }

    public override void Exit() {
        Debug.Log("Exiting Top Enemy Turn State");
        base.Exit();
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<MiddleEnemyTurnState>();
    }
}