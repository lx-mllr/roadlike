using UnityEngine;
using Zenject;

public class TriggerCreateScreen : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

    public CanvasRenderer screenToCreate;

	void OnTriggerEnter(Collider other) {
            TriggerCreate();
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;
    }

    public void TriggerCreate () {
        _signalBus.Fire(new CreateScreenSignal () { 
            toCreate = screenToCreate
        });
    }

}