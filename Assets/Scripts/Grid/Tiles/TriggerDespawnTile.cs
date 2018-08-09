using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerDespawnTile : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

	void OnTriggerEnter(Collider other)
	{
		signalBus.Fire<DespawnTileSignal>();
		Collider thisCollider = GetComponent<Collider>();
		thisCollider.enabled = false;
	}
}
