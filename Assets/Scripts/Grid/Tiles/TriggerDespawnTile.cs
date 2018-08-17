using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerDespawnTile : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

	public BoxCollider backCollider;

	void OnTriggerEnter(Collider other)
	{
		signalBus.Fire<DespawnTileSignal>();
		backCollider.enabled = true;
	}
}
