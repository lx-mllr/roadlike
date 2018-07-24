using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour {

	// Use this for initialization
	public Vector3 nextGridPos;
	public Vector3 nextTileEuler;

	public MeshCollider meshCollider;

	protected void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public class Factory : PlaceholderFactory<Tile>
	{
	}
}
