using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrackISteering : MonoBehaviour {

	public bool trackX = false;
	public bool trackY = false;
	public bool trackZ = false;

	[Inject]
	readonly ISteering toTrack;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (
			(trackX) ? toTrack.transform.position.x : transform.position.x,
			(trackY) ? toTrack.transform.position.y : transform.position.y,
			(trackZ) ? toTrack.transform.position.z : transform.position.z
		);

	}
}
