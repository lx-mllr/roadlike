using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class RandomTileFactory : IFactory<Tile> {

    DiContainer _container;
    RTFSettings _settings;

    public RandomTileFactory(DiContainer container, RTFSettings settings)
    {
        _container = container;
        _settings = settings;
    }

    public Tile Create()
    {
        int index = (int)(UnityEngine.Random.Range(0.0f, 1.0f) * _settings.randomTiles.Length);
        return _container.InstantiatePrefabForComponent<Tile>(_settings.randomTiles[index]);
    }

    [Serializable]
    public class RTFSettings {
        public Tile[] randomTiles;
    }
}