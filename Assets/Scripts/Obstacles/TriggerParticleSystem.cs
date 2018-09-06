using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticleSystem : MonoBehaviour {

	public ParticleSystem _particles;

	void OnTriggerEnter(Collider other) {
		_particles.Play();
	}
}
