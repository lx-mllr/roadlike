using System;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
    readonly Tile.Factory _tileFactory;

	private Tile _previousTile = null;
	private Tile _currentTile = null;

	public TrackBuilder(Tile.Factory tileFactory)
	{
		_tileFactory = tileFactory;
	}

	public void Initialize()
	{
		_currentTile = (Tile) _tileFactory.Create();
		_currentTile.transform.position = _currentTile.transform.localScale / 2;
	}

	public void Generate()
	{
		_previousTile = _currentTile;
		_currentTile = (Tile) _tileFactory.Create();

		Vector3 pos = _previousTile.transform.position;
		Vector3 size = _previousTile.GetComponent<Collider>().bounds.size;
		_currentTile.transform.position = new Vector3(pos.x, pos.y, pos.z + size.z);
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
