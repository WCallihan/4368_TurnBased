using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI popupText;
	[SerializeField] private Color damageColor;
	[SerializeField] private Color healingColor;
	[SerializeField] private Color shieldingColor;
	[SerializeField] private Color hitChanceColor;

	public void SetDamageText(float damage) {
		popupText.color = damageColor;
		popupText.text = $"-{damage}";
	}

	public void SetHealingText(float healing) {
		popupText.color = healingColor;
		popupText.text = $"+{healing}";
	}

	public void SetShieldingText(float shielding) {
		popupText.color = shieldingColor;
		popupText.text = $"+{shielding}";
	}

	public void SetHitChanceText(float chanceChange) {
		popupText.color = hitChanceColor;
		if(chanceChange < 0) {
			popupText.text = $"{chanceChange}"; //the negative is included
		} else {
			popupText.text = $"+{chanceChange}";
		}
	}
    
	//called at the end of the animation
    public void DestroyObject() {
		Destroy(gameObject);
	}
}