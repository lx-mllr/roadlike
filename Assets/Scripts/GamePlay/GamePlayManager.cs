using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable, ITickable {

    [Serializable]
    public struct GMSettings {
        public float reEnableInputThreshold;
    }

    [Inject] ISteering _steering;
    [Inject] User _user;
    [Inject] GMSettings _settings;
    [Inject] SignalBus _signalBus;

    private bool _gameActive;
    private int roundScore;

    public void Initialize () {
    }

    public void OnGameStart () {
        _gameActive = true;
        roundScore = 0;
    }

    public void OnGameEnd () {
        _gameActive = false;
        _steering.Reset();
        _user.OnGameEnd(roundScore);

        // default signal will create main screen, and remove currently active (gameHUD)
        _signalBus.Fire<CreateScreenSignal>();
    }

    public void Tick () {

    }

    public void OnTileDespawn () {
        roundScore++;
        _signalBus.Fire<ScoreIncremented>();
    }

    public void ApplyForce (ApplyForceToCarSignal signal) {
        _steering.rigidBody.AddExplosionForce(signal.power, signal.impactPoint, signal.radius, signal.upMod, ForceMode.Impulse);
    }
}