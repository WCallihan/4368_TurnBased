using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectionButton : MonoBehaviour {

    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private CharacterControllerBase character;

    private void Awake() {
        button.enabled = true;
        buttonImage.enabled = true;
        //TODO: position perfectly over the character
    }

    private void Update() {
        if(character.Dead) {
            button.enabled = false;
            buttonImage.enabled = false;
        }
    }

    public void SetTarget() {
        character.Targeted();
    }
}