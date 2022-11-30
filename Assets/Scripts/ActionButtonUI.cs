using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private CanvasGroup descriptionPopup;
	[SerializeField] private TextMeshProUGUI descriptionText;

	private bool lerpFlag;

    public void SetButtonUI(AbilityBase ability) {
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
}