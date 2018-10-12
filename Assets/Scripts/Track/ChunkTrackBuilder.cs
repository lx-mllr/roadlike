using Zenject;
using System;
using UnityEngine;
using System.Collections.Generic;

public class ChunkTrackBuilder : ITrackBuilder, IInitializable {
    
    readonly ChunkTrackBuilderSettings _settings;

    readonly Tile.Factory _tileFactory;
	readonly RandomTileFactory.RTFSettings _tileFactorySettings;
    
	readonly BuilderFactory _spawnPatternFactory;
    readonly ImplBuilderFactory.Settings _obstacleFactorySettings;
    
    Chunk _activeChunk;
    int _chunkElementIndex;
    
	private LinkedList<Tile> _tiles;
    
    [Serializable]
    public class ChunkTrackBuilderSettings {
        public List<Chunk> chunks;

        public int startingCount = 3;
    }

    public ChunkTrackBuilder (Tile.Factory tileFactory,
						        RandomTileFactory.RTFSettings RTFsettings,
                                ChunkTrackBuilderSettings settings,
                                ImplBuilderFactory.Settings OFsettings,
                                BuilderFactory spawnPatternFactory) {
        _settings = settings;

        _tileFactory = tileFactory;
        _tileFactorySettings = RTFsettings;

        _spawnPatternFactory = spawnPatternFactory;
        _obstacleFactorySettings = OFsettings;
        
        _tiles = new LinkedList<Tile>();
    }
    
    public void Initialize () {
		Start();
	}

	public void Start () {
		// start 1 tile back so main menu looks normal
		Tile start = (Tile) _tileFactory.Create();
		float initZ = -1 * start.meshCollider.bounds.size.z;
		start.transform.position = new Vector3(0.0f, 0.0f, initZ);
		_tiles.AddLast(start);

        for (int i = 1; i < _settings.startingCount; i++) {
            Generate(Vector3.zero, false);
        }
    }
    
    public void Reset () {
        while (_tiles.Count > 0)
		{
			RemoveOldestTile();
		}

		_tiles.Clear();
		_tileFactorySettings.startingCounter = 0;

		Start();
    }

    public void OnSpawnTile(SpawnTileSignal signal) {
        Generate(signal.spawnDirection);
    }

    public void Despawn() {
        RemoveOldestTile();
    }

    private void Generate (Vector3 spawnDirection, bool animate = true) {
        bool offStartingBlock = _tileFactorySettings.startingCounter >= _tileFactorySettings.startingTiles.Count;
        
        if (offStartingBlock) {
            ChunkElement element = GetNextElement();
            _tileFactorySettings.randomTiles = element.tiles;
            _obstacleFactorySettings.builders = element.obstacleBuilders;
        }
		
        Tile previousTile = _tiles.Last.Value;
		Tile nextTile = _tileFactory.Create();
		_tiles.AddLast(nextTile);
		
		Vector3 pTilePos = previousTile.transform.position;
		Vector3 pTileSize = previousTile.meshCollider.bounds.size;

		Quaternion nextRotation = previousTile.transform.rotation * Quaternion.Euler(spawnDirection);

		Vector3 nextOffset = nextRotation * nextTile.placementOffset;
		nextOffset = Vector3.Scale(pTileSize, nextOffset);

		nextTile.transform.position = pTilePos + nextOffset;
		nextTile.transform.rotation = nextRotation;

		if (offStartingBlock &&
                _obstacleFactorySettings.builders.Count > 0) {
			IBuilder pattern = _spawnPatternFactory.Create();
			pattern.SpawnForTile(nextTile);
		}

        if (animate) {
            nextTile.AnimateReveal();
        }
	}

    private void RemoveOldestTile () {
		Tile toDestroy = _tiles.First.Value;
		_tiles.RemoveFirst();
		GameObject.Destroy(toDestroy.gameObject);
	}

    private ChunkElement GetNextElement () {
        UpdateChunk();
        ChunkElement element = _activeChunk.elements[_chunkElementIndex];
        _chunkElementIndex++;
        return element;
    }

    private void UpdateChunk () {
        if (_activeChunk == null ||
                _chunkElementIndex >= _activeChunk.elements.Count) {
            _activeChunk = _settings.chunks[(int)(UnityEngine.Random.value * _settings.chunks.Count)];
            _chunkElementIndex = 0;
        }
    }
}