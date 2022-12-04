using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldbarUI : MonoBehaviour {

    [SerializeField] private Image damageTakenBar;
    [SerializeField] private Image currentShieldBar;

    private float maxShielding;

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void TurnOnShielding(float shielding) {
        gameObject.SetActive(true);
        maxShielding = shielding;
        damageTakenBar.fillAmount = 1;
        currentShieldBar.fillAmount = 1;
    }

    public void UpdateBar(float currentShield) {
        //update the current health bar
        currentShieldBar.fillAmount = currentShield / maxShielding;
        //update the damage taken bar
        StartCoroutine(LerpDamageTakenBar());
        //turn off bar if shielding is gone
        if(currentShield == 0) gameObject.SetActive(false);
    }

    private IEnumerator LerpDamageTakenBar() {
        //wait a second so the damage amount is easy to see
        yield return new WaitForSeconds(0.5f);

        float timeElapsed = 0;
        float lerpDuration = 0.75f;
        //have the damage taken bar catch up with the current health bar over time
        while(timeElapsed < lerpDuration) {
            damageTakenBar.fillAmount = Mathf.Lerp(damageTakenBar.fillAmount, currentShieldBar.fillAmount, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //set the bars equal at the end just in case
        damageTakenBar.fillAmount = currentShieldBar.fillAmount;
    }
}