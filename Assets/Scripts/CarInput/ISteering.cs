using UnityEngine;

public interface ISteering
{
    void Move(float steering, float accel, float footbrake, float handbrake);
    void Reset();
    Vector2 inputRatios { get; }
    Transform transform { get; }
    Rigidbody rigidBody { get; }
}