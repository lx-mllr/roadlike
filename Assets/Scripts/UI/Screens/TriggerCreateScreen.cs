using UnityEngine;
using Zenject;

public class TriggerCreateScreen : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

    public CanvasRenderer screenToCreate;

	void OnTriggerEnter(Collider other) {
        _signalBus.Fire(new CreateScreenSignal () { 
            toCreate = screenToCreate
        });
    }

}