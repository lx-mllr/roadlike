using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerSpawnTile : MonoBehaviour {

	public Vector3 eulerDirection = Vector3.zero;

	[Inject]
	SignalBus signalBus {get; set;}

	void OnTriggerEnter(Collider other)
	{
		signalBus.Fire(new SpawnTileSignal() {
			spawnDirection = eulerDirection
		});
	}
}
