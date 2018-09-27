using System;
using UnityEngine;
using Zenject;

public class TriggerStart : MonoBehaviour {

    SignalBus _signalBus;

    [Inject]
    public void Init (SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void OnClick ()
    {
        _signalBus.Fire<GameStartSignal>();
    }
}