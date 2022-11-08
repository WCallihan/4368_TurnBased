using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    [Header("Turn Indicators")]
    [SerializeField] private GameObject playerTurnUI;
    [SerializeField] private GameObject enemyTurnUI;
    [Header("End Screens")]
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [Header("Target Selection Panels")]
    [SerializeField] private GameObject allyTargetSelectionUI;
    [SerializeField] private GameObject enemyTargetSelectionUI;

    private GameObject currentTargetingUI;

    private void OnEnable() {
        //subscribe to turn start and ending events to update UI
        PlayerTurnState.PlayerTurnStarted += ShowPlayerTurn;
        PlayerTurnState.PlayerTurnsEnded += HidePlayerTurn;
        EnemyTurnState.EnemyTurnsStarted += ShowEnemyTurn;
        EnemyTurnState.EnemyTurnsEnded += HideEnemyTurn;
        WinState.WinStateEntered += ShowWin;
        WinState.WinStateExited += HideWin;
        LoseState.LoseStateEntered += ShowLoss;
        LoseState.LoseStateExited += HideLoss;
        AbilityBase.StartTargetSelection += ShowTargetSelection;
        CharacterControllerBase.CharacterTargeted += HideTargetSelection;
    }

    private void OnDisable() {
        //unsubscribe from all events
        PlayerTurnState.PlayerTurnStarted -= ShowPlayerTurn;
        PlayerTurnState.PlayerTurnsEnded -= HidePlayerTurn;
        EnemyTurnState.EnemyTurnsStarted -= ShowEnemyTurn;
        EnemyTurnState.EnemyTurnsEnded -= HideEnemyTurn;
        WinState.WinStateEntered -= ShowWin;
        WinState.WinStateExited -= HideWin;
        LoseState.LoseStateEntered -= ShowLoss;
        LoseState.LoseStateExited -= HideLoss;
        AbilityBase.StartTargetSelection -= ShowTargetSelection;
        CharacterControllerBase.CharacterTargeted -= HideTargetSelection;
    }

    private void Start() {
        HidePlayerTurn();
        HideEnemyTurn();
        HideWin();
        HideLoss();
    }

    private void ShowPlayerTurn() { playerTurnUI.SetActive(true); }

    private void HidePlayerTurn() { playerTurnUI.SetActive(false); }

    private void ShowEnemyTurn() { enemyTurnUI.SetActive(true); }

    private void HideEnemyTurn() { enemyTurnUI.SetActive(false); }

    private void ShowWin() { winUI.SetActive(true); }

    private void HideWin() { winUI.SetActive(false); }

    private void ShowLoss() { loseUI.SetActive(true); }

    private void HideLoss() { loseUI.SetActive(false); }

    private void ShowTargetSelection(bool allies) {
        if(allies) {
            currentTargetingUI = allyTargetSelectionUI;
        } else {
            currentTargetingUI = enemyTargetSelectionUI;
        }
        currentTargetingUI.SetActive(true);
    }

    private void HideTargetSelection(CharacterControllerBase useless) { if(currentTargetingUI) currentTargetingUI.SetActive(false); }
}