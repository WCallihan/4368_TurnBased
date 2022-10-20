using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract player turn state to be inherited by the top, middle, and bottom turn states
public abstract class PlayerTurnState : RPGState {

    //TODO: public character scriptable object and/or game object

    private void OnEnable() {
        //TODO: subscribe to the character's events
    }

    private void OnDisable() {
        //TODO: unsubscribe from the character's events
    }

    public override void Enter() {
        //base.Enter();
        //TODO: tell the character to show start their turn or show their action panel
    }

    public override void Exit() {
        //base.Exit();
        //TODO: turn off any character panels
    }
}