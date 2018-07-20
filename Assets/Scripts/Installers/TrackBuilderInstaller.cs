using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public Tile tile;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<SpawnTileSignal>().OptionalSubscriber();
        Container.DeclareSignal<DespawnTileSignal>().OptionalSubscriber();

        InstallSimplePathSystem();
    }

    public void InstallSimplePathSystem()
    {
        Container.BindFactory<Tile, Tile.Factory>().FromComponentInNewPrefab(tile);
        Container.BindInterfacesAndSelfTo<TrackBuilder>().AsSingle();

        Container.BindSignal<SpawnTileSignal>().ToMethod<ITrackBuilder>(x => x.Generate).FromResolve();
        Container.BindSignal<DespawnTileSignal>().ToMethod<ITrackBuilder>(x => x.Despawn).FromResolve();
    }
}