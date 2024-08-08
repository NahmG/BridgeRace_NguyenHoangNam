using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Bot : Character
{
    [SerializeField] float speed;
    [SerializeField] float rotate;
    [SerializeField] float sight;
        
    Vector3 direction;
    Vector3 destination;
    Vector3 nextDestination;

    private IState currentState;
    int index;
    bool canMove;
    private FloorBase floor;

    Character target;
    public Character Target => target;


    public override void Update()
    {
        base.Update();

        currentState?.OnExecute(this);

        Debug.DrawLine(transform.position, destination, UnityEngine.Color.yellow);

        if (ArriveDestination(destination))
        {
            destination = nextDestination;
        }
    }

    public override void OnCollisionEnter(Collision col)
    {
        base.OnCollisionEnter(col);

        if (col.gameObject.CompareTag("Bot") || col.gameObject.CompareTag("Player"))
        {
            Character c = col.gameObject.GetComponent<Character>();
            if(this.BrickCollected > c.BrickCollected)
            {
                ChangeState(new CollectBrickState());
            }
        }
    }

    public override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);

        if (col.gameObject.CompareTag("Door"))
        {
            Door door = col.gameObject.GetComponent<Door>();

            if (!door.Opened)
            {
                door.OpenDoor();
            }

            FloorBase floor = door.gameObject.transform.parent.GetComponent<FloorBase>();
            if (floor != null)
            {
                floor.Spawn(colorSet.color);
                SetFloor(floor);
                ChangeState(new CollectBrickState());
            }
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        canMove = true;
        destination = transform.position;
        ChangeState(new CollectBrickState());

        Floor1 floor = FindObjectOfType<Floor1>();
        SetFloor(floor);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void Move()
    {
        base.Move();        

        if(canMove)
        {
            Vector3 translation = destination - transform.position;
            translation.y = 0f;

            direction = translation.normalized;

            anim.ChangeAnim("run");
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotate);
        }
        else
        {
            direction = Vector3.zero;
        }
        

        if (!ArriveDestination(destination))
        {
            rb.velocity = new Vector3(direction.x * speed, rb.velocity.y, direction.z * speed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public override void StopMoving()
    {
        destination = transform.position;
        anim.ChangeAnim("idle");
    }

    public bool TargetInRange()
    {
        if(target != null && Vector3.Distance(transform.position, target.transform.position) <= sight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal void SetTarget(Character target)
    {
        if (ray.OnBridge(this))
        {
            return;
        }
        this.target = target;
        if (TargetInRange())
        {
            if(this.BrickCollected > target.BrickCollected)
            {
                ChangeState(new KickTargetState());
            }
        }
    }

    public override void EndKnockBack()
    {
        base.EndKnockBack();

        canMove = true; 
        anim.ChangeAnim("idle");
        ChangeState(new CollectBrickState());
    }

    public override void KnockBack()
    {
        base.KnockBack();
        canMove = false;
        StopMoving();
        
        ChangeState(null);

        anim.ChangeAnim("knockback");
        this.rb.AddForce(-transform.forward * 100);
    }
    
    public void FindTarget()
    {
        if(target != null)
        {
            SetDestination(target.transform.position);
        }
    }

    public void FindBrick()
    {
        SetDestination(GetClosestBrick());
    }

    public void GoOnBridge()
    {
        Vector3 des = floor.bridges[index].start.position;
        SetDestination(des);
        if (ArriveDestination(des))
        {
            SetDestination(floor.bridges[index].end.position);
        }
    }

    public override void Win()
    {
        base.Win();

        canMove = false;
        ChangeState(null);
    }

    public override void Lose()
    {
        base.Lose();

        canMove = false;
        ChangeState(null);
    }

    public void SetDestination(Vector3 newDes)
    {
        nextDestination = newDes;
    }

    private bool ArriveDestination(Vector3 des)
    {
        Vector3 currentPos = transform.position;
        currentPos.y = 0f;
        des.y = 0f;

        return (des - currentPos).magnitude < 0.2f;
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    #region Other
    public void ResetIndex()
    {
        Bridge[] brs = floor.bridges;
        if (Equal(brs))
        {
            index = UnityEngine.Random.Range(0, brs.Length);
        }
        else
        {
            index = Max(brs);
        }
    }

    public void SetFloor(FloorBase floor)
    {
        this.floor = floor;
    }

    private Vector3 GetClosestBrick()
    {
        FloorBrick[] bricks = FindObjectsOfType<FloorBrick>();

        FloorBrick closestBrick = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (FloorBrick brick in bricks)
        {
            if (brick.Color.color == colorSet.color && brick.Active)
            {
                float distance = Vector3.Distance(currentPosition, brick.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBrick = brick;
                }
            }
        }

        if (closestBrick != null)
        {
            return closestBrick.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private bool Equal(Bridge[] brs)
    {
        int[] c = new int[brs.Length];
        for (int i = 0; i < c.Length; i++)
        {
            c[i] = brs[i].GetCount(colorSet.color);
        }
        for (int i = 1; i < c.Length; i++)
        {
            if (c[i] != c[0])
            {
                return false;
            }
        }
        return true;
    }

    private int Max(Bridge[] brs)
    {
        int ind = 0;
        int max = brs[0].GetCount(colorSet.color);
        for (int i = 0; i < brs.Length; i++)
        {
            if (max < brs[i].GetCount(colorSet.color))
            {
                ind = i;
            }
        }
        return ind;
    }

    #endregion
}
