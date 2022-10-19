using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGStateMachine : StateMachine {

    void Start() {
        //set starting state
        ChangeState<MainMenuState>();
    }
}