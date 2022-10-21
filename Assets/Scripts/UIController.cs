using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [SerializeField] private GameObject playerTurnUI;
    [SerializeField] private GameObject enemyTurnUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;

    private void OnEnable() {
        //subscribe to turn start and ending events to update UI
        PlayerTurnState.PlayerTurnStarted += ShowPlayerTurn;
        PlayerTurnState.PlayerTurnEnded += HidePlayerTurn;
        EnemyTurnsState.EnemyTurnStarted += ShowEnemyTurn;
        EnemyTurnsState.EnemyTurnEnded += HideEnemyTurn;
        WinState.WinStateEntered += ShowWin;
        WinState.WinStateExited += HideWin;
        LoseState.LoseStateEntered += ShowLoss;
        LoseState.LoseStateExited += HideLoss;
    }

    private void OnDisable() {
        //unsubscribe from all events
        PlayerTurnState.PlayerTurnStarted -= ShowPlayerTurn;
        PlayerTurnState.PlayerTurnEnded -= HidePlayerTurn;
        EnemyTurnsState.EnemyTurnStarted -= ShowEnemyTurn;
        EnemyTurnsState.EnemyTurnEnded -= HideEnemyTurn;
        WinState.WinStateEntered -= ShowWin;
        WinState.WinStateExited -= HideWin;
        LoseState.LoseStateEntered -= ShowLoss;
        LoseState.LoseStateExited -= HideLoss;
    }

    private void Start() {
        HidePlayerTurn();
        HideEnemyTurn();
        HideWin();
        HideLoss();
    }

    private void ShowPlayerTurn() {
        playerTurnUI.SetActive(true);
    }

    private void HidePlayerTurn() {
        playerTurnUI.SetActive(false);
    }

    private void ShowEnemyTurn() {
        enemyTurnUI.SetActive(true);
    }

    private void HideEnemyTurn() {
        enemyTurnUI.SetActive(false);
    }

    private void ShowWin() {
        winUI.SetActive(true);
    }

    private void HideWin() {
        winUI.SetActive(false);
    }

    private void ShowLoss() {
        loseUI.SetActive(true);
    }

    private void HideLoss() {
        loseUI.SetActive(false);
    }
}