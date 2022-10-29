using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStat { Attack, Defense, Healing }

public class CharacterController : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ActionsPanel actionsPanel;

    private CharacterData charData;
    private float currentHealth;

    public CharacterData CharData => charData;

    public static event Action<CharacterController> CharacterTargeted;

    private void Awake() {
        HidePanel();
    }

    public void AssignCharacter(CharacterData data) {
        charData = data;
        spriteRenderer.sprite = charData.Sprite;
        currentHealth = data.MaxHealth;
    }

    public void UnassignCharacter() {
        charData = null;
        spriteRenderer.sprite = null;
        currentHealth = -1;
    }

    public void ShowPanel() {
        actionsPanel.SetActions(charData);
        actionsPanel.gameObject.SetActive(true);
    }

    public void HidePanel() { actionsPanel.gameObject.SetActive(false); }

    public void Targeted() {
        CharacterTargeted?.Invoke(this);
    }

    public float GetStat(CharacterStat statToGet) {
        switch(statToGet) {
            case CharacterStat.Attack:
                return charData.AttackStat;
            case CharacterStat.Defense:
                return charData.DefenseStat;
            case CharacterStat.Healing:
                return charData.HealingStat;
            default:
                return -1;
        }
    }

    public void Heal(float healing) {
        Debug.Log($"{gameObject.name} healed by {healing}");

        //apply healing, but clamp at the MaxHealth of the character
        float newHealth = Mathf.Clamp(currentHealth + healing, currentHealth, charData.MaxHealth);

        //TODO: update character ui
    }
}