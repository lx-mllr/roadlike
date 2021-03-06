﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TriggerApplyForce : MonoBehaviour {

	public float impactPower = 75.0f;
	public float forceRadius = 1.0f;
	public float upwardsMod = 1.5f;
	public string tagFilter = "";

	[Inject] SignalBus _signalBus;
	
	void OnTriggerEnter (Collider other)
	{
		if (tagFilter.Length == 0 
			|| other.tag == tagFilter)
		{
			Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);

			float distSqr = (transform.position - collisionPoint).sqrMagnitude;
			
			_signalBus.Fire(new ApplyForceToCarSignal () {
				power = impactPower,
				impactPoint = collisionPoint,
				radius = forceRadius,
				upMod = upwardsMod
			});
		}
	}
}
