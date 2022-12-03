using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//abstract enemy turn state to be inherited by the top, middle, and bottom turn states
public abstract class EnemyTurnState : RPGState {

    [SerializeField] private EnemyCharacter enemyCharacter;

    private UIController uiController;

    public static event Action EnemyTurnsStarted;
    public static event Action EnemyTurnsEnded;

    protected abstract void NextTurn();

    private void OnEnable() {
        enemyCharacter.EnemyCharacterTurnOver += EndCharacterTurn;
    }

    private void OnDisable() {
        enemyCharacter.EnemyCharacterTurnOver -= EndCharacterTurn;
    }

    private void Start() {
        uiController = FindObjectOfType<UIController>();
    }

    public override void Enter() {
        //base.Enter();

        //skip everything if the character is dead
        if(enemyCharacter.Dead) {
            NextTurn();
            return;
        }

		uiController.SetTurnName(enemyCharacter.CharData.Name);

		enemyCharacter.Animator.SetTrigger("Activate");

        StartCoroutine(enemyCharacter.PrepAttack());
    }

    //work around functions to allow subclasses to call the events
    protected void StartEnemyTurns() { EnemyTurnsStarted?.Invoke(); }
    protected void EndEnemyTurns() { EnemyTurnsEnded?.Invoke(); }

    private void EndCharacterTurn() {
        StartCoroutine(EndCharacterTurnWait());
    }

    private IEnumerator EndCharacterTurnWait() {
        yield return new WaitForSeconds(1);

        var playerCharacters = FindObjectsOfType<PlayerCharacter>();
        foreach(var p in playerCharacters) {
            if(!p.Dead) {
                //a player character is alive, the game continues
                NextTurn();
                yield break;
            }
        }

        //all player characters are dead, go to lose state
        StateMachine.ChangeState<LoseState>();
    }
}