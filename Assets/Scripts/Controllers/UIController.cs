using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Header("Action Taken Display")]
    [SerializeField] private TextMeshProUGUI actionTakenText;

    private GameObject currentTargetingUI;
    private static int displaysCountingDown;

    private void OnEnable() {
        //subscribe to turn start and ending events to update UI
        PlayerTurnState.PlayerTurnsStarted += ShowPlayerTurn;
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
        PlayerTurnState.PlayerTurnsStarted -= ShowPlayerTurn;
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
        displaysCountingDown = 0;
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

    public void DisplayActionTaken(string actionTakenString) {
        StartCoroutine(ActionTakenCoroutine(actionTakenString));
    }

    private IEnumerator ActionTakenCoroutine(string actionTakenString) {
        displaysCountingDown++;
        actionTakenText.text = actionTakenString;
        yield return new WaitForSeconds(2);
        displaysCountingDown--;
        if(displaysCountingDown == 0) actionTakenText.text = "";
    }
}