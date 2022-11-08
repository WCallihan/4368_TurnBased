using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setup state where the player selects their three characters and their placements
public class SetupState : RPGState {

    [SerializeField] private GameObject characterSelectPanel;

    public override void Enter() {
        //base.Enter();
        Debug.Log("Entering Setup State");

        if(characterSelectPanel) characterSelectPanel.SetActive(true);
        //subscribe to the event to end the setup state
        SetupController.ConfirmedSelection += EndSetup;
    }

    private void EndSetup() {
        StateMachine.ChangeState<TopCharacterTurnState>();
    }

    public override void Exit() {
        //base.Exit();
        Debug.Log("Exiting Setup State");

        if(characterSelectPanel) characterSelectPanel.SetActive(false);
    }
}