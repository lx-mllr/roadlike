using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonFlashController : MonoBehaviour {

	const string L_WALL_POS = "_LWall_pos";
	const string L_FLASH_STRENGTH = "_LFlashStrength";
	const string R_WALL_POS = "_RWall_pos";
	const string R_FLASH_STRENGTH = "_RFlashStrength";


	public Renderer[] _renderers;

	public Vector3 visualOffset = Vector3.zero;

	public float baseIntensity = 0.001f;
	public float intensity = 0.5f;
	public float flashTreshold = 0.5f;
	public float timeTillFlash = 0.5f;

	private float lastFlash = 0.0f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		bool flashLeft = false, flashRight = false;
		float lFlashStrengh = baseIntensity;
		float rFlashStrength = baseIntensity;

		// find left wall
		Vector3 toLeft = Vector3.left;
		Physics.Raycast(transform.position, toLeft, out hit);
		Vector3 lWallPos = hit.point;

		// find right wall
		Vector3 toRight = Vector3.right;
		Physics.Raycast(transform.position, toRight, out hit);
		Vector3 rWallPos = hit.point;

		Vector3 carToLeft = transform.position - lWallPos;
		Vector3 carToRight = rWallPos - transform.position;

		float totalSqrMag = carToLeft.sqrMagnitude + carToRight.sqrMagnitude;

		if (Time.time - lastFlash > timeTillFlash)
		{
			lastFlash = Time.time;
			flashLeft = Random.value > flashTreshold;
			flashRight = Random.value > flashTreshold;

			if (flashLeft) {
				lFlashStrengh = (1 - (carToLeft.sqrMagnitude / totalSqrMag)) * intensity;
			}
			if (flashRight) {
				rFlashStrength = (1 - (carToRight.sqrMagnitude / totalSqrMag)) * intensity;
			}

			lWallPos = transform.InverseTransformPoint(lWallPos);
			rWallPos = transform.InverseTransformPoint(rWallPos);

			for (int i = 0; i < _renderers.Length; i++)
			{
				_renderers[i].material.SetVector(L_WALL_POS, lWallPos);
				_renderers[i].material.SetVector(R_WALL_POS, rWallPos);
				_renderers[i].material.SetFloat(L_FLASH_STRENGTH, lFlashStrengh);
				_renderers[i].material.SetFloat(R_FLASH_STRENGTH, rFlashStrength);
			}
		}
	}
}
