using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterControllerBase {

    [SerializeField] private ActionsPanel actionsPanel;

    private int baseHitChance;
    private int hitChance = 100;

    public int HitChance => hitChance;

    private void OnEnable() {
        PlayerTurnState.PlayerTurnsStarted += ResetHitChance;
    }

    private void OnDisable() {
        PlayerTurnState.PlayerTurnsStarted -= ResetHitChance;
    }

    private void Awake() {
        HidePanel();
    }

    public void ShowPanel() {
        actionsPanel.SetActions(charData);
        actionsPanel.gameObject.SetActive(true);
    }

    public void HidePanel() { actionsPanel.gameObject.SetActive(false); }

    public void SetBaseHitChance(int chance) {
        baseHitChance = chance;
        ResetHitChance();
    }

    public void ChangeHitChance(int chance) {
        hitChance += chance;
    }

    //reset hit chance at the beginning of the player's turns
    private void ResetHitChance() {
        hitChance = baseHitChance;
    }
}