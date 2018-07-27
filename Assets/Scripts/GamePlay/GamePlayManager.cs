using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable {

    private ShowMainScreenTrigger _reseter;

    IInputManager _inputManager;
    ISteering _steering;
    ShowMainScreenTrigger.Factory _reseterFactory;

    public GamePlayManager(IInputManager inputManager,
                            ISteering steering,
                            ShowMainScreenTrigger.Factory factory) {
        _inputManager = inputManager;
        _steering = steering;
        _reseterFactory = factory;
    }

    public void Initialize () {
    }
    
    public void DisableInput () {
        _inputManager.Enabled = false;
    }

    public void OnGameStart () {
        _inputManager.Enabled = true;
        _reseter = _reseterFactory.Create();
    }

    public void Reset () {
        _steering.Reset();
        if (_reseter) {
            _reseter.CleanUp();
        }
    }
}