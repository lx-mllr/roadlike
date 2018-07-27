using System;
using UnityEngine;
using Zenject;

public class UIManager : IInitializable {

    Canvas _canvas;
    Screens _screens;
    BaseScreen.Factory _factory;
    ScreenFactory.Settings _factorySettings;

    public UIManager(Canvas canvas, 
                    Screens screens,
                    BaseScreen.Factory factory,
                    ScreenFactory.Settings settings)
    {
        _canvas = canvas;
        _screens = screens;
        _factory = factory;
        _factorySettings = settings;
    }

    public void Initialize () {
        AddMainScreen();
    }

    public void AddMainScreen () {
        _factorySettings.toCreate = _screens.mainScreen;
        BaseScreen screen = _factory.Create();
        screen.transform.SetParent(_canvas.transform, false);
    }
}