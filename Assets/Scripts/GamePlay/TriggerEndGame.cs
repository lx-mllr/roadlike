using UnityEngine;
using Zenject;

public class TriggerEndGame : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

	void OnTriggerEnter(Collider other) {
        _signalBus.Fire<GameEndSignal>();
    }
}