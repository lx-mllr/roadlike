using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller<InputInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<DisableInputSignal>();

        #if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EditorInputManager>().AsSingle();
        #else
            Container.BindInterfacesAndSelfTo<LeftRightInputManager>().AsSingle();
            // Container.BindInterfacesAndSelfTo<SwipeInputManager>().AsSingle();
        #endif

        Container.BindSignal<StartButtonSignal>().ToMethod<IInputManager>(x => x.Enable).FromResolve();
        Container.BindSignal<DisableInputSignal>().ToMethod<IInputManager>(x => x.Reset).FromResolve();
    }
}