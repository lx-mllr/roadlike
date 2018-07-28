using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller<InputInstaller>
{
    public override void InstallBindings()
    {
        #if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EditorInputManager>().AsSingle();
        #else
            Container.BindInterfacesAndSelfTo<LeftRightInputManager>().AsSingle();
        #endif

        Container.BindSignal<StartButtonSignal>().ToMethod<IInputManager>(x => x.Enable).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<IInputManager>(x => x.Reset).FromResolve();
    }
}