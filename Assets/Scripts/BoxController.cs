﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

	public float MAX_ROT_DIR = 75.0f;
	public float rotSpeed = 0.2f;
	public float rotTime = 0.3f;

	public float MAX_SPEED = 4.0f;

	public bool useCoroutine = true;

	public Vector3 targ;

	private float eulerY;
	private float speed;
	private Quaternion targetRot;
	private Vector3 fwd;

	private bool coroutineRunning = false;

	// Use this for initialization
	void Start () 
	{
	}

	/// xRatio [-1, 1]
	public void move(float xRatio, float yRatio)
	{
		applyRotation(xRatio);
		applyMovement(yRatio);
	}

	private void applyMovement(float yRatio)
	{
		Debug.Log("yRatio " + yRatio);
		speed = yRatio * MAX_SPEED;
		transform.position += transform.forward * speed;
	}

	private void applyRotation(float xRatio)
	{
		float rotDir = xRatio;
		rotDir *= rotSpeed;

		fwd = Vector3.RotateTowards(transform.forward, transform.right, rotDir, 1.0f);
		targetRot = Quaternion.FromToRotation(transform.forward, fwd);

		if (useCoroutine)
		{
			if (!coroutineRunning)
			{
				StartCoroutine( rotateTo());
			}
		}
		else
		{
			transform.rotation = targetRot;
			transform.forward = fwd;
		}
	}

	private IEnumerator rotateTo()
	{
		coroutineRunning = true;
		float startTime = Time.time;
		while(Time.time > startTime + rotTime)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.time - startTime);
			transform.forward = Vector3.Lerp(transform.forward, fwd, Time.time - startTime);
			yield return null;
		}
		transform.rotation = targetRot;
		transform.forward = fwd;
		coroutineRunning = false;
	}
}
