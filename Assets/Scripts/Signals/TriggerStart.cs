using System;
using UnityEngine;
using Zenject;

public class TriggerStart : MonoBehaviour {

	[Inject]
	SignalBus signalBus {get; set;}

    public void OnClick ()
    {
        signalBus.Fire<StartButtonSignal>();
    }
}