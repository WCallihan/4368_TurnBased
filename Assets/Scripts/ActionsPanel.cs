using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanel : MonoBehaviour {

    [SerializeField] private ActionButtonUI ability1Button;
    [SerializeField] private ActionButtonUI ability2Button;
    [SerializeField] private ActionButtonUI attackButton;
    [SerializeField] private ActionButtonUI dodgeButton;

	private AbilityBase ability1;
	private AbilityBase ability2;
	private AbilityBase attack;
	private AbilityBase dodge;

	public event Action<AbilityBase> AbilitySelected;
    
    //called by the buttons on actions panel

    public void SelectAbility1() { AbilitySelected?.Invoke(ability1); }

    public void SelectAbility2() { AbilitySelected?.Invoke(ability2); }

    public void SelectAttack() { AbilitySelected?.Invoke(attack); }

    public void SelectDodge() { AbilitySelected?.Invoke(dodge); }

    //sets the buttons on the panel to match the character data's abilities
    public void SetActions(CharacterData data) {
		ability1 = data.Ability1;
        ability1Button.SetButtonUI(ability1);
		ability2 = data.Ability2;
        ability2Button.SetButtonUI(ability2);
		attack = data.BasicAttack;
		attackButton.SetButtonUI(attack);
		dodge = data.BasicDodge;
		dodgeButton.SetButtonUI(dodge);
    }
}