using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStat { Attack, Defense, Healing }
public enum ChangableStatis { HitChance, Shielding, Grappled }

public class CharacterControllerBase : MonoBehaviour {

    [SerializeField] private Image characterImage;
    [SerializeField] private HealthbarUI healthbar;
    [SerializeField] private ShieldbarUI shieldbar;
    [SerializeField] private Animator animator;

    protected CharacterData charData;
    private float currentHealth;
    private float shielding;
    private bool dead;

    public CharacterData CharData => charData;
    public float Shielding => shielding;
    public bool Dead => dead;
    public Animator Animator => animator;

    public static event Action<CharacterControllerBase> CharacterTargeted;

    protected virtual void Awake() {
        dead = false;
        gameObject.SetActive(false);
    }

    public void AssignCharacter(CharacterData data) {
        gameObject.SetActive(true);
        charData = data;
        characterImage.sprite = charData.Sprite;
        currentHealth = data.MaxHealth;
    }

    public void UnassignCharacter() {
        gameObject.SetActive(false);
        charData = null;
        characterImage.sprite = null;
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
        if(dead) return;

        Debug.Log($"{gameObject.name} healed by {healing}");

        //apply healing, but clamp at the MaxHealth of the character
        currentHealth = Mathf.Clamp(currentHealth + healing, currentHealth, charData.MaxHealth);

        //update healthbar UI
        healthbar.UpdateBar(currentHealth, charData.MaxHealth);
    }

    public void TakeDamage(float damage) {
        Debug.Log($"{gameObject.name} damage by {damage}");

        if(shielding > 0) {
            //take away shielding before health
            float originalDamage = damage;
            damage = Mathf.Clamp(damage - shielding, 0, damage);
            shielding = Mathf.Clamp(shielding - originalDamage, 0, shielding);
            shieldbar.UpdateBar(shielding);
        }        

        //apply damage, but clamp at 0
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);

        //update healthbar UI
        healthbar.UpdateBar(currentHealth, charData.MaxHealth);

        if(currentHealth == 0) {
            dead = true;
            characterImage.sprite = null;
            gameObject.SetActive(false);
        }
    }

    public void SetShielding(float shield) {
        shielding = shield;
        if(shieldbar) shieldbar.TurnOnShielding(shielding);
    }
}