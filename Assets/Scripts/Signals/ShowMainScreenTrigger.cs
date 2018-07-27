using UnityEngine;
using Zenject;

public class ShowMainScreenTrigger : MonoBehaviour {

    [Inject]
    readonly SignalBus _signalBus;

    void OnTriggerEnter(Collider other) {
        _signalBus.Fire<ShowMainScreenSignal>();
        _signalBus.Fire<GameEndSignal>();
    }

    public void CleanUp() {
        Destroy(gameObject);
    }

    public class Factory : PlaceholderFactory<ShowMainScreenTrigger> {
    }
}