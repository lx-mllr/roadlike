using System;
using UnityEngine;

public struct DisableInputSignal {
}

public struct GameEndSignal {
}

public struct CollectCoinSignal {
}

public struct ApplyForceToCarSignal {
    public float power;
    public Vector3 impactPoint;
    public float radius;
    public float upMod;
}