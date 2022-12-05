using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    [SerializeField] private ScreenFade screenFade;

    public void LoadScene(string sceneName) {
        StartCoroutine(WaitForFadeToLoadScene(sceneName));
    }

    private IEnumerator WaitForFadeToLoadScene(string sceneName) {
        screenFade.FadeOut();
        yield return new WaitUntil(() => screenFade.FadeOver);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
        StartCoroutine(WaitForFadeToQuit());
    }

    private IEnumerator WaitForFadeToQuit() {
        screenFade.FadeOut();
        yield return new WaitUntil(() => screenFade.FadeOver);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}