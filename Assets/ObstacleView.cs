using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObstacleView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public class Factory : PlaceholderFactory<ObstacleView> {
	}
}
