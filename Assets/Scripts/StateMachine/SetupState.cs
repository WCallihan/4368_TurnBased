using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//setup state where the player selects their three characters and their placements
public class SetupState : RPGState {

	private SetupController setupController;

    public override void Enter() {

		setupController = FindObjectOfType<SetupController>();

        //subscribe to the event to end the setup state
        SetupController.ConfirmedSelection += EndSetup;

		setupController.ActivatePanel();
    }

    private void EndSetup() {
        StateMachine.ChangeState<TopCharacterTurnState>();
    }

    public override void Exit() {
		setupController.DeactivatePanel();
    }
}