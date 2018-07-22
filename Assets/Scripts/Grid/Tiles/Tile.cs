using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour, ITile {

	// Use this for initialization
	protected void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public class Factory : PlaceholderFactory<Tile>
	{
	}
}
