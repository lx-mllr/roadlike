using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
	readonly TrackBuilderSettings _settings;
    readonly Tile.Factory _tileFactory;
	readonly RandomTileFactory.RTFSettings _factorySettings;
	readonly BuilderFactory _spawnPatternFactory;

	private LinkedList<Tile> _tiles;

	public TrackBuilder (Tile.Factory tileFactory,
						RandomTileFactory.RTFSettings RTFsettings,
						TrackBuilderSettings TBsettings,
						BuilderFactory SPFactory) 
	{
		_settings = TBsettings;
		_tileFactory = tileFactory;
		_factorySettings = RTFsettings;
		_spawnPatternFactory = SPFactory;

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

		// Assuming (0,0,0) for spawn direction forces this to only generate straight tiles. 
		for (int i = 0; i < _settings.generateAheadCount; i++) {
			Generate(Vector3.zero);
		}
	}

	public void Reset () {
		while (_tiles.Count > 0)
		{
			RemoveOldestTile();
		}

		_tiles.Clear();
		_factorySettings.startingCounter = 0;

		Start();
	}

	public void OnSpawnTile(SpawnTileSignal signal) {
		Generate(signal.spawnDirection);
	}

	private void Generate (Vector3 spawnDirection) {
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

		if (_tiles.Count > _settings.generateAheadCount)
		{
			IBuilder pattern = _spawnPatternFactory.Create();
			pattern.SpawnForTile(nextTile);
			nextTile.AnimateReveal();
		}
	}

	public void Despawn () {
		RemoveOldestTile();
	}

	private void RemoveOldestTile () {
		Tile toDestroy = _tiles.First.Value;
		_tiles.RemoveFirst();
		GameObject.Destroy(toDestroy.gameObject);
	}

	 [Serializable]
    public struct TrackBuilderSettings {
		public int generateAheadCount;
	}
}
