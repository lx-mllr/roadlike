using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteering : MonoBehaviour, ISteering 
{
	public Vector3 resetPosition = Vector3.zero;
	public float rotSpeed = 0.1f;
	public float rotTime = 0.3f;

	public float MAX_SPEED = 4.0f;

	//public bool useCoroutine = true;

	private float speed;
	private Quaternion targetRot;
	private Vector3 fwd;

	void Awake () {
		if (resetPosition.sqrMagnitude == 0) {
			resetPosition = transform.position;
		}
	}

	public void Reset () {
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		
		speed = 0.0f;

		transform.position = resetPosition;
		transform.rotation = Quaternion.identity;
	}

	/// xRatio [-1, 1]
	public void Move (float xRatio, float yRatio) {
		ApplyRotation(xRatio);
		ApplyMovement(yRatio);
	}

	private void ApplyMovement (float yRatio) {
		speed = yRatio * MAX_SPEED;
		transform.position += transform.forward * speed;
	}

	private void ApplyRotation (float xRatio) {
		float rotDir = xRatio;
		rotDir *= rotSpeed;

		fwd = Vector3.RotateTowards(transform.forward, transform.right, rotDir, 1.0f);
		targetRot = Quaternion.FromToRotation(transform.forward, fwd);

		transform.rotation = targetRot;
		transform.forward = fwd;
	}

	private IEnumerator RotateTo () {
		float startTime = Time.time;
		while(Time.time > startTime + rotTime)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.time - startTime);
			transform.forward = Vector3.Lerp(transform.forward, fwd, Time.time - startTime);
			yield return null;
		}
		transform.rotation = targetRot;
		transform.forward = fwd;
	}
}
