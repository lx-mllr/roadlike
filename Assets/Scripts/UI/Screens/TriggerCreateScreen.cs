using UnityEngine;
using Zenject;

public class TriggerCreateScreen : MonoBehaviour {

    [Inject]
    SignalBus _signalBus;

    private bool fireOnce = false;

    public CanvasRenderer screenToCreate;

	void OnTriggerEnter(Collider other) {
        if (!fireOnce) {
            _signalBus.Fire(new CreateScreenSignal () { 
                toCreate = screenToCreate
            });
            fireOnce = true;
        }
    }

}