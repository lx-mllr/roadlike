using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerApplyForce : MonoBehaviour {

	public float impactPower = 75.0f;
	public float forceRadius = 1.0f;
	public float upwardsMod = 1.5f;

	[Inject] SignalBus _signalBus;
	
	void OnTriggerEnter (Collider other)
	{
		var collisionPoint = other.ClosestPointOnBounds(transform.position);
		
		_signalBus.Fire(new ApplyForceToCarSignal () {
			power = impactPower,
			impactPoint = collisionPoint,
			radius = forceRadius,
			upMod = upwardsMod
		});
	}
}
