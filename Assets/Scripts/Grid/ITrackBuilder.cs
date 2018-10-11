using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackBuilder {
	void Start();
	void Reset();
	void OnSpawnTile(SpawnTileSignal signal);
	void Despawn();
}
