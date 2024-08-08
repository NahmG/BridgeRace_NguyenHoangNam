using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastResponse: MonoBehaviour 
{
    [SerializeField] LayerMask bridgeLayer;
    [SerializeField] LayerMask floor1;
    [SerializeField] LayerMask floor2;
    [SerializeField] LayerMask floor3;

    public RaycastHit Launch(Vector3 pos, LayerMask layer, Vector3 direction, float length)
    {
        Physics.Raycast(pos, direction,out RaycastHit hit ,length, layer);
        return hit;
    }

    public bool OnBridge(Character c)
    {
        return Launch(c.transform.position, bridgeLayer, Vector3.down, 1f).collider != null;
    }

    public bool OffBridge(Character c)
    {
        return Launch(c.transform.position, bridgeLayer, Vector3.down, .05f).collider == null;
    }

    public RaycastHit OnFloor1(Character c)
    {
        return Launch(c.transform.position + Vector3.up * .5f, floor1, Vector3.down, 1f);
    }
    public RaycastHit OnFloor2(Character c)
    {
        return Launch(c.transform.position + Vector3.up * .5f, floor2, Vector3.down, 1f);
    }

    public RaycastHit OnFloor3(Character c)
    {
        return Launch(c.transform.position + Vector3.up * .5f, floor3, Vector3.down, 1f);
    }
}
