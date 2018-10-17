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
        Container.DeclareSignal<EnableTrackingCamSignal>().OptionalSubscriber();
        Container.DeclareSignal<DisableTrackingCamSignal>().OptionalSubscriber();

        Container.BindInstance(settings.CamSettings);
        Container.BindInterfacesAndSelfTo<CamManager>().AsSingle().NonLazy();

        Container.BindSignal<GameStartSignal>().ToMethod<CamManager>(x => x.onStartButton).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<CamManager>(x => x.onShowMainScreen).FromResolve();
        Container.BindSignal<EnableTrackingCamSignal>().ToMethod<CamManager>(x => x.onEnableTracking).FromResolve();
        Container.BindSignal<DisableTrackingCamSignal>().ToMethod<CamManager>(x => x.onDisableTracking).FromResolve();
        Container.BindSignal<ApplyForceToCarSignal>().ToMethod<FollowCam>(x => x.onApplyForce).FromResolve();

    }
}