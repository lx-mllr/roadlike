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
    ScreenFactory.Settings _settings;
    
    public ScreenFactory(DiContainer container,
                            ScreenFactory.Settings settings)
    {
        _container = container;
        _settings = settings;
    }

    public BaseScreen Create()
    {
        return _container.InstantiatePrefabForComponent<BaseScreen>(_settings.toCreate);
    }

    public class Settings {
        public CanvasRenderer toCreate;
    }
}