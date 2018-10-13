using UnityEngine;
using Zenject;

public class LinearBuilder : IBuilder {
	
    readonly LinearBuilderSettings _settings;

    int _count = 0;

	public LinearBuilder (LinearBuilderSettings settings) {
		_settings = settings;

		_count =  _settings.minCount + (int)(Random.value * (_settings.maxCount - _settings.minCount));
	}

    public void SpawnForTile (Tile tile) {
		GameObject gameObject;
		Vector3 tSize = tile.meshCollider.bounds.size;
		float zMax = tSize.z / 2;

		for (int i = 0; i < _count; i++) {
			float zOffset = Map ((i + 1) / _count, 0f, 1f, -zMax, zMax);
			gameObject = spawnAndParent(tile, zOffset);
		}
    }

	private GameObject spawnAndParent(Tile t, float zOffset) {
        GameObject gameObject = GameObject.Instantiate(_settings.prefab);
        gameObject.transform.parent = t.transform;
		Vector3 pos = t.transform.position;
		pos.y = gameObject.transform.position.y;
		pos.z += zOffset;
		gameObject.transform.position = pos;

        return gameObject;
    }

	private float Map (float value, float min1, float max1, float min2, float max2) {
		return ((value - min1) / (max1 - min1) * (max2 - min2)) + min2;
	}
}