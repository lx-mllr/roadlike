using System;
using UnityEngine;
using Zenject;

public class UIManager : IInitializable {

    [Inject] Canvas _canvas;
    [Inject] Screens _screens;

    private CanvasRenderer _activeScreen;
    public CanvasRenderer activeScreen { get { return _activeScreen; } }

    public void Initialize () {
        CreateScreen(new CreateScreenSignal());
    }

    public void CreateScreen (CreateScreenSignal signal) {
        if (_activeScreen) {
            DestroyScreen();
        }

        if (signal.toCreate == null) {
            signal.toCreate = _screens.mainScreen; // i feel meh about this
        }

        _activeScreen = CanvasRenderer.Instantiate(signal.toCreate, _canvas.transform, false);
    }

    public void DestroyScreen () {
        if (_activeScreen) {
            GameObject.Destroy(_activeScreen.gameObject);
            _activeScreen = null;
        }
    }
}