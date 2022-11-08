using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleEnemyTurnState : EnemyTurnState {

    public override void Enter() {
        Debug.Log("Entering Middle Enemy Turn State");
        base.Enter();
    }

    public override void Exit() {
        Debug.Log("Exiting Middle Enemy Turn State");
        base.Exit();
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<BottomEnemyTurnState>();
    }
}