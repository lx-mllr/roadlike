using UnityEngine;
using Zenject;

public interface IInputManager : IInitializable, ITickable {
    void Enable();
    void Reset();
}