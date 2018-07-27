using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackBuilder {
	void Start();
	void Reset();
	void Generate();
	void Despawn();
}
