using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerSpawnTile : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Spawn Signal");
		signalBus.Fire<SpawnTileSignal>();
		
		Collider trigger = GetComponent<BoxCollider>();
		trigger.enabled = false;
	}
}
