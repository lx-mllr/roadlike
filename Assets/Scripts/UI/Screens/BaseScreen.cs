using System;
using UnityEngine;
using Zenject;

public class BaseScreen : MonoBehaviour {
    
    public void DestroyScreen () {
        Destroy(this.gameObject);
    }
    
    public class Factory : PlaceholderFactory<BaseScreen> {
        
    }
}

public class ScreenFactory : IFactory<BaseScreen> {

    DiContainer _container;
    Screens _screens;
    
    public ScreenFactory(DiContainer container, Screens screens)
    {
        _container = container;
        _screens = screens;
    }

    public BaseScreen Create()
    {
        return _container.InstantiatePrefabForComponent<BaseScreen>(_screens.toCreate);
    }
}