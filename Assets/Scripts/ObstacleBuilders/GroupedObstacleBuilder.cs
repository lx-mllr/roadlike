using UnityEngine;

public class GroupedObstacleBuilder : IBuilder {
    readonly GroupedBuilderSettings _settings;

    int _count = 0;
    int _spread = 0;

    public GroupedObstacleBuilder (GroupedBuilderSettings settings) {
        _settings = settings;

        _count = _settings.minCount + (int)(Random.value * (_settings.maxCount - _settings.minCount));
        for (int i = _settings.minSpread; i <= _settings.maxSpread; i++) {
            _spread = i;
            if (i % _count == 0) {
                break;
            }
        }
    }

    public void SpawnForTile (Tile tile) {
        int i = 0;
        MineView rf;
        Vector3 newPos;
        Bounds tBounds = tile.meshCollider.bounds;

        for (; i < _count; i++) {
            newPos = tile.transform.position + getPos(i);

            if (tBounds.Contains(newPos + Vector3.down)) {
                rf = spawnAndParent(tile);
                rf.transform.position = newPos;
            }
        }
    }

    private MineView spawnAndParent(Tile t) {
        MineView m = GameObject.Instantiate(_settings.prefab);
        m.transform.parent = t.transform;
        return m;
    }

    private Vector3 getPos (int index) {
        int mod = index % _spread;
        int subLayer = index / _spread;
        float theta = (mod / (float)_spread) * (2.0f * 3.14f);
        theta += UnityEngine.Random.value * _settings.rotVariance;

        Vector3 ret = Vector3.zero;
        Vector2 offset = _settings.circleSize;
        for (int i = 0; i <= subLayer; i++) {
            ret.x += _settings.circleSize.x * Mathf.Sin(theta);
            ret.z += _settings.circleSize.y * Mathf.Cos(theta);

            offset *= _settings.distReduction;
            theta += UnityEngine.Random.value * _settings.rotVariance;
        }
        return ret;
    }
}