using UnityEngine;

public class PlayerMovement : MovementCore
{
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    float gravityScale;

    Vector3 velocity;
    readonly float gravity = -9.81f;

    public override void UpdateData()
    {
        base.UpdateData();

        controller.Move(velocity * Time.deltaTime);
    }

    public override void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public override void ApplyGravity(float scale)
    {
        velocity += scale * gravity * Vector3.up;
    }
}