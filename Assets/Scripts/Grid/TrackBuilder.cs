﻿using System;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
    readonly Tile.Factory _tileFactory;

	private Tile _previousTile = null;
	private Tile _currentTile = null;

	private Quaternion rotation;

	public TrackBuilder(Tile.Factory tileFactory)
	{
		_tileFactory = tileFactory;
	}

	public void Initialize()
	{
		_currentTile = (Tile) _tileFactory.Create();
	}

	public void Generate()
	{
		_previousTile = _currentTile;
		_currentTile = (Tile) _tileFactory.Create();

		rotation *= Quaternion.Euler(_previousTile.nextTileEuler);
		Vector3 pPos = _previousTile.transform.position;
		Vector3 tSize = _previousTile.meshCollider.bounds.size;
		Vector3 nextOffset = rotation * _previousTile.nextGridPos;
		nextOffset = new Vector3(nextOffset.x * tSize.x, 
											nextOffset.y * tSize.y,
											nextOffset.z * tSize.z);

		_currentTile.transform.position = new Vector3(pPos.x + nextOffset.x, pPos.y + nextOffset.y, pPos.z + nextOffset.z);
		_currentTile.transform.rotation = Quaternion.Euler(_previousTile.nextTileEuler) * _currentTile.transform.rotation;

	}

	public void Despawn()
	{
		if (_previousTile)
		{
			Debug.Log("On Destory");
			GameObject.Destroy(_previousTile.gameObject);
		}
	}
}
