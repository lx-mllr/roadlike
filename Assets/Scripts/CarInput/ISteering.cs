using UnityEngine;

public interface ISteering
{
    void Move(float xRatio, float yRatio);
    void Reset();
    Transform transform {get;}
}