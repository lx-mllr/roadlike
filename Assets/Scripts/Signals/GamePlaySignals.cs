using System;
using UnityEngine;

public struct DisableInputSignal {
}

public struct EnableInputSignal {
}

public struct GameStartSignal {
}

public struct GameEndSignal {
}

public struct CollectCoinSignal {
}

public struct ScoreIncremented {
}

public struct ApplyForceToCarSignal {
    public float power;
    public Vector3 impactPoint;
    public float radius;
    public float upMod;
}