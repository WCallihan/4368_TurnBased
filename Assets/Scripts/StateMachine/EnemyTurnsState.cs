using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnsState : RPGState {

    //TODO: public enemy characters divided by position

    public static event Action EnemyTurnStarted;
    public static event Action EnemyTurnEnded;

    private bool enemyTurnsOver;

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Enemy Turns State");
        EnemyTurnStarted?.Invoke();
        enemyTurnsOver = false;
        //TODO: for each enemy, decide what they should do and execute it
    }

    public override void Tick() {
        if(Input.GetKeyDown(KeyCode.R)) enemyTurnsOver = true; //FOR TESTING ONLY

        //base.Tick();

        //TODO: check for lose condition

        if(enemyTurnsOver) {
            enemyTurnsOver = false;
            StateMachine.ChangeState<TopCharacterTurnState>();
        }
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Enemy Turns State");
        EnemyTurnEnded?.Invoke();
    }
}