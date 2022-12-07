using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : RPGState {

	private UIController uiController;

    public override void Enter() {
		//show loss screen
		uiController = FindObjectOfType<UIController>();
		uiController.ShowLoss();

		//make all the living enemies bob up and down
		var enemyCharacters = FindObjectsOfType<EnemyCharacter>();
		foreach (var e in enemyCharacters) {
			if(!e.Dead) e.Animator.SetTrigger("Activate");
		}
    }

	public void ExitLoseState() {
		StateMachine.ChangeState<SetupState>();
	}

	public override void Exit() {
		//hide loss screen
		uiController.HideLoss();
    }
}