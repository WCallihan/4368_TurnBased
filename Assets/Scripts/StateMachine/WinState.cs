using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : RPGState {

    public static event Action WinStateEntered;
    public static event Action WinStateExited;

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Win State");
        WinStateEntered?.Invoke();
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Win State");
        WinStateExited?.Invoke();
    }
}