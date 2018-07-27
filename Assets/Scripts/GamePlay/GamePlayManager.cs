using System;
using Zenject;

public class GamePlayManager : IInitializable {

    IInputManager _inputManager;

    public GamePlayManager(IInputManager inputManager) {
        _inputManager = inputManager;
    }

    public void Initialize () {
        _inputManager.Enabled = false;
    }

    public void OnGameStart () {
        _inputManager.Enabled = true;
    }
}