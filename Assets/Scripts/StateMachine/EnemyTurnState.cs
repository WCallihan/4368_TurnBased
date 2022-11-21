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

        enemyCharacter.Animator.SetTrigger("Activate");

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
        var target = ChooseTarget();
        float damage = ((enemyCharacter.CharData.AttackStat) * 50) / 100;

        //if the enemy is grappled, distribute the damage between them and the target
        if(enemyCharacter.Grappled) {
            enemyCharacter.TakeDamage(damage / 2);
            target.TakeDamage(damage / 2);
        } else {
            target.TakeDamage(damage);
        }

        //TODO: update UI

        enemyCharacter.Animator.SetTrigger("UseAbility");

        Debug.Log($"{enemyCharacter.CharData.Name} attacked {target.CharData.Name} for {damage} damage");
        uiController.DisplayActionTaken($"{enemyCharacter.CharData.Name} attacked {target.CharData.Name} for {damage} damage");
        yield return new WaitForSecondsRealtime(2);
        EndCharacterTurn();
    }

    //randomly choose a target scaled based on their hit chance
    private PlayerCharacter ChooseTarget() {
        PlayerCharacter[] playerCharacters = FindObjectsOfType<PlayerCharacter>();
        List<PlayerCharacter> possibleTargets = new List<PlayerCharacter>();
        //make a new list with all non-dead player characters
        foreach (var p in playerCharacters) {
            if(!p.Dead) possibleTargets.Add(p);
        }

        int hitChancePool = 0;
        List<int> hitChances = new List<int>();

        //add all the hit chances into one pool and put the increments in an array corresponding to each character
        for (int i=0; i<possibleTargets.Count; i++) {
            hitChancePool += possibleTargets[i].HitChance;
            hitChances.Add(hitChancePool);
        }

        //randomly choose a number in the pool, the higher the character's hit chance, the more likely that this number is within its range
        int rand = UnityEngine.Random.Range(0, hitChancePool + 1);
        /*Debug.Log($"hitChancePool: {hitChancePool}");
        Debug.Log($"hitChances: {hitChances[0]}, {hitChances[1]}, {hitChances[2]}");
        Debug.Log($"rand: {rand}");*/

        //find which range the random number lies between and return that target
        for(int i = 0; i < hitChances.Count; i++) {
            if(rand <= hitChances[i]) {
                return possibleTargets[i];
            }
        }

        return null; //this should throw an error later, which is good
    }
}