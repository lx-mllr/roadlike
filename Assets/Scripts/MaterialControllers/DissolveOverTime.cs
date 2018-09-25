using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOverTime : MonoBehaviour {

	public string PropertyKey = "";
	public float timeScale = 1f;
	public bool invert = false;

	private Material _material;
	private float _time;

	// Use this for initialization
	void Start () { 
		_material = GetComponent<Renderer>().material;
		_time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		_time += Time.deltaTime / timeScale;
		_material.SetFloat(PropertyKey, (invert) ? 1 - _time : _time);
	}
}
