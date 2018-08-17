using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable {

    [Inject]
    ISteering _steering;

    [Inject]
    Screens _screens;

    [Inject]
    SignalBus _signalBus;

    public void Initialize () {
        _signalBus.Fire(new CreateScreenSignal() {
            toCreate = _screens.mainScreen
        });
    }

    public void OnGameStart () {
    }

    public void Reset () {
        _steering.Reset();
    }

    public void ApplyForce (ApplyForceToCarSignal signal) {
        _steering.rigidBody.AddExplosionForce(signal.power, signal.impactPoint, signal.radius, 1.5f, ForceMode.Impulse);
    }
}