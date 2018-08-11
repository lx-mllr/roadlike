using UnityEngine;
using Zenject;

public class TriggerEndGame : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

	void OnTriggerEnter(Collider other) {
        Collider c = GetComponent<Collider>();
        _signalBus.Fire<GameEndSignal>();
        c.enabled = false;
    }
}