using UnityEngine;
using Zenject;

public class TrackingCam : MonoBehaviour {

	[Inject] ISteering _steering;
	
	public Vector3 camDist_direction = new Vector3(-0.2f, 6.0f, -5.75f);
	public Vector3 camTarg = new Vector3(-0.4f, 1.0f, 10.0f);
	public float camDist_magnitude = 10;

	[Range(0.0f, 1.0f)]
	public float smoothingPos = 0.55f;
	[Range(0.0f, 1.0f)]
	public float smoothingRot = 0.35f;

    void LateUpdate () {
        
		camDist_direction.Normalize();
		camDist_direction *= camDist_magnitude;

        
    }
}