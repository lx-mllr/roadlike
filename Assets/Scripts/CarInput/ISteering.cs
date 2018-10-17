using UnityEngine;

public interface ISteering
{
    bool IsGrounded(out bool canSteer, out bool canDrive);
    void Move(float steering, float accel, float footbrake, float handbrake);
    void Reset();
    Transform transform { get; }
    Rigidbody rigidBody { get; }
}