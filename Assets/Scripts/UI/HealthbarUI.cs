using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour {

    [SerializeField] private Image damageTakenBar;
    [SerializeField] private Image currentHealthBar;
	[SerializeField] private TextMeshProUGUI healthText;

	public void SetHealthText(float maxHealth) {
		UpdateHealthText(maxHealth, maxHealth);
	}

    public void UpdateBar(float currentHealth, float maxHealth) {
        //update the current health bar
        currentHealthBar.fillAmount = currentHealth / maxHealth;
		UpdateHealthText(currentHealth, maxHealth);
        //update the damage taken bar
        StartCoroutine(LerpDamageTakenBar());
    }

	public void UpdateHealthText(float currentHealth, float maxHealth) {
		healthText.text = $"{currentHealth}/{maxHealth}";
	}

	private IEnumerator LerpDamageTakenBar() {
        //wait a second so the damage amount is easy to see
        yield return new WaitForSeconds(0.5f);

        float timeElapsed = 0;
        float lerpDuration = 0.75f;
        //have the damage taken bar catch up with the current health bar over time
        while(timeElapsed < lerpDuration) {
            damageTakenBar.fillAmount = Mathf.Lerp(damageTakenBar.fillAmount, currentHealthBar.fillAmount, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //set the bars equal at the end just in case
        damageTakenBar.fillAmount = currentHealthBar.fillAmount;
    }
}