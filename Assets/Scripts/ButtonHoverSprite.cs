using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private Image hoverImage;

	private void Awake() {
		SetAlpha(0);
	}

	public void OnPointerEnter(PointerEventData data) {
		SetAlpha(1);
	}

	public void OnPointerExit(PointerEventData data) {
		SetAlpha(0);
	}

	private void SetAlpha(float alpha) {
		var tempColor = hoverImage.color;
		tempColor.a = alpha;
		hoverImage.color = tempColor;
	}
}