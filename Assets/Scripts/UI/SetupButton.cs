using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetupButton : MonoBehaviour {

    [SerializeField] private CharacterData characterData;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject blackoutPanel;
	[SerializeField] private AudioClip buttonPressSound;

    private SetupController setupController;

    private void Awake() {
        setupController = FindObjectOfType<SetupController>();
        SetCharacter();
    }

    private void SetCharacter() {
        characterImage.sprite = characterData.Sprite;
        nameText.text = characterData.Name;
    }

    //called by the setup buttons and send the character data and blackout panel to the setup controller
    public void SendCharacter() {
        setupController.ToggleCharacterSelection(characterData, blackoutPanel);
		AudioHelper.PlayClip2D(buttonPressSound);
    }
}