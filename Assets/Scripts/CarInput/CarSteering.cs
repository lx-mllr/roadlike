using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour, ISteering
{
	public WheelState[] frontWheels;
	public WheelState[] backWheels;

	public Vector3 resetPosition = Vector3.zero;
	public float rotPadding = 0.8f;
	public float rotSpeed = 0.1f;

	public float speedPadding = 0.8f;
	public float MAX_SPEED = 3.0f;
	public float decelRate = 0.01f;

	public float gravAngleFactor = 0.15f;
	public Vector3 reoreientDirection = Vector3.forward + Vector3.down;

	private float _speed;
	private Quaternion _prevTargRot;
	private Vector3 _prevFwd;
	private Vector3 _moveDir;

	private Rigidbody _rigidBody;
	public Rigidbody rigidBody { get { return _rigidBody; } }

	void Awake () {
		_rigidBody = GetComponent<Rigidbody>();
		Reset();
	}

	public void Reset () {
		_rigidBody.velocity = Vector3.zero;
		_rigidBody.angularVelocity = Vector3.zero;
		
		_speed = 0.0f;

		transform.position = resetPosition;
		transform.forward = _prevFwd = Vector3.forward;

		int xRot = (Random.value > 0.5f) ? 1 : -1;
		int zRot = (Random.value > 0.5f) ? 1 : -1;
		Quaternion resetRotation = Quaternion.Euler(xRot, 0, zRot);
		transform.rotation = resetRotation;
		_prevTargRot = Quaternion.identity;
	}

	public bool IsGrounded (out bool canSteer, out bool canDrive) {
		canSteer = true;
		canDrive = true;
		for (int i = 0; i < backWheels.Length; i++)
		{
			canDrive &= backWheels[i].IsGrounded();
		}
		for (int i = 0; i < frontWheels.Length; i++)
		{
			canSteer &= frontWheels[i].IsGrounded();
		}
		return canSteer && canDrive;
	}

	/// sttering and accel -> [-1, 1]
	public void Move (float steering, float accel, float footbrake, float handbrake) {
	
		bool canSteer = true;
		bool canDrive = true;
		IsGrounded(out canSteer, out canDrive);

		ApplyRotation(steering, canSteer, canDrive);
		ApplyMovement(accel, canDrive);
	}

	private void ApplyMovement (float yRatio, bool motor) {

		_speed = motor ?
					Mathf.Lerp(_speed, yRatio * MAX_SPEED, speedPadding) :
					Mathf.Lerp(_speed, _speed * decelRate, speedPadding);

		transform.position += _moveDir.normalized * _speed;
	}

	private void ApplyRotation (float xRatio, bool canSteer, bool canDrive) {
		float rotDir = xRatio;
		rotDir *= (!canDrive && !canSteer) ? rotSpeed / 2 : rotSpeed;   // smaller RoM without road contact

		Vector3 fwd = Vector3.RotateTowards(_prevFwd, transform.right, rotDir, 1.0f);
		if (!canDrive && !canSteer) {
			fwd += reoreientDirection * gravAngleFactor;
		}
		Quaternion targetRot = Quaternion.FromToRotation(_prevFwd, fwd);

		transform.rotation = Quaternion.Lerp(_prevTargRot, targetRot, rotPadding);
		transform.forward =  Vector3.Lerp(_prevFwd, fwd, rotPadding);

		_prevFwd = fwd;
		_prevTargRot = targetRot;

		if (canDrive) {
			_moveDir = fwd;
		}
	}
}
