using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tile : MonoBehaviour {

	[Inject] CoinView.Factory _coinFactory;

	// Use this for initialization
	public Vector3 placementOffset;

	public MeshCollider meshCollider;

	private CoinView _coin;

	public void SpawnCoin () {
		_coin = _coinFactory.Create();
		Vector3 tSize = meshCollider.bounds.size;
		Vector3 pPos = transform.position;// + (UnityEngine.Random.Range(0.0f, 0.5f) * tSize);
		pPos.x += (UnityEngine.Random.Range(-0.25f, 0.25f) * tSize.x);
		pPos.y = 1;
		_coin.transform.position = pPos;
	}

	void OnDestroy () {
		if (_coin && _coin.gameObject) {
			Destroy(_coin.gameObject);
		}
	}

	public class Factory : PlaceholderFactory<Tile>
	{
	}
}
