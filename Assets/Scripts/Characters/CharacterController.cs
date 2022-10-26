using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject actionsPanel;
    private CharacterData charData;

    public CharacterData CharData => charData;

    private void Awake() {
        HidePanel();
    }

    public void AssignCharacter(CharacterData data) {
        charData = data;
        sprite = charData.Sprite;
    }

    public void UnassignCharacter() {
        charData = null;
        sprite = null;
    }

    public void ShowPanel() { actionsPanel.SetActive(true); }

    public void HidePanel() { actionsPanel.SetActive(false); }
}