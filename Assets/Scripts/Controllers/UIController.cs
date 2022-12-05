using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {

    [Header("Turn Indicators")]
	[SerializeField] private TextMeshProUGUI turnNameText;
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
        AbilityBase.StartTargetSelection += ShowTargetSelection;
        CharacterControllerBase.CharacterTargeted += HideTargetSelection;
    }

    private void OnDisable() {
        //unsubscribe from all events
        PlayerTurnState.PlayerTurnsStarted -= ShowPlayerTurn;
        PlayerTurnState.PlayerTurnsEnded -= HidePlayerTurn;
        EnemyTurnState.EnemyTurnsStarted -= ShowEnemyTurn;
        EnemyTurnState.EnemyTurnsEnded -= HideEnemyTurn;
        AbilityBase.StartTargetSelection -= ShowTargetSelection;
        CharacterControllerBase.CharacterTargeted -= HideTargetSelection;
    }

    private void Start() {
		ResetTurnName();
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

    public void ShowWin() {
		ResetTurnName();
		winUI.SetActive(true);
	}

    public void HideWin() { winUI.SetActive(false); }

    public void ShowLoss() {
		ResetTurnName();
		loseUI.SetActive(true);
	}

    public void HideLoss() { loseUI.SetActive(false); }

    private void ShowTargetSelection(bool allies) {
        if(allies) {
            currentTargetingUI = allyTargetSelectionUI;
        } else {
            currentTargetingUI = enemyTargetSelectionUI;
        }
        currentTargetingUI.SetActive(true);
    }

    private void HideTargetSelection(CharacterControllerBase useless) { if(currentTargetingUI) currentTargetingUI.SetActive(false); }

	private void ResetTurnName() {
		turnNameText.text = "";
	}

	public void SetTurnName(string characterName) {
		turnNameText.text = $"{characterName}'s Turn:";
	}

	public void DisplayActionPermanent(string actionString) {
		actionTakenText.text = actionString;
	}

    public void DisplayActionTaken(string actionTakenString) {
        StartCoroutine(ActionTakenCoroutine(actionTakenString));
    }

    private IEnumerator ActionTakenCoroutine(string actionTakenString) {
        displaysCountingDown++;
        actionTakenText.text = actionTakenString;
        yield return new WaitForSeconds(3);
        displaysCountingDown--;
        if(displaysCountingDown == 0) actionTakenText.text = "";
    }
}