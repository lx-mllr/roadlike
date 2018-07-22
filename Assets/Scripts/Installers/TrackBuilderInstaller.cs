using System;
using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public Settings settings;

    [Serializable]
    public class Settings {
        public RandomTileFactory.RTFSettings tileFactorySettings;
    }

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<SpawnTileSignal>().OptionalSubscriber();
        Container.DeclareSignal<DespawnTileSignal>().OptionalSubscriber();

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
    }
}