using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWheelCharacter : MonoBehaviour {
    
    private Quaternion originalRotation;

	private void Awake() {
		originalRotation = transform.rotation;
	}

	private void Update() {
		transform.rotation = originalRotation;
	}
}