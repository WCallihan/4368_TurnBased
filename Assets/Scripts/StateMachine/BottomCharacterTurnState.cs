using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomCharacterTurnState : PlayerTurnState {

    //TODO: decide if there is any special Enter behaviour

    public override void Exit() {
        base.Exit();
        //TODO: change state to the enemy turn state
    }
}