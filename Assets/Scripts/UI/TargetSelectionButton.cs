using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectionButton : MonoBehaviour {

    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private CharacterControllerBase character;
	[SerializeField] private AudioClip buttonPressSound;

	private void Awake() {
        button.enabled = true;
        buttonImage.enabled = true;
    }

    private void Update() {
        if(character.Dead) {
            button.enabled = false;
            buttonImage.enabled = false;
        }
    }

	//called by the target buttons to target the associated character
    public void SetTarget() {
        character.Targeted();
		AudioHelper.PlayClip2D(buttonPressSound);
    }
}