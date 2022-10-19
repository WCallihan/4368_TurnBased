using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RPGStateMachine))]
public class RPGState : State {

    protected RPGStateMachine StateMachine { get; private set; }

    private void Awake() {
        StateMachine = GetComponent<RPGStateMachine>();
    }
}