using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField] private GameObject damagePopupPrefab;

	private void OnEnable() {
		CharacterControllerBase.CharacterHurt += SpawnDamagePopup;
		CharacterControllerBase.CharacterHealed += SpawnHealingPopup;
		CharacterControllerBase.CharacterShielded += SpawnShieldingPopup;
	}

	private void OnDisable() {
		CharacterControllerBase.CharacterHurt -= SpawnDamagePopup;
		CharacterControllerBase.CharacterHealed -= SpawnHealingPopup;
		CharacterControllerBase.CharacterShielded -= SpawnShieldingPopup;
	}

	private void SpawnDamagePopup(CharacterControllerBase character, float damage) {
		var popup = Instantiate(damagePopupPrefab, character.gameObject.transform);
		var popupScript = popup.GetComponent<DamagePopup>();
		popupScript.SetDamageText(damage);
	}

	private void SpawnHealingPopup(CharacterControllerBase character, float healing) {
		var popup = Instantiate(damagePopupPrefab, character.gameObject.transform);
		var popupScript = popup.GetComponent<DamagePopup>();
		popupScript.SetHealingText(healing);
	}

	private void SpawnShieldingPopup(CharacterControllerBase character, float shielding) {
		var popup = Instantiate(damagePopupPrefab, character.gameObject.transform);
		var popupScript = popup.GetComponent<DamagePopup>();
		popupScript.SetShieldingText(shielding);
	}

	public void QuitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}