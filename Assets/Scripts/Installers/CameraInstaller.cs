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

        Container.BindSignal<GameStartSignal>().ToMethod<CamManager>(x => x.onStartButton).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<CamManager>(x => x.onShowMainScreen).FromResolve();
        Container.BindSignal<DisableInputSignal>().ToMethod<CamManager>(x => x.onInputDisable).FromResolve();
        Container.BindSignal<EnableInputSignal>().ToMethod<CamManager>(x => x.onInputEnable).FromResolve();
        Container.BindSignal<ApplyForceToCarSignal>().ToMethod<FollowCam>(x => x.onApplyForce).FromResolve();

    }
}