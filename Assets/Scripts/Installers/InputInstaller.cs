using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller<InputInstaller>
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<DisableInputSignal>();
        Container.DeclareSignal<EnableInputSignal>();

        #if UNITY_EDITOR
            Container.BindInterfacesAndSelfTo<EditorInputManager>().AsSingle();
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        #else
            Container.BindInterfacesAndSelfTo<LeftRightInputManager>().AsSingle();
            // Container.BindInterfacesAndSelfTo<SwipeInputManager>().AsSingle();
        #endif

        Container.BindSignal<GameStartSignal>().ToMethod<IInputManager>(x => x.Enable).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<IInputManager>(x => x.Reset).FromResolve();
        
        Container.BindSignal<EnableInputSignal>().ToMethod<IInputManager>(x => x.Enable).FromResolve();
        Container.BindSignal<DisableInputSignal>().ToMethod<IInputManager>(x => x.Reset).FromResolve();
    }
}