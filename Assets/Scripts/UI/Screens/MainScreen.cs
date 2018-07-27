using UnityEngine;
using Zenject;

public class MainScreen : MonoBehaviour {

    public void DestroyScreen () {
        Destroy(this.gameObject);
    }

    public class Factory : PlaceholderFactory<MainScreen> {
        
    }
}

public class MainScreenFactory : IFactory<MainScreen> {

    DiContainer _container;
    Screens _screens;
    
    public MainScreenFactory(DiContainer container, Screens screens)
    {
        _container = container;
        _screens = screens;
    }

    public MainScreen Create()
    {
        return _container.InstantiatePrefabForComponent<MainScreen>(_screens.mainScreen);
    }
}
