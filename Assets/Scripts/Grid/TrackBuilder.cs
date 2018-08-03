using System;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder, IInitializable  {
	
    readonly Tile.Factory _tileFactory;
	readonly RandomTileFactory.RTFSettings _factorySettings;

	readonly CoinView.Factory _coinFactory;

	private Tile _previousTile = null;
	private Tile _currentTile = null;

	public TrackBuilder (Tile.Factory tileFactory,
						RandomTileFactory.RTFSettings settings,
						CoinView.Factory coinFactory) {
		_tileFactory = tileFactory;
		_factorySettings = settings;
		_coinFactory = coinFactory;
	}

	public void Initialize () {
		Start();
	}

	public void Start () {
		_currentTile = (Tile) _tileFactory.Create();
	}

	public void Reset () {
		CleanUpTile(_previousTile);
		CleanUpTile(_currentTile);
		_previousTile = null;
		_currentTile = null;
		_factorySettings.startingCounter = 0;

		Start();
	}

	public void Generate () {
		_previousTile = _currentTile;
		_currentTile = (Tile) _tileFactory.Create();
		
		Quaternion rot = _previousTile.transform.rotation;
		Vector3 pPos = _previousTile.transform.position;
		Vector3 tSize = _previousTile.meshCollider.bounds.size;
		Vector3 nextOffset = rot * _previousTile.nextGridPos;
		nextOffset = new Vector3(nextOffset.x * tSize.x, 
											nextOffset.y * tSize.y,
											nextOffset.z * tSize.z);

		_currentTile.transform.position = new Vector3(pPos.x + nextOffset.x, pPos.y + nextOffset.y, pPos.z + nextOffset.z);
		_currentTile.transform.rotation = Quaternion.Euler(_previousTile.nextTileEuler) * rot;

		CoinView coin = _coinFactory.Create();
		tSize = _currentTile.meshCollider.bounds.size;
		pPos = _currentTile.transform.position + (UnityEngine.Random.Range(-0.7f, 0.7f) * tSize);
		pPos.y = 1;
		coin.transform.position = pPos;
	}

	public void Despawn () {
		CleanUpTile(_previousTile);
	}

	private void CleanUpTile (Tile t) {
		if (t) {
			GameObject.Destroy(t.gameObject);
		}
	}
}
