using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Joystick joy;

    Vector3 moveDirection;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    bool CanMove;

    private void Awake()
    {
        CameraFollow.Ins.SetTarget(transform);
    }

    public override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);

        if (col.gameObject.CompareTag("Door"))
        {
            Door door = col.gameObject.GetComponent<Door>();
            if (door != null)
            {
                if (transform.position.z - door.transform.position.z > 0)
                {
                    if (!door.Opened)
                    {
                        door.OpenDoor();
                    }

                    FloorBase floor = door.gameObject.transform.parent.GetComponent<FloorBase>();
                    if (floor != null)
                    {
                        floor.Spawn(colorSet.color);
                    }
                }
            }
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        CanMove = true;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

    }

    public override void StopMoving()
    {
        base.StopMoving();
        CanMove = false;
        rb.velocity = Vector3.zero;
    }

    public override void Move()
    {
        if (CanMove)
        {
            base.Move();

            moveDirection = new Vector3(joy.Horizontal, 0, joy.Vertical);

            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

            if (moveDirection != Vector3.zero)
            {
                anim.ChangeAnim("run");

                Quaternion rot = Quaternion.LookRotation(moveDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed);
            }
            else
            {
                anim.ChangeAnim("idle");
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    public override void EndKnockBack()
    {
        base.EndKnockBack();

        anim.ChangeAnim("idle");
        CanMove = true;
    }

    public override void KnockBack()
    {
        base.KnockBack();

        rb.velocity = Vector3.zero;
        CanMove = false;
        anim.ChangeAnim("knockback");
        this.rb.AddForce(-transform.forward * 100);
    }

    public override void Win()
    {
        base.Win();
    }
}
