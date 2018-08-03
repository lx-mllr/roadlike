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
        Container.DeclareSignal<CreateScreenSignal>().OptionalSubscriber();

        Container.BindInstance(screens);
        Container.BindInstance(canvas);

        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
        Container.BindSignal<CreateScreenSignal>().ToMethod<UIManager>(x => x.CreateScreen).FromResolve();
    }
}

[Serializable]
public class Screens {
    public CanvasRenderer mainScreen;
}