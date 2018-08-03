using System;
using UnityEngine;

public class PhysicsSteering : MonoBehaviour, ISteering {

	public AxleInfo[] axels;
	public float maxTorque;
	public float maxSteering;

	private float currSteering;
	private float currTorque;

	// Use this for initialization
	void Start () {
		
	}

	public void Reset () {
		currSteering = currTorque = 0.0f;
		ForEachAxel(SetSteering);
		ForEachAxel(SetTorque);
	}
	
	public void Move (float xRatio, float yRatio) {
		currTorque = xRatio * maxTorque;
		currSteering = yRatio * maxSteering;

		ForEachAxel(SetSteering);
		ForEachAxel(SetTorque);
		
	}

	private void SetSteering (AxleInfo axel) {
		if (axel.steering) {
			axel.leftWheel.steerAngle = currSteering;
			axel.rightWheel.steerAngle = currSteering;
		}
	}

	private void SetTorque (AxleInfo axel) {
		if (axel.motor) {
			axel.leftWheel.motorTorque = currTorque;
			axel.rightWheel.motorTorque = currTorque;
		}
	}

	private void ForEachAxel (Action<AxleInfo> toApply)  {
		for (int i = 0; i < axels.Length; i++)
		{
			toApply(axels[i]);
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
