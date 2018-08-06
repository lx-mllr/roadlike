using System;
using UnityEngine;

public class PhysicsSteering : MonoBehaviour, ISteering {

	public AxleInfo[] axels;
	public float maxTorque;
	public float maxSteering;

	public float anitRoll = 2.0f;

	public Vector3 centerOfMass = Vector3.forward;

	private float _currSteering;
	private float _currTorque;
	private Rigidbody _rb;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
		//_rb.centerOfMass += centerOfMass;
	}

	public void Reset () {
		_currSteering = _currTorque = 0.0f;
		ForEachAxel(SetSteering);
		ForEachAxel(SetTorque);
	}
	
	public void Move (float xRatio, float yRatio) {
		_currTorque = yRatio * maxTorque;
		_currSteering = xRatio * maxSteering;

		ForEachAxel(SetSteering);
		ForEachAxel(SetTorque);
		ForEachAxel(AntiRoll);
	}

	private void ForEachAxel (Action<AxleInfo> action) {
		for (int i = 0; i < axels.Length; i++) {
			action(axels[i]);
		}
	}

	private void SetSteering (AxleInfo axel) {
		if (axel.steering) {
			axel.leftWheel.steerAngle = _currSteering;
			axel.rightWheel.steerAngle = _currSteering;
		}
	}

	private void SetTorque (AxleInfo axel) {
		if (axel.motor) {
			axel.leftWheel.motorTorque = _currTorque;
			axel.rightWheel.motorTorque = _currTorque;
		}
	}

	private void AntiRoll (AxleInfo axel) {
		WheelHit hit;
		float travelL = 1.0f;
		float travelR = 1.0f;

		bool groundedL = axel.leftWheel.GetGroundHit(out hit);
		if (groundedL) {
			travelL = ((axel.leftWheel.transform.InverseTransformPoint(hit.point).y + axel.leftWheel.radius) * -1) / axel.leftWheel.suspensionDistance;
		}

		bool groundedR = axel.rightWheel.GetGroundHit(out hit);
		if (groundedR) {
			travelR = ((axel.rightWheel.transform.InverseTransformPoint(hit.point).y + axel.rightWheel.radius) * -1) / axel.rightWheel.suspensionDistance;
	}

		float antiRollForce = (travelL - travelR) * anitRoll;

		if (groundedL) {
			_rb.AddForceAtPosition(axel.leftWheel.transform.up * -antiRollForce, axel.leftWheel.transform.position);
		}
		if (groundedR) {
			_rb.AddForceAtPosition(axel.rightWheel.transform.up * antiRollForce, axel.rightWheel.transform.position);
		}
	}
}

[System.Serializable]
public struct AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
