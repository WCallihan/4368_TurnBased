using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanel : MonoBehaviour {

    [SerializeField] private ActionButtonUI ability1Button;
    [SerializeField] private ActionButtonUI ability2Button;
    [SerializeField] private ActionButtonUI attackButton;
    [SerializeField] private ActionButtonUI dodgeButton;

    public static event Action Ability1Selected;
    public static event Action Ability2Selected;
    public static event Action AttackSelected;
    public static event Action DodgeSelected;
    
    //called by the buttons on actions panel

    public void SelectAbility1() { Ability1Selected?.Invoke(); }

    public void SelectAbility2() { Ability2Selected?.Invoke(); }

    public void SelectAttack() { AttackSelected?.Invoke(); }

    public void SelectDodge() { DodgeSelected?.Invoke(); }

    //sets the buttons on the panel to match the character data's abilities
    public void SetActions(CharacterData data) {
        ability1Button.SetButtonUI(data.Ability1);
        ability2Button.SetButtonUI(data.Ability2);
    }
}