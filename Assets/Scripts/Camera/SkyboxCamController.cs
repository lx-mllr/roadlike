using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamController : MonoBehaviour {

	private Camera _cam;

	// Use this for initialization
	void Start () {
		_cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (_cam) {
			_cam.fieldOfView = CamManager.activeCamera.fieldOfView;
			transform.rotation = CamManager.activeCamera.transform.rotation;
		}
	}
}
