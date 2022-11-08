using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract enemy turn state to be inherited by the top, middle, and bottom turn states
public abstract class EnemyTurnState : RPGState {

    [SerializeField] private EnemyCharacter enemyCharacter;

    public static event Action EnemyTurnsStarted;
    public static event Action EnemyTurnsEnded;

    protected abstract void NextTurn();

    public override void Enter() {
        //base.Enter();
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
        //TODO: check for lose condition
        NextTurn();
    }


    public IEnumerator Attack() {
        PlayerCharacter[] playerCharacters = FindObjectsOfType<PlayerCharacter>();
        int randInd = UnityEngine.Random.Range(0, playerCharacters.Length);
        var target = playerCharacters[randInd];
        float damage = ((enemyCharacter.CharData.AttackStat) * 50) / 100;
        target.TakeDamage(damage);

        //TODO: update UI

        Debug.Log($"{enemyCharacter.CharData.Name} attacked {target.CharData.Name} for {damage} damage");
        yield return new WaitForSecondsRealtime(2);
        EndCharacterTurn();
    }
}