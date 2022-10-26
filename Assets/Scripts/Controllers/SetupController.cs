using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour {

    [SerializeField] private CharacterController topCharacter;
    [SerializeField] private CharacterController middleCharacter;
    [SerializeField] private CharacterController bottomCharacter;

    private CharacterController[] orderedControllers;

    private void Awake() {
        orderedControllers = new CharacterController[] { topCharacter, middleCharacter, bottomCharacter };
    }

    //toggle whether the character is selected depending on the color of their blackout panel
    public void ToggleSelection(CharacterData data, Image panel) {
        if(panel.color != Color.black) {
            AssignCharacter(data, panel);
        } else {
            UnassignCharacter(data, panel);
        }
    }

    //assign the character to the first available character controller
    private void AssignCharacter(CharacterData data, Image panel) {
        foreach (var c in orderedControllers) {
            if (c.CharData != null) {
                c.AssignCharacter(data);
                SetBlackoutPanel(panel, Color.black);
            }
        }
    }

    //unassign the character from its currently assigned character controller
    private void UnassignCharacter(CharacterData data, Image panel) {
        foreach (var c in orderedControllers) {
            if(c.CharData == data) {
                c.UnassignCharacter();
                SetBlackoutPanel(panel, Color.white);
            }
        }
    }

    //set the blackout panel to the given color
    private void SetBlackoutPanel(Image panel, Color color) {
        panel.color = color;
    }
}