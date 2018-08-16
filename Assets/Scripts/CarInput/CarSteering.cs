using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour, ISteering
{
	public Vector3 resetPosition = Vector3.zero;
	public float rotPadding = 0.8f;
	public float rotSpeed = 0.1f;

	public float speedPadding = 0.8f;
	public float MAX_SPEED = 3.0f;

	private float speed;
	private Quaternion _prevTargRot;
	private Quaternion targetRot;
	private Vector3 _prevFwd;
	private Vector3 fwd;
	private bool _grounded;

	private Rigidbody _rigidBody;

	public bool Grounded { 
		get {
			return _grounded;
		} 
	}

	void Awake () {
		if (resetPosition.sqrMagnitude == 0) {
			resetPosition = transform.position;
		}

		_rigidBody = GetComponent<Rigidbody>();
	}

	void Update () {
	}

	public void Reset () {
		_rigidBody.velocity = Vector3.zero;
		_rigidBody.angularVelocity = Vector3.zero;
		
		speed = 0.0f;

		transform.position = resetPosition;
		transform.rotation = Quaternion.identity;
	}

	/// xRatio [-1, 1]
	public void Move (float steering, float accel, float footbrake, float handbrake) {
		_grounded = Physics.Raycast(transform.position, transform.up * -1, 1.0f);
		ApplyRotation(steering);
		ApplyMovement(accel);
	}

	private void ApplyMovement (float yRatio) {
		Vector3 dir = transform.forward;
		speed = Mathf.Lerp(speed, yRatio * MAX_SPEED, speedPadding);

		// //Vector3 targOrentation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f) * transform.forward;

		// if (_grounded)
		// {
		// 	//dir = Vector3.Lerp(dir, ((transform.up * -3f) + (targOrentation * 2f)).normalized, 0.1f);
		// 	_rigidBody.velocity = dir * speed;
		// }

		transform.position += dir * speed;
	}

	private void ApplyRotation (float xRatio) {
		float rotDir = xRatio;
		rotDir *= rotSpeed;

		fwd = Vector3.RotateTowards(transform.forward, transform.right, rotDir, 1.0f);
		targetRot = Quaternion.FromToRotation(transform.forward, fwd);

		transform.rotation = Quaternion.Lerp(_prevTargRot, targetRot, rotPadding);
		transform.forward =  Vector3.Lerp(_prevFwd, fwd, rotPadding);

		// transform.rotation = targetRot;
		// transform.forward = fwd;

		_prevFwd = fwd;
		_prevTargRot = targetRot;
	}
}
