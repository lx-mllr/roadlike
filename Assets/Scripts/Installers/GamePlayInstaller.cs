using System;
using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller<GamePlayInstaller>
{
    public Settings _settings;
    
    [Serializable]
    public struct Settings {
    }

    public override void InstallBindings()
    {
        Container.BindSignal<StartButtonSignal>().ToMethod<GamePlayManager>(x => x.OnGameStart).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<GamePlayManager>(x => x.Reset).FromResolve();

        Container.BindInterfacesAndSelfTo<GamePlayManager>().AsSingle();
    }
}