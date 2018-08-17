﻿using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
	readonly TrackBuilderSettings _settings;
    readonly Tile.Factory _tileFactory;
	readonly RandomTileFactory.RTFSettings _factorySettings;


	private Tile _previousTile = null;
	private Tile _currentTile = null;
	private ArrayList _tiles;

	public TrackBuilder (Tile.Factory tileFactory,
						RandomTileFactory.RTFSettings RTFsettings,
						TrackBuilderSettings TBsettings) 
	{
		_settings = TBsettings;
		_tileFactory = tileFactory;
		_factorySettings = RTFsettings;
		_tiles = new ArrayList();
	}

	public void Initialize () {
		Start();
	}

	public void Start () {
		_tiles.Add((Tile) _tileFactory.Create());
		for (int i = 0; i < _settings.generateAheadCount; i++)
		{
			Generate();
		}
	}

	public void Reset () {
		Tile toDestroy;
		while (_tiles.Count > 0)
		{
			toDestroy = (Tile) _tiles[0];
			_tiles.RemoveAt(0);
			CleanUpTile(toDestroy);
		}

		_tiles.Clear();
		_factorySettings.startingCounter = 0;

		Start();
	}

	public void Generate () {
		_previousTile = (Tile) _tiles[_tiles.Count - 1];
		_currentTile = (Tile) _tileFactory.Create();
		_tiles.Add(_currentTile);
		
		Quaternion rot = _previousTile.transform.rotation;
		Vector3 pPos = _previousTile.transform.position;
		Vector3 tSize = _previousTile.meshCollider.bounds.size;
		Vector3 nextOffset = rot * _previousTile.nextGridPos;
		nextOffset = new Vector3(nextOffset.x * tSize.x, 
											nextOffset.y * tSize.y,
											nextOffset.z * tSize.z);

		_currentTile.transform.position = new Vector3(pPos.x + nextOffset.x, pPos.y + nextOffset.y, pPos.z + nextOffset.z);
		_currentTile.transform.rotation = Quaternion.Euler(_previousTile.nextTileEuler) * rot;

		
		if (_factorySettings.startingCounter < _tiles.Count)
		{
			if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f) {
				_currentTile.SpawnCoin();
			}
			else {
				_currentTile.SpawnHammer();
			}
		}
	}

	public void Despawn () {
			Tile toDestroy = (Tile) _tiles[0];
			_tiles.RemoveAt(0);
			CleanUpTile(toDestroy);
	}

	private void CleanUpTile (Tile t) {
		if (t) {
			GameObject.Destroy(t.gameObject);
		}
	}

	 [Serializable]
    public struct TrackBuilderSettings {
		public int generateAheadCount;
	}
}
