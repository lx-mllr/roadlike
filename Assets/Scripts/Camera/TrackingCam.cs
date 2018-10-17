using UnityEngine;
using Zenject;

public class TrackingCam : MonoBehaviour {

	[Inject] ISteering _steering;
	[Inject] SignalBus _signalBus;

	[Range(0.0f, 1.0f)]
	public float smoothingPos = 0.5f;

    private Vector3 _targetPosition;
    public Vector3 targetPosition { set { _targetPosition = value; } }

    void LateUpdate () {
        Vector3 toCar = _steering.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toCar);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothingPos);

        bool canSteer, canDrive;
        if (_steering.IsGrounded(out canSteer, out canDrive)) {
            _signalBus.Fire<DisableTrackingCamSignal>();
        }
    }
}