using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract enemy turn state to be inherited by the top, middle, and bottom turn states
public abstract class EnemyTurnState : RPGState {

    [SerializeField] private EnemyCharacter enemyCharacter;

    private UIController uiController;

    public static event Action EnemyTurnsStarted;
    public static event Action EnemyTurnsEnded;

    protected abstract void NextTurn();

    private void Start() {
        uiController = FindObjectOfType<UIController>();
    }

    public override void Enter() {
        //base.Enter();

        //skip everything if the character is dead
        if(enemyCharacter.Dead) {
            Debug.Log("here");
            NextTurn();
            return;
        }

        StartCoroutine(Attack());
        //TODO: show UI
    }

    public override void Exit() {
        //base.Exit();
        //TODO: hide UI
    }


    //work around functions to allow subclasses to call the events
    protected void StartEnemyTurns() { EnemyTurnsStarted?.Invoke(); }
    protected void EndEnemyTurns() { EnemyTurnsEnded?.Invoke(); }


    private void EndCharacterTurn() {
        var playerCharacters = FindObjectsOfType<PlayerCharacter>();
        foreach(var p in playerCharacters) {
            if(!p.Dead) {
                //a player character is alive, the game continues
                NextTurn();
                return;
            }
        }

        //all player characters are dead, go to lose state
        StateMachine.ChangeState<LoseState>();
    }


    public IEnumerator Attack() {
        PlayerCharacter[] playerCharacters = FindObjectsOfType<PlayerCharacter>();
        int randInd = UnityEngine.Random.Range(0, playerCharacters.Length);
        var target = playerCharacters[randInd];
        float damage = ((enemyCharacter.CharData.AttackStat) * 50) / 100;
        target.TakeDamage(damage);

        //TODO: update UI

        Debug.Log($"{enemyCharacter.CharData.Name} attacked {target.CharData.Name} for {damage} damage");
        uiController.DisplayActionTaken($"{enemyCharacter.CharData.Name} attacked {target.CharData.Name} for {damage} damage");
        yield return new WaitForSecondsRealtime(2);
        EndCharacterTurn();
    }
}