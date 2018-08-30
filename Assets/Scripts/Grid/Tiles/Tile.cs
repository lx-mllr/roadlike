using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour {

	[Inject] CoinView.Factory _coinFactory;
	[Inject] ObstacleView.Factory _hammerFactory;

	// Use this for initialization
	public Vector3 placementOffset;

	public MeshCollider meshCollider;

	private CoinView _coin;
	private ObstacleView _hammer;

	public void SpawnCoin () {
		_coin = _coinFactory.Create();
		Vector3 tSize = meshCollider.bounds.size;
		Vector3 pPos = transform.position;// + (UnityEngine.Random.Range(0.0f, 0.5f) * tSize);
		pPos.x += (UnityEngine.Random.Range(-0.25f, 0.25f) * tSize.x);
		pPos.y = 1;
		_coin.transform.position = pPos;
	}

	public void SpawnHammer () {
		_hammer = _hammerFactory.Create();
		Vector3 pos = transform.position;
		Vector3 tSize = meshCollider.bounds.size;
		pos.y = _hammer.transform.position.y;
		pos.z += (UnityEngine.Random.Range(-0.25f, 0.25f) * tSize.z);
		_hammer.transform.position = pos;
	}

	void OnDestroy () {
		if (_coin && _coin.gameObject) {
			Destroy(_coin.gameObject);
		}
		
		if (_hammer && _hammer.gameObject) {
			Destroy(_hammer.gameObject);
		}
	}

	public class Factory : PlaceholderFactory<Tile>
	{
	}
}
