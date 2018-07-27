using System;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour {

    Canvas _canvas;
    Screens _screens;
    BaseScreen.Factory _factory;

    [Inject]
    public void Init(Canvas canvas, 
                    Screens screens,
                    BaseScreen.Factory factory)
    {
        _canvas = canvas;
        _screens = screens;
        _factory = factory;

        AddMainScreen();
    }

    public void AddMainScreen () {
        _screens.toCreate = _screens.mainScreen;
        BaseScreen screen = _factory.Create();
        screen.transform.SetParent(_canvas.transform, false);
    }
}