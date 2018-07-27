using System;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour {

    Canvas _canvas;
    MainScreen.Factory _factory;

    [Inject]
    public void Init(Canvas canvas, MainScreen.Factory factory)
    {
        _canvas = canvas;
        _factory = factory;

        AddMainScreen();
    }

    public void AddMainScreen () {
        MainScreen screen = _factory.Create();
        screen.transform.SetParent(_canvas.transform, false);
    }
}