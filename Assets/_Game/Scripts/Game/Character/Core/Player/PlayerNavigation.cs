using UnityEngine;

public class PlayerNavigation : NavigationCore
{
    [SerializeField]
    Joystick stick;

    public override void UpdateData()
    {
        float horizontal = stick.Horizontal;
        float vertical = stick.Vertical;

        Vector3 move = new(horizontal, 0, vertical);
        MoveDirection = move.normalized;
    }
}