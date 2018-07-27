using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISystemInstaller : MonoInstaller<UISystemInstaller>
{
    public Screens screens;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<StartButtonSignal>().OptionalSubscriber();

        Container.BindInstance(screens);
        Container.Bind<Canvas>().FromComponentSibling();
        Container.Bind<UIManager>().FromComponentSibling();
        Container.BindFactory<MainScreen, MainScreen.Factory>().FromFactory<MainScreenFactory>();
    }
}

[Serializable]
public class Screens {
    public CanvasRenderer mainScreen;
}