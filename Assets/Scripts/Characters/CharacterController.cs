using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStat { Attack, Defense, Healing }

public class CharacterController : MonoBehaviour {

    [SerializeField] private SpriteRenderer spriteRenderer;

    protected CharacterData charData;
    private float currentHealth;

    public CharacterData CharData => charData;

    public static event Action<CharacterController> CharacterTargeted;

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

    public void Targeted() {
        CharacterTargeted?.Invoke(this);
    }

    public void Heal(float healing) {
        Debug.Log($"{gameObject.name} healed by {healing}");

        //apply healing, but clamp at the MaxHealth of the character
        currentHealth = Mathf.Clamp(currentHealth + healing, currentHealth, charData.MaxHealth);

        //TODO: update character ui
    }
}