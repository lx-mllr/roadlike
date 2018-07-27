using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISystemInstaller : MonoInstaller<UISystemInstaller>
{
    public Screens screens;
    public Canvas canvas;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<StartButtonSignal>().OptionalSubscriber();
        Container.DeclareSignal<ShowMainScreenSignal>().OptionalSubscriber();

        Container.BindInstance(new ScreenFactory.Settings());
        Container.BindFactory<BaseScreen, BaseScreen.Factory>().FromFactory<ScreenFactory>();

        Container.BindInstance(screens);
        Container.BindInstance(canvas);
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
        Container.BindSignal<ShowMainScreenSignal>().ToMethod<UIManager>(x => x.AddMainScreen).FromResolve();
    }
}

[Serializable]
public class Screens {
    public CanvasRenderer mainScreen;
}