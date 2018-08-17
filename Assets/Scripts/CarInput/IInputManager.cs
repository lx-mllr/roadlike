using UnityEngine;
using Zenject;

public interface IInputManager : IInitializable, ITickable {
    bool Enabled { get; }
    void Enable();
    void Reset();
}