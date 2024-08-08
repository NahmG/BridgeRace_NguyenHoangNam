using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 rotation;
    [SerializeField] float speed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
