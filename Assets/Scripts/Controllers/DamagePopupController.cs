using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamagePopupController : MonoBehaviour {

	[SerializeField] private GameObject damagePopupPrefab;

	private void OnEnable() {
		CharacterControllerBase.CharacterHurt += SpawnDamagePopup;
		CharacterControllerBase.CharacterHealed += SpawnHealingPopup;
		CharacterControllerBase.CharacterShielded += SpawnShieldingPopup;
		PlayerCharacter.PlayerHitChanceChange += SpawnHitChancePopup;
	}

	private void OnDisable() {
		CharacterControllerBase.CharacterHurt -= SpawnDamagePopup;
		CharacterControllerBase.CharacterHealed -= SpawnHealingPopup;
		CharacterControllerBase.CharacterShielded -= SpawnShieldingPopup;
		PlayerCharacter.PlayerHitChanceChange -= SpawnHitChancePopup;
	}

	private void SpawnDamagePopup(CharacterControllerBase character, float damage) {
		var popupScript = popupSpawnHelper(character);
		popupScript.SetDamageText(damage);
	}

	private void SpawnHealingPopup(CharacterControllerBase character, float healing) {
		var popupScript = popupSpawnHelper(character);
		popupScript.SetHealingText(healing);
	}

	private void SpawnShieldingPopup(CharacterControllerBase character, float shielding) {
		var popupScript = popupSpawnHelper(character);
		popupScript.SetShieldingText(shielding);
	}

	private void SpawnHitChancePopup(CharacterControllerBase character, float hitChanceChange) {
		var popupScript = popupSpawnHelper(character);
		popupScript.SetHitChanceText(hitChanceChange);
	}

	private DamagePopup popupSpawnHelper(CharacterControllerBase character) {
		var popup = Instantiate(damagePopupPrefab, character.gameObject.transform);
		return popup.GetComponent<DamagePopup>();
	}
}