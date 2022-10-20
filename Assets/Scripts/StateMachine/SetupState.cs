using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setup state where the player selects their three characters and their placements
public class SetupState : RPGState {

    [SerializeField] private GameObject characterSelectPanel;
    [SerializeField] private EnemyTurnsState enemyTurnsState;

    //TODO: private array to store characters and their locations

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Setup State");

        characterSelectPanel.SetActive(true);
    }

    public override void Tick() {
        //base.Tick();
        //TODO: keep track of where each selected character goes on the field and move them there
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Setup State");

        characterSelectPanel.SetActive(false);

        //TODO: choose the enemies randomly and assign them to the enemy turns state with appropriate positions
    }
}