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
        Tile toCreate;
        if (_settings.startingCounter < _settings.startingTiles.Length)
        {
            toCreate = _settings.startingTiles[_settings.startingCounter];
            _settings.startingCounter++;
        }
        else
        {
            int index = (int) (UnityEngine.Random.Range(0.0f, 1.0f) * _settings.randomTiles.Length);
            toCreate = _settings.randomTiles[index];
        }

        return _container.InstantiatePrefabForComponent<Tile>(toCreate);
    }

    [Serializable]
    public class RTFSettings {
        public Tile[] randomTiles;
        public Tile[] startingTiles;
        
        public int startingCounter;
    }
}