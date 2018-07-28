using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable {

    private ShowMainScreenTrigger _reseter;

    ISteering _steering;
    ShowMainScreenTrigger.Factory _reseterFactory;

    public GamePlayManager(ISteering steering,
                            ShowMainScreenTrigger.Factory factory) {
        _steering = steering;
        _reseterFactory = factory;
    }

    public void Initialize () {
    }

    public void OnGameStart () {
        _reseter = _reseterFactory.Create();
    }

    public void Reset () {
        _steering.Reset();
        if (_reseter) {
            _reseter.CleanUp();
        }
    }
}