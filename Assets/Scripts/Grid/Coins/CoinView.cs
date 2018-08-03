using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour {

	public Vector3 toRotate = Vector3.up;
	public float rotationSpeed = 0.2f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(toRotate * rotationSpeed);
	}

	public class Factory : PlaceholderFactory<CoinView> {
	}
}
