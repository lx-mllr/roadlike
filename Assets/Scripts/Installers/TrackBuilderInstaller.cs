using UnityEngine;
using Zenject;

public class TrackBuilderInstaller : MonoInstaller<TrackBuilderInstaller>
{
    public Tile tile;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.DeclareSignal<SpawnTileSignal>().OptionalSubscriber();

        Container.BindFactory<Tile, Tile.Factory>().FromComponentInNewPrefab(tile);
        Container.Bind<ITrackBuilder>().To<TrackBuilder>().AsSingle().NonLazy();
    }
}