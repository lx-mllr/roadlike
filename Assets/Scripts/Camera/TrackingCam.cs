using UnityEngine;
using Zenject;

public class TrackingCam : MonoBehaviour {

	[Inject] ISteering _steering;
	

	[Range(0.0f, 1.0f)]
	public float smoothingPos = 0.1f;
    private Vector3 _targetPosition;

    void OnEnable () {
        _targetPosition = new Vector3(_steering.transform.position.x, transform.position.y, _steering.transform.position.z);
    }

    void LateUpdate () {
        _targetPosition.z = _steering.transform.position.z;

        Vector3 toCar = _steering.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toCar);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothingPos);
    }
}