using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderDisable : MonoBehaviour {

	public virtual void OnTriggerEnter (Collider other)
	{
		Collider myCollider = GetComponent<Collider>();
		myCollider.enabled = false;
	}
}
