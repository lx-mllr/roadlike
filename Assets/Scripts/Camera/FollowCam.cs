﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

	[Inject] ISteering _steering;
	
	public float camDist_magnitude = 10;
	public Vector3 camDist_direction = new Vector3(-0.2f, 6.0f, -5.75f);
	public Vector3 camTargOffset = new Vector3(-0.4f, 1.0f, 10.0f);
	public float impactForce = 3;
	public AnimationCurve impactTransition = AnimationCurve.Linear(0, 0, 2, 0);
	public bool drawCamVectors = false;

	
	[Range(0.0f, 1.0f)]
	public float smoothingImpact = 0.55f;

	[Range(0.0f, 1.0f)]
	public float smoothingPos = 0.55f;
	[Range(0.0f, 1.0f)]
	public float smoothingRot = 0.35f;

// Does not work with "no cascade" shadows
	[Range(-1.0f, 1.0f)]
	public float horizontalOblique = 0.0f;
	[Range(-1.0f, 1.0f)]
	public float verticalOblique = 0.0f;
	public bool updateObliqueness = false;

	private Camera _camera;

	private Vector3 _impactOffset;
	private float impactTransTime;

	public void onApplyForce(ApplyForceToCarSignal signal) {
		//Vector3 sceenToWorld = _camera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, _camera.nearClipPlane));
		Vector3 impactToCar = Vector3.Normalize(signal.impactPoint - _steering.transform.position);
		float dot = Vector3.Dot(impactToCar, transform.right);
		float offset = 1 - Mathf.Abs(dot);
		_impactOffset = impactToCar * (impactForce * offset);
		impactTransTime = 0.0f;
	}

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

		Vector3 target = camDist_direction.normalized * camDist_magnitude;
		CamState targetState = getTargetState(target);

		currentState.lerpTo(targetState, smoothingPos, smoothingRot);
		currentState.updateToTransform(transform);

		if (drawCamVectors) {
			DrawCamVecs();
			currentState.DrawLines();
		}

		impactTransTime += Time.deltaTime;
	}

	private CamState getTargetState(Vector3 posOffset)
	{
		Vector3 pos = _steering.transform.position + posOffset;// = _steering.transform.rotation * posOffset;
		//pos += _steering.transform.position;

		float t_ImpactTrans = impactTransition.Evaluate(impactTransTime);
		Vector3 impact = Vector3.Lerp(Vector3.zero, _impactOffset, t_ImpactTrans);
		Vector3 eyeTarget = camTargOffset + impact;
		eyeTarget = _steering.transform.rotation * eyeTarget;
		eyeTarget += _steering.transform.position;
		Vector3 fwd = eyeTarget - pos;
		fwd.Normalize();
	
		//Vector3 up = m_rigidBody.rotation * Vector3.up;
		CamState targetState = new CamState(pos, Quaternion.LookRotation(fwd));
		return targetState;
	}

	private void DrawCamVecs()
	{
		Vector3 rotatedDir = _steering.transform.rotation * camDist_direction;
		Debug.DrawLine(_steering.transform.position, _steering.transform.position + rotatedDir, Color.blue, 0.0f);

		Vector3 rotatedTarg = _steering.transform.rotation * camTargOffset;
		Debug.DrawLine(_steering.transform.position, _steering.transform.position + rotatedTarg, Color.black, 0.0f);
	}

	private void SetObliqueness() {
		Matrix4x4 proj = _camera.projectionMatrix;
		proj[0, 2] = horizontalOblique;
		proj[1, 2] = verticalOblique;
		_camera.projectionMatrix = proj;
	}
}
