using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : RPGState {

	private UIController uiController;

    public override void Enter() {
		//show win screen
		uiController = FindObjectOfType<UIController>();
		uiController.ShowWin();

		//make all the living player characters bob up and down
		var playerCharacters = FindObjectsOfType<PlayerCharacter>();
		foreach (var p in playerCharacters) {
			if(!p.Dead) p.Animator.SetTrigger("Activate");
		}
    }

	public void ExitWinState() {
		StateMachine.ChangeState<SetupState>();
	}

    public override void Exit() {
		//hide win screen
		uiController.HideWin();
    }
}