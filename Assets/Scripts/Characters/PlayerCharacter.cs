using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : CharacterControllerBase {

    [SerializeField] private ActionsPanel actionsPanel;

    private void Awake() {
        HidePanel();
    }

    public void ShowPanel() {
        actionsPanel.SetActions(charData);
        actionsPanel.gameObject.SetActive(true);
    }

    public void HidePanel() { actionsPanel.gameObject.SetActive(false); }
}