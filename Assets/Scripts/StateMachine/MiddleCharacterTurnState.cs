using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCharacterTurnState : PlayerTurnState {

    //TODO: decide if there is any special Enter behaviour

    public override void Exit() {
        base.Exit();
        StateMachine.ChangeState<BottomCharacterTurnState>();
    }
}