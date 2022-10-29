using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject actionsPanel;
    private CharacterData charData;

    public CharacterData CharData => charData;

    private void Awake() {
        HidePanel();
    }

    public void AssignCharacter(CharacterData data) {
        charData = data;
        spriteRenderer.sprite = charData.Sprite;
    }

    public void UnassignCharacter() {
        charData = null;
        spriteRenderer.sprite = null;
    }

    public void ShowPanel() { actionsPanel.SetActive(true); }

    public void HidePanel() { actionsPanel.SetActive(false); }
}