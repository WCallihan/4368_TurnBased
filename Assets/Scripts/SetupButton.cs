using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupButton : MonoBehaviour {

    [SerializeField] private SetupController setupController;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private GameObject blackoutPanel;

    //called by the setup buttons and send the character data and blackout panel to the setup controller
    public void SendCharacter() {
        setupController.ToggleCharacterSelection(characterData, blackoutPanel);
    }
}