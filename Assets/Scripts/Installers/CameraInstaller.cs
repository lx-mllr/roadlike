using System;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller<CameraInstaller>
{
    public Settings settings;

    [Serializable]
    public class Settings {
        public CamManager.CamSettings CamSettings;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(settings.CamSettings);
        Container.BindInterfacesAndSelfTo<CamManager>().AsSingle().NonLazy();

        Container.BindSignal<StartButtonSignal>().ToMethod<CamManager>(x => x.onStartButton).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<CamManager>(x => x.onShowMainScreen).FromResolve();
    }
}