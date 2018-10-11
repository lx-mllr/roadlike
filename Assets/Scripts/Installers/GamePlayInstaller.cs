using System;
using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller<GamePlayInstaller>
{
    public Settings _settings;
    
    [Serializable]
    public struct Settings {
        public GamePlayManager.GMSettings gmSettings;
    }

    public override void InstallBindings()
    {
        Container.DeclareSignal<GameStartSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameEndSignal>().OptionalSubscriber();
        Container.DeclareSignal<ScoreIncremented>().OptionalSubscriber();
        Container.DeclareSignal<CollectCoinSignal>();
        Container.DeclareSignal<ApplyForceToCarSignal>();

        Container.Bind<SaveManager>().AsSingle();

// User
        Container.BindInterfacesAndSelfTo<User>().AsSingle().NonLazy();

        //Container.BindSignal<CollectCoinSignal>().ToMethod<User>(x => x.IncreaseCoinCount).FromResolve();
        //Container.BindSignal<GameEndSignal>().ToMethod<User>(x => x.SaveState).FromResolve();

// GP
        Container.BindInterfacesAndSelfTo<GamePlayManager>().AsSingle();
        Container.BindInstance<GamePlayManager.GMSettings>(_settings.gmSettings);

        Container.BindSignal<GameStartSignal>().ToMethod<GamePlayManager>(x => x.OnGameStart).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<GamePlayManager>(x => x.OnGameEnd).FromResolve();
        Container.BindSignal<DespawnTileSignal>().ToMethod<GamePlayManager>(x => x.OnTileDespawn).FromResolve();
        Container.BindSignal<ApplyForceToCarSignal>().ToMethod<GamePlayManager>(x => x.ApplyForce).FromResolve();

    }
}