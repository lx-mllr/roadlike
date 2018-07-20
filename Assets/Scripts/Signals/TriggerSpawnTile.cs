using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerSpawnTile : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Triggered");
		signalBus.Fire<SpawnTileSignal>();
	}
}
