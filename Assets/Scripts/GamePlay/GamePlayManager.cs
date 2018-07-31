using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable {

    private ShowMainScreenTrigger _reseter;

    SignalBus _signalBus;
    ISteering _steering;
    ShowMainScreenTrigger.Factory _reseterFactory;

    public GamePlayManager(ISteering steering,
                            ShowMainScreenTrigger.Factory factory,
                            SignalBus signalBus) {
        _steering = steering;
        _reseterFactory = factory;

        _signalBus = signalBus;
    }

    public void Initialize () {
        _signalBus.Fire<ShowMainScreenSignal>();
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