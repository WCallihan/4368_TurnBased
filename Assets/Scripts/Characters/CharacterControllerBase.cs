using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStat { Attack, Defense, Healing }
public enum ChangableStatus { HitChance, Shielding, Grappled }

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
	public static event Action<CharacterControllerBase, float> CharacterHurt;
	public static event Action<CharacterControllerBase, float> CharacterHealed;
	public static event Action<CharacterControllerBase, float> CharacterShielded;

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

        //apply healing, but clamp at the MaxHealth of the character
        currentHealth = Mathf.Clamp(currentHealth + healing, currentHealth, charData.MaxHealth);

        //update healthbar UI
        healthbar.UpdateBar(currentHealth, charData.MaxHealth);

		//set off static event to prompt damage popup
		CharacterHealed?.Invoke(this, healing);
    }

    public void TakeDamage(float damageTaken) {
		float damageToHealth = damageTaken;
        if(shielding > 0) {
            //take away shielding before health
            float originalDamage = damageTaken;
            damageToHealth = Mathf.Clamp(damageTaken - shielding, 0, damageTaken);
            shielding = Mathf.Clamp(shielding - originalDamage, 0, shielding);
            shieldbar.UpdateBar(shielding);
        }        

        //apply damage, but clamp at 0
        currentHealth = Mathf.Clamp(currentHealth - damageToHealth, 0, currentHealth);

        //play animation
        animator.SetTrigger("GetHit");

        //update healthbar UI
        healthbar.UpdateBar(currentHealth, charData.MaxHealth);

		//set off static event to prompt damage popup
		CharacterHurt?.Invoke(this, damageTaken);

        if(currentHealth == 0) {
			StartCoroutine(Die());
        }
    }

    public void SetShielding(float shield) {
		//set shielding amount
        shielding = shield;

		//update the bar ui if the character has one
        if(shieldbar) shieldbar.TurnOnShielding(shielding);

		//set off static event to prompt shielding popup
		CharacterShielded?.Invoke(this, shield);
    }

	private IEnumerator Die() {
		dead = true;
		yield return new WaitForSeconds(1);
		gameObject.SetActive(false);
	}
}