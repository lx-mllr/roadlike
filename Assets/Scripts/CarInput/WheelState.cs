using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelState : MonoBehaviour {

	public float suspsension = 0.7f;
	private bool _grounded;
	public bool Grounded { get { return _grounded; } }

	// Use this for initialization
	void Start () {
		_grounded = false;
	}

	public void PollForGround() {
		_grounded = Physics.Raycast(transform.position, Vector3.down, suspsension);
	}
}
