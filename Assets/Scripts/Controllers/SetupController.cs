using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour {

    [SerializeField] private CharacterController topCharacter;
    [SerializeField] private CharacterController middleCharacter;
    [SerializeField] private CharacterController bottomCharacter;
    //TODO: serialized list of enemy characters
    //TODO: serialized list of enemy data objects

    private CharacterController[] orderedControllers;

    public static event Action ConfirmedSelection;

    private void Awake() {
        orderedControllers = new CharacterController[] { topCharacter, middleCharacter, bottomCharacter };
    }

    //toggle whether the character is selected depending on the color of their blackout panel
    public void ToggleCharacterSelection(CharacterData data, GameObject panel) {
        if(!panel.activeInHierarchy) {
            AssignCharacter(data, panel);
        } else {
            UnassignCharacter(data, panel);
        }
    }

    //assign the character to the first available character controller
    private void AssignCharacter(CharacterData data, GameObject panel) {
        foreach (var c in orderedControllers) {
            if (c.CharData == null) {
                c.AssignCharacter(data);
                panel.SetActive(true);
                return;
            }
        }
    }

    //unassign the character from its currently assigned character controller
    private void UnassignCharacter(CharacterData data, GameObject panel) {
        foreach (var c in orderedControllers) {
            if(c.CharData == data) {
                c.UnassignCharacter();
                panel.SetActive(false);
                return;
            }
        }
    }

    //called by the Confirm button and triggers the end of the setup state
    public void ConfirmSelection() {
        //check that three characters are selected
        foreach (var c in orderedControllers) {
            if(c.CharData == null) {
                return;
            }
        }
        //TODO: randomly assign enemy characters and turn them on
        ConfirmedSelection?.Invoke();
    }
}