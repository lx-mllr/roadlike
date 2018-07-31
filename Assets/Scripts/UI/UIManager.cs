using System;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour {

    [Inject]
    Canvas _canvas;

    [Inject]
    Screens _screens;

    public void AddMainScreen () {
        Instantiate(_screens.mainScreen, _canvas.transform, false);
    }
}