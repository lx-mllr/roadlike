using UnityEngine;

public interface ISteering
{
    void Move(float steering, float accel, float footbrake, float handbrake);
    void Reset();
    Transform transform { get; }
    Rigidbody rigidBody { get; }
}