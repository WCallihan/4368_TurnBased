using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public event Action PressedAbility1;
    public event Action PressedAbility2;
    public event Action PressedAttack;
    public event Action PressedDodge;

    private void Update() {
        DetectAbility1();
        DetectAbility2();
        DetectAttack();
        DetectDodge();
    }

    private void DetectAbility1() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            PressedAbility1?.Invoke();
        }
    }

    private void DetectAbility2() {
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            PressedAbility2?.Invoke();
        }
    }

    private void DetectAttack() {
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            PressedAttack?.Invoke();
        }
    }

    private void DetectDodge() {
        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            PressedDodge?.Invoke();
        }
    }
}