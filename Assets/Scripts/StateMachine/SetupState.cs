using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setup state where the player selects their three characters and their placements
public class SetupState : RPGState {

    [SerializeField] private GameObject characterSelectPanel;
    [SerializeField] private EnemyTurnsState enemyTurnsState;

    //TODO: private array to store characters and their locations

    private bool setupOver;

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Setup State");
        setupOver = false;
        if(characterSelectPanel) characterSelectPanel.SetActive(true);
    }

    public override void Tick() {
        //base.Tick();
        //TODO: keep track of where each selected character goes on the field and move them there

        if(Input.GetKeyDown(KeyCode.Space)) setupOver = true; //FOR TESTING ONLY

        if(setupOver) {
            setupOver = false;
            StateMachine.ChangeState<TopCharacterTurnState>();
        }
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Setup State");

        if(characterSelectPanel) characterSelectPanel.SetActive(false);

        //TODO: choose the enemies randomly and assign them to the enemy turns state with appropriate positions
    }
}