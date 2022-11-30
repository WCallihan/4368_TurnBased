using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private CanvasGroup descriptionPopup;
	[SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Color damageAbilityColor;
    [SerializeField] private Color healingAbilityColor;
    [SerializeField] private Color statusAbilityColor;

	private bool lerpFlag;

    public void SetButtonUI(AbilityBase ability) {
        buttonImage.color = GetButtonColor(ability);
        buttonText.text = ability.AbilityName;
        descriptionText.text = ability.AbilityDescription;
    }

	public void OnPointerEnter(PointerEventData eventData) {
		StartCoroutine(LerpDescriptionPopup(1));
	}

	public void OnPointerExit(PointerEventData eventData) {
		StartCoroutine(LerpDescriptionPopup(0));
	}

	private IEnumerator LerpDescriptionPopup(float newAlpha) {
		lerpFlag = !lerpFlag;
		bool expectedFlag = lerpFlag;
		float startingAlpha = descriptionPopup.alpha;
		float lerpDuration = 0.35f;
		float timeElapsed = 0;
		while(timeElapsed < lerpDuration && lerpFlag == expectedFlag) {
			descriptionPopup.alpha = Mathf.Lerp(startingAlpha, newAlpha, timeElapsed / lerpDuration);
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		if(lerpFlag == expectedFlag) descriptionPopup.alpha = newAlpha;
	}

    private Color GetButtonColor(AbilityBase ability) {
        if(ability is DamagingAbility) {
            return damageAbilityColor;
        } else if(ability is HealingAbility) {
            return healingAbilityColor;
        } else if(ability is StatusAbility) {
            return statusAbilityColor;
        } else {
            return Color.white;
        }
    }
}