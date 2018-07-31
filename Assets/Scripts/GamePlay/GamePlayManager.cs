using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable {

    [Inject]
    SignalBus _signalBus;

    [Inject]
    ISteering _steering;

    [Inject]
    Screens _screens;

    public void Initialize () {
        _signalBus.Fire(new CreateScreenSignal () {
            toCreate = _screens.mainScreen
        });
    }

    public void OnGameStart () {

    }

    public void Reset () {
        _steering.Reset();
    }
}