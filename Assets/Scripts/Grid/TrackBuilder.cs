using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
	readonly TrackBuilderSettings _settings;
    readonly Tile.Factory _tileFactory;
	readonly RandomTileFactory.RTFSettings _factorySettings;
	readonly BuilderFactory _spawnPatternFactory;

	private Tile _previousTile = null;
	private Tile _currentTile = null;
	private ArrayList _tiles;

	public TrackBuilder (Tile.Factory tileFactory,
						RandomTileFactory.RTFSettings RTFsettings,
						TrackBuilderSettings TBsettings,
						BuilderFactory SPFactory) 
	{
		_settings = TBsettings;
		_tileFactory = tileFactory;
		_factorySettings = RTFsettings;
		_spawnPatternFactory = SPFactory;

		_tiles = new ArrayList();
	}

	public void Initialize () {
		Start();
	}

	public void Start () {
		Tile start = (Tile) _tileFactory.Create();
		float initZ = -1 * start.meshCollider.bounds.size.z;
		start.transform.position = new Vector3(0.0f, 0.0f, initZ);
		_tiles.Add(start);

		for (int i = 0; i < _settings.generateAheadCount; i++)
		{
			Generate();
		}
	}

	public void Reset () {
		while (_tiles.Count > 0)
		{
			RemoveTile();
		}

		_tiles.Clear();
		_factorySettings.startingCounter = 0;

		Start();
	}

	public void Generate () {
		_previousTile = (Tile) _tiles[_tiles.Count - 1];
		_currentTile = (Tile) _tileFactory.Create();
		_tiles.Add(_currentTile);
		
		Vector3 pTilePos = _previousTile.transform.position;

		Vector3 pTileSize = _previousTile.meshCollider.bounds.size;
		Vector3 nextOffset = _previousTile.transform.rotation * _currentTile.placementOffset;
		nextOffset = Vector3.Scale(pTileSize, nextOffset);

		_currentTile.transform.position = pTilePos + nextOffset;
		_currentTile.transform.rotation = _previousTile.transform.rotation * _currentTile.transform.rotation;

		if (_factorySettings.startingCounter >= _factorySettings.startingTiles.Length
			&& _tiles.Count > _settings.generateAheadCount)
		{
			IBuilder pattern = _spawnPatternFactory.Create();
			pattern.SpawnForTile(_currentTile);
			_currentTile.AnimateReveal();
		}
	}

	public void Despawn () {
		RemoveTile();
	}

	private void RemoveTile (int index = 0) {
		if (index >= 0
			 && index < _tiles.Count)
		{
			Tile toDestroy = (Tile) _tiles[0];
			_tiles.RemoveAt(0);
			GameObject.Destroy(toDestroy.gameObject);
		}
	}

	 [Serializable]
    public struct TrackBuilderSettings {
		public int generateAheadCount;
	}
}
