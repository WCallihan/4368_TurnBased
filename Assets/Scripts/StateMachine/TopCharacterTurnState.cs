using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player turn state for the top most character on the team
public class TopCharacterTurnState : PlayerTurnState {

    //TODO: decide if there is any special Enter behaviour

    public override void Exit() {
        base.Exit();
        StateMachine.ChangeState<MiddleCharacterTurnState>();
    }
}