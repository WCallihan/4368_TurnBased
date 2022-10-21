using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : RPGState {

    public static event Action LoseStateEntered;
    public static event Action LoseStateExited;

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Lose State");
        LoseStateEntered?.Invoke();
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Lose State");
        LoseStateExited?.Invoke();
    }
}