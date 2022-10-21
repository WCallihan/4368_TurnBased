using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCharacterTurnState : PlayerTurnState {

    public override void Enter() {
        base.Enter();
        Debug.Log("Entering Bottom Character Turn State");
    }

    //FOR TESTING ONLY
    public override void Tick() {
        base.Tick();
        if(Input.GetKeyDown(KeyCode.E)) characterTurnOver = true;
    }

    public override void Exit() {
        base.Exit();
        Debug.Log("Exiting Bottom Character Turn State");
        EndTurn();
    }

    protected override void NextTurn() {
        StateMachine.ChangeState<EnemyTurnsState>();
    }
}