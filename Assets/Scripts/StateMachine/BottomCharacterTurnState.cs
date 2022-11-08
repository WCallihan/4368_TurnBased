using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCharacterTurnState : PlayerTurnState {

    public override void Enter() {
        base.Enter();
        Debug.Log("Entering Bottom Character Turn State");
    }

    public override void Exit() {
        base.Exit();
        Debug.Log("Exiting Bottom Character Turn State");
        EndPlayerTurns();
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<TopEnemyTurnState>();
    }
}