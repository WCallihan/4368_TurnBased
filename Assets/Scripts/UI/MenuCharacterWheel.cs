using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterWheel : MonoBehaviour {

	[SerializeField] private float spinSpeed;
	[SerializeField] private float desiredWheelRadius;

	private void Update() {
		transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(gameObject.transform.position, desiredWheelRadius);
	}
}