using UnityEngine;
using Zenject;

public class TriggerTrackingCam : MonoBehaviour {

    [Inject] SignalBus _signalBus;

    void OnTriggerEnter(Collider other) {
        Vector3 targetPos = transform.parent.position;

        Camera child = transform.parent.GetComponentInChildren<Camera>(true);
        if (child) {
            targetPos = child.transform.position;
        }

        _signalBus.Fire(new EnableTrackingCamSignal() {
            trackingPosition = targetPos
        });
    }
}