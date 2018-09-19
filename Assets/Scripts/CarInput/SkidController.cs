using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkidController : MonoBehaviour {

	[Inject] IInputManager _inputManager;
	

	public ParticleSystem leftWheel;
	public ParticleSystem rightWheel;
	public float threshold = 1.75f;

	void Update () {
		float steeringRatio = _inputManager.inputRatio.x;

		if (steeringRatio > threshold) {
			rightWheel.Play();
		}
		else {
			rightWheel.Stop();
		}

		if (steeringRatio < threshold * -1) {
			leftWheel.Play();
		}
		else {
			leftWheel.Stop();
		}
	}
}
