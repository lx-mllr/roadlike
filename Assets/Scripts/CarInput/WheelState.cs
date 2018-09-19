using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelState : MonoBehaviour {

	public float suspsension = 0.7f;

	// Use this for initialization
	// void Start () {
	// }

	public bool IsGrounded() {
		return Physics.Raycast(transform.position, Vector3.down, suspsension);
	}
}
