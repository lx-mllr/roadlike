using System;
using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public SimpleTrackSettings settings;

    [Serializable]
    public class SimpleTrackSettings {
        public RandomTileFactory.RTFSettings tileFactorySettings;
        public TrackBuilder.TrackBuilderSettings trackBuilderSettings;
        public ChunkTrackBuilder.ChunkTrackBuilderSettings chunkTrackBuilderSettings;
        
        public CoinView coinPrefab;
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
        Container.BindInstance(settings.tileFactorySettings);
        Container.BindFactory<Tile, Tile.Factory>().FromFactory<RandomTileFactory>();

        Container.BindInstance(settings.trackBuilderSettings);
        Container.BindInterfacesAndSelfTo<TrackBuilder>().AsSingle().Lazy();
    }

    public void InstallChunkPathSystem () {
        Container.BindInstance(settings.tileFactorySettings);
        Container.BindInstance(settings.chunkTrackBuilderSettings);

        Container.BindFactory<Tile, Tile.Factory>().FromFactory<RandomTileFactory>();
        Container.BindInterfacesAndSelfTo<ChunkTrackBuilder>().AsSingle().Lazy();
    }

    public void InstallCoinSystem() 
    {
        Container.BindFactory<CoinView, CoinView.Factory>().FromComponentInNewPrefab(settings.coinPrefab);
    }
}