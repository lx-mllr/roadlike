using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerDespawnTile : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("on Despawn");
		signalBus.Fire<DespawnTileSignal>();
	}
}
