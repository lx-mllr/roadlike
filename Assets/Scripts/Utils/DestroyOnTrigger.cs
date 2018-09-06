using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		Destroy(this.gameObject);
	}
}
