using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable, ITickable {

    [Serializable]
    public struct GMSettings {
        public float reEnableInputThreshold;
    }

    [Inject] ISteering _steering;
    [Inject] GMSettings _settings;
    [Inject] SignalBus _signalBus;

    private bool _gameActive;

    public void Initialize () {
    }

    public void OnGameStart () {
        _gameActive = true;
    }

    public void OnGameEnd () {
        _gameActive = false;
        _steering.Reset();

        // default signal will create main screen, and remove currently active (gameHUD)
        _signalBus.Fire<CreateScreenSignal>();
    }

    public void Tick () {

    }

    public void ApplyForce (ApplyForceToCarSignal signal) {
        _steering.rigidBody.AddExplosionForce(signal.power, signal.impactPoint, signal.radius, signal.upMod, ForceMode.Impulse);
    }
}