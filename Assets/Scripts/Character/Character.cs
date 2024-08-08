using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField] protected Rigidbody rb;

    [SerializeField] Vector3 spawnPoint;

    [SerializeField] Renderer rend;
    public ColorSet colorSet;

    [SerializeField] Transform pickUp;
    [SerializeField] Brick brickPref;

    Stack<Brick> bricks = new Stack<Brick>();

    [SerializeField] int brickCollected;
    public int BrickCollected => brickCollected;

    public RaycastResponse ray;
    public CharacterAnim anim;

    public virtual void Update()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, 3, ray.OnBridge(this));
        Physics.IgnoreLayerCollision(gameObject.layer, 7, ray.OnBridge(this));
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Brick"))
        {
            CollectBrick(col.gameObject);
        }

        if (col.gameObject.CompareTag("OnBridge"))
        {
            BuildBridge(col.gameObject);
        }
    }

    public virtual void OnTriggerExit(Collider col)
    {

    }

    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bot") || col.gameObject.CompareTag("Player"))
        {
            Character c = col.gameObject.GetComponent<Character>();

            if(this.brickCollected <= c.brickCollected)
            {
                this.ScatterBrick();
                this.KnockBack();
                Invoke(nameof(EndKnockBack), 1f);

            }   
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("OnBridge"))
        {
            col.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    #region Init
    public virtual void OnInit()
    {
        transform.position = spawnPoint;
        anim.ChangeAnim("idle");
        rend.material.color = colorSet.color;
        brickCollected = 0;
    }

    public virtual void OnDespawn()
    {
        if(brickCollected > 0)
        {
            while (bricks.Count > 0)
            {
                Destroy(bricks.Pop().gameObject);
            }
        }
    }

    #endregion

    #region Action

    public virtual void Move()
    {
        rb.useGravity = !ray.OnBridge(this);
        if (ray.OnBridge(this))
        {
            if (ray.OffBridge(this))
            {
                rb.velocity += Vector3.down * 4;
            }
        }
    }

    public virtual void StopMoving()
    {

    }

    public void CollectBrick(GameObject obj)
    {
        FloorBrick brick = obj.GetComponent<FloorBrick>();
        if (brick != null)
        {
            if (this.colorSet.color == brick.Color.color)
            {
                brick.TurnOffDisplay();
                Brick b = Instantiate(brickPref, pickUp);

                b.ChangeColor(colorSet.color);
                b.transform.position += new Vector3(0, .12f * brickCollected, 0);

                bricks.Push(b);

                brickCollected++;
            }
        }

        Brick br = obj.GetComponent<Brick>();
        if(br != null)
        {
            if(br.color == br.scatterColor.color)
            {
                br.ChangeColor(colorSet.color);

                br.transform.SetParent(pickUp);
                br.transform.SetLocalPositionAndRotation(new Vector3(0, .12f * brickCollected, 0), Quaternion.identity);
                bricks.Push(br);    

                brickCollected++;
            }
        }
    }

    public void BuildBridge(GameObject obj)
    {
        OnBridgeBrick b = obj.GetComponent<OnBridgeBrick>();
        if (b != null)
        {
            bool setted = (b.rend.material.color == colorSet.color);
            bool front = (b.transform.position.z - transform.position.z) > 0;

            if (!setted && front)
            {
                if (brickCollected > 0)
                {
                    b.OnBuild(colorSet.color);
                    Destroy(bricks.Pop().gameObject);
                    brickCollected--;
                }
                else
                {
                    b.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                }
            }
        }
    }

    public void ScatterBrick()
    {
        while (bricks.Count > 0)
        {
            Brick b = bricks.Pop();
            b.transform.localPosition = Vector3.zero;

            b.transform.SetParent(null);
            b.ChangeColor(b.scatterColor.color);
            b.Scatter();

            if (ray.OnFloor1(this).collider)
            {
                Vector3 pos = b.transform.position;
                pos.x = Mathf.Clamp(pos.x, -5.6f, 5.6f);
                pos.z = Mathf.Clamp(pos.z, -7.7f, 4.5f);
                b.transform.position = pos;
            }

            if(ray.OnFloor2(this).collider)
            {
                Vector3 pos = b.transform.position;
                pos.x = Mathf.Clamp(pos.x, -4.6f, 4.6f);
                pos.z = Mathf.Clamp(pos.z, 17.27f, 29.27f);
                b.transform.position = pos;
            }
            if(ray.OnFloor3(this).collider)
            {
                Vector3 pos = b.transform.position;
                pos.x = Mathf.Clamp(pos.x, -4.6f, 4.6f);
                pos.z = Mathf.Clamp(pos.z, 38.5f, 47.7f);
            }
        }
        brickCollected = 0;
    }

    public virtual void KnockBack()
    {

    }

    public virtual void EndKnockBack()
    {

    }

    public virtual void Win()
    {
        StopMoving();
        anim.ResetAnim();
        OnDespawn();
        transform.forward = Vector3.back;
        anim.ChangeAnim("win");
    }

    public virtual void Lose()
    {
        StopMoving();
        anim.ResetAnim();
        OnDespawn();

        transform.forward = Vector3.back;
    }

    #endregion
}
