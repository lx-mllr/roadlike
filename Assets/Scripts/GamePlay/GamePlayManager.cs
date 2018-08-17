using System;
using UnityEngine;
using Zenject;

public class GamePlayManager : IInitializable, ITickable {

    [Serializable]
    public struct GMSettings {
        public float reEnableInputThreshold;
    }

    [Inject] ISteering _steering;
    [Inject] IInputManager _inputManager;
    [Inject] GMSettings _settings;
    [Inject] SignalBus _signalBus;

    private bool _gameActive;

    public void Initialize () {
    }

    public void OnGameStart () {
        _gameActive = true;
    }

    public void Reset () {
        _gameActive = false;
        _steering.Reset();
    }

    public void Tick () {
        if (_gameActive)
        {
            Debug.Log(_steering.rigidBody.velocity.sqrMagnitude);
            if (!_inputManager.Enabled &&
                    _steering.rigidBody.velocity.sqrMagnitude < _settings.reEnableInputThreshold) {
                _signalBus.Fire<EnableInputSignal>();
            }
        }

    }

    public void ApplyForce (ApplyForceToCarSignal signal) {
        _steering.rigidBody.AddExplosionForce(signal.power, signal.impactPoint, signal.radius, 1.5f, ForceMode.Impulse);
    }
}