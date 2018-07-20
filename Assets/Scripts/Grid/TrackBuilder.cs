using System;
using UnityEngine;
using Zenject;

public class TrackBuilder : ITrackBuilder  {
	
	readonly SignalBus _signalBus;
    readonly Tile.Factory _tileFactory;

	enum TrackSkin {
		DEFAULT
	};

	public TrackBuilder(SignalBus signalBus, Tile.Factory tileFactory)
	{
		_tileFactory = tileFactory;
		_signalBus = signalBus;

		_signalBus.Subscribe<SpawnTileSignal>(Generate);
	}

	public void Generate()
	{
		Tile t = _tileFactory.Create();
		t.transform.rotation = Quaternion.Euler(-45, 0, 45);
		t.transform.position = new Vector3(0, 0, 60);
		Debug.Log("TESTING");
		
		_signalBus.Unsubscribe<SpawnTileSignal>(Generate);
	}
}
