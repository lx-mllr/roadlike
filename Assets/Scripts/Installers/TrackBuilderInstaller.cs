using System;
using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public ChunkTrackSettings chunkSettings;
    public SimpleTrackSettings simpleTrackSettings;

    [Serializable]
    public class ChunkTrackSettings {
        public RandomTileFactory.RTFSettings tileFactorySettings;
        public ChunkTrackBuilder.ChunkTrackBuilderSettings chunkTrackBuilderSettings;
        
        public CoinView coinPrefab;
    }

    [Serializable]
    public class SimpleTrackSettings {
        public RandomTileFactory.RTFSettings tileFactorySettings;
        public TrackBuilder.TrackBuilderSettings trackBuilderSettings;
    }

    public override void InstallBindings()
    {        
        Container.DeclareSignal<SpawnTileSignal>().OptionalSubscriber();
        Container.DeclareSignal<DespawnTileSignal>().OptionalSubscriber();

        InstallCoinSystem();
        InstallPathSystem();
    }

    public void InstallPathSystem () {
        // InstallSimplePathSystem();
        InstallChunkPathSystem();

        Container.BindSignal<SpawnTileSignal>().ToMethod<ITrackBuilder>(x => x.OnSpawnTile).FromResolve();
        Container.BindSignal<DespawnTileSignal>().ToMethod<ITrackBuilder>(x => x.Despawn).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<ITrackBuilder>(x => x.Reset).FromResolve();
    }

    public void InstallSimplePathSystem () {
        Container.BindInstance(simpleTrackSettings.tileFactorySettings);
        Container.BindFactory<Tile, Tile.Factory>().FromFactory<RandomTileFactory>();

        Container.BindInstance(simpleTrackSettings.trackBuilderSettings);
        Container.BindInterfacesAndSelfTo<TrackBuilder>().AsSingle().Lazy();
    }

    public void InstallChunkPathSystem () {
        Container.BindInstance(chunkSettings.tileFactorySettings);
        Container.BindInstance(chunkSettings.chunkTrackBuilderSettings);

        Container.BindFactory<Tile, Tile.Factory>().FromFactory<RandomTileFactory>();
        Container.BindInterfacesAndSelfTo<ChunkTrackBuilder>().AsSingle().Lazy();
    }

    public void InstallCoinSystem() 
    {
        Container.BindFactory<CoinView, CoinView.Factory>().FromComponentInNewPrefab(chunkSettings.coinPrefab);
    }
}