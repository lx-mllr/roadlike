using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerCoinCollider : MonoBehaviour {

	[Inject]
	SignalBus _signalBus;

	void OnTriggerEnter (Collider other) {
		_signalBus.Fire<CollectCoinSignal>();

		Destroy(gameObject);
	}
}
