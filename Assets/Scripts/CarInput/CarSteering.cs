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

	private float _speed;
	private Quaternion _prevTargRot;
	private Quaternion targetRot;
	private Vector3 _prevFwd;
	private Vector3 _fwd;

	private Rigidbody _rigidBody;
	public Rigidbody rigidBody { get { return _rigidBody; } }

	void Awake () {
		_rigidBody = GetComponent<Rigidbody>();
		Reset();
	}

	void Update () {
	}

	public void Reset () {
		_rigidBody.velocity = Vector3.zero;
		_rigidBody.angularVelocity = Vector3.zero;
		
		_speed = 0.0f;

		transform.position = resetPosition;

		int xRot = (Random.value > 0.5f) ? 1 : -1;
		int zRot = (Random.value > 0.5f) ? 1 : -1;
		Quaternion resetRotation = Quaternion.Euler(xRot, 0, zRot);
		transform.rotation = resetRotation;
	}

	/// xRatio [-1, 1]
	public void Move (float steering, float accel, float footbrake, float handbrake) {
		ApplyRotation(steering);
		ApplyMovement(accel);
	}

	private void ApplyMovement (float yRatio) {
		Vector3 dir = transform.forward;
		_speed = Mathf.Lerp(_speed, yRatio * MAX_SPEED, speedPadding);

		transform.position += dir * _speed;
	}

	private void ApplyRotation (float xRatio) {
		float rotDir = xRatio;
		rotDir *= rotSpeed;

		_fwd = Vector3.RotateTowards(transform.forward, transform.right, rotDir, 1.0f);
		targetRot = Quaternion.FromToRotation(transform.forward, _fwd);

		transform.rotation = Quaternion.Lerp(_prevTargRot, targetRot, rotPadding);
		transform.forward =  Vector3.Lerp(_prevFwd, _fwd, rotPadding);

		_prevFwd = _fwd;
		_prevTargRot = targetRot;
	}
}
