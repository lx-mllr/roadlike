using UnityEngine;
using Zenject;

public class GamePlayInstaller : MonoInstaller<GamePlayInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GamePlayManager>().AsSingle();
        Container.BindSignal<StartButtonSignal>().ToMethod<GamePlayManager>(x => x.OnGameStart).FromResolve();
    }
}