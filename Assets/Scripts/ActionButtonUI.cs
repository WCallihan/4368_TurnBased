using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI buttonText;

    private string buttonDescription;

    public void SetButtonUI(AbilityBase ability) {
        buttonText.text = ability.AbilityName;
        buttonDescription = ability.AbilityDescription;
    }

    //TODO: when the mouse hovers over the button, show the description
}