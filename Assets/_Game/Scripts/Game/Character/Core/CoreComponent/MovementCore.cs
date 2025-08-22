using UnityEngine;

public abstract class MovementCore : BaseCore
{
    public virtual void SetVelocity(Vector3 velocity) { }
    public virtual void ApplyGravity(float scale) { }
}