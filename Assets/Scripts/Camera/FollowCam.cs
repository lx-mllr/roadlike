using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	struct CamState {
		public Vector3 position;
		public Quaternion rotation;

		public CamState(Vector3 pos, Quaternion rot)
		{
			position = pos;
			rotation = rot;
		}

		public void lerpTo(CamState targCam, float smoothingPos, float smoothingRot)
		{
			position = Vector3.Lerp(position, targCam.position,  smoothingPos);
			rotation = Quaternion.Lerp(rotation, targCam.rotation, smoothingRot);
		}

		public void updateToTransform(Transform transform)
		{
			transform.position = position;
			transform.rotation = rotation;
		}

		public void DrawLines()
		{
			Vector3 fwd = rotation * Vector3.forward;
			Debug.DrawLine(position, position + (fwd * 10), Color.white, 0.0f);
		}

	}

	public Rigidbody m_rigidBody;
	
	public Vector3 camDist_direction = new Vector3(-0.2f, 6.0f, -5.75f);
	public Vector3 camTarg = new Vector3(-0.4f, 1.0f, 10.0f);
	public float camDist_magnitude = 10;
	public bool drawCamVectors = false;

	[Range(0.0f, 1.0f)]
	public float smoothingPos = 0.55f;
	[Range(0.0f, 1.0f)]
	public float smoothingRot = 0.35f;

	[Range(-1.0f, 1.0f)]
	public float horizontalOblique = 0.0f;
	[Range(-1.0f, 1.0f)]
	public float verticalOblique = 0.0f;
	public bool updateObliqueness = false;

	private Camera _camera;

	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera>();

		SetObliqueness();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (!_camera.enabled) {
			return;
		}

		if (updateObliqueness) {
			SetObliqueness();
		}
		
		CamState currentState = new CamState(transform.position, transform.rotation);

		// update if editor changed these values
		camDist_direction.Normalize();
		camDist_direction *= camDist_magnitude;

		CamState targetState = updateTargetState();

		currentState.lerpTo(targetState, smoothingPos, smoothingRot);
		//currentState.DrawLines();
		//DrawCamVecs();
		currentState.updateToTransform(transform);

		if (drawCamVectors) {
			DrawCamVecs();
		}
	}

	private CamState updateTargetState()
	{
		Vector3 pos = m_rigidBody.rotation * camDist_direction;
		pos += m_rigidBody.transform.position;

		Vector3 eyeTarget = m_rigidBody.rotation * camTarg;
		eyeTarget += m_rigidBody.position;
		Vector3 fwd = eyeTarget - pos;
		fwd.Normalize();
	
		//Vector3 up = m_rigidBody.rotation * Vector3.up;
		CamState targetState = new CamState(pos, Quaternion.LookRotation(fwd));
		return targetState;
	}

	private void DrawCamVecs()
	{
		Vector3 rotatedDir = m_rigidBody.rotation * camDist_direction;
		Debug.DrawLine(m_rigidBody.position, m_rigidBody.position + rotatedDir, Color.blue, 0.0f);

		Vector3 rotatedTarg = m_rigidBody.rotation * camTarg;
		Debug.DrawLine(m_rigidBody.position, m_rigidBody.position + rotatedTarg, Color.black, 0.0f);
	}

	private void SetObliqueness() {
		Matrix4x4 proj = _camera.projectionMatrix;
		proj[0, 2] = horizontalOblique;
		proj[1, 2] = verticalOblique;
		_camera.projectionMatrix = proj;
	}
}
