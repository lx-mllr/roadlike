using System;
using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public Settings settings;

    [Serializable]
    public class Settings {
        public RandomTileFactory.RTFSettings tileFactorySettings;
        
        public CoinView coinPrefab;
    }

    public override void InstallBindings()
    {        
        Container.DeclareSignal<SpawnTileSignal>().OptionalSubscriber();
        Container.DeclareSignal<DespawnTileSignal>().OptionalSubscriber();
        
        Container.DeclareSignal<GameEndSignal>().OptionalSubscriber();

        InstallCoinSystem();
        InstallSimplePathSystem();
    }

    public void InstallSimplePathSystem()
    {
        //Container.BindFactory<Tile, Tile.Factory>().FromComponentInNewPrefab(tile);
        Container.BindInstance(settings.tileFactorySettings);
        Container.BindFactory<Tile, Tile.Factory>().FromFactory<RandomTileFactory>();
        Container.BindInterfacesAndSelfTo<TrackBuilder>().AsSingle();

        Container.BindSignal<SpawnTileSignal>().ToMethod<ITrackBuilder>(x => x.Generate).FromResolve();
        Container.BindSignal<DespawnTileSignal>().ToMethod<ITrackBuilder>(x => x.Despawn).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<ITrackBuilder>(x => x.Reset).FromResolve();
    }

    public void InstallCoinSystem() 
    {
        Container.BindFactory<CoinView, CoinView.Factory>().FromComponentInNewPrefab(settings.coinPrefab);
    }
}