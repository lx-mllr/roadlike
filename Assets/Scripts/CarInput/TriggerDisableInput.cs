using UnityEngine;
using Zenject;

public class TriggerDisableInput : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

	void OnTriggerEnter(Collider other) {
        _signalBus.Fire<DisableInputSignal>();
    }
}