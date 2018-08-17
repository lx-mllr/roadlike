using System;
using UnityEngine;
using Zenject;

public class UIManager : IInitializable {

    [Inject] Canvas _canvas;
    [Inject] Screens _screens;

    public void Initialize () {
        CreateScreen(new CreateScreenSignal() {
            toCreate = _screens.mainScreen
        });
    }

    public void CreateScreen (CreateScreenSignal signal) {
        CanvasRenderer.Instantiate(signal.toCreate, _canvas.transform, false);
    }
}