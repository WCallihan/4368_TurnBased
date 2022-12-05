using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour {

    [SerializeField] private Animator animator;

    public bool FadeOver;

    public void FadeOut() {
        FadeOver = false;
        animator.SetTrigger("FadeOut");
    }

    //called by the FadeOut animation on the last frame
    public void SetFadeOver() {
        FadeOver = true;
    }
}