using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomEnemyTurnState : EnemyTurnState {

    public override void Enter() {
        Debug.Log("Entering Bottom Enemy Turn State");
        base.Enter();
    }

    public override void Exit() {
        Debug.Log("Exiting Bottom Enemy Turn State");
        base.Exit();
        EndEnemyTurns();
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<TopCharacterTurnState>();
    }
}