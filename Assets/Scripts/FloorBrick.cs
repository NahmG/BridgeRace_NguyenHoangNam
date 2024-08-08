using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorBrick : GameUnit
{
    [SerializeField] float frameRate = 3f;
    float time = 0f;

    [SerializeField] Renderer rend;
    [SerializeField] BoxCollider col;

    public bool Active { get { return rend.enabled; } }

    private void Start()
    {
        rend.material.color = Color.color;
    }

    private void Update()
    {
        if (!rend.enabled)
        {
            time += Time.deltaTime;
            if (time > frameRate)
            {
                time -= frameRate;
                rend.enabled = true;
                col.enabled = true;
            }
        }
    }

    public void TurnOffDisplay()
    {
        rend.enabled = false;
        col.enabled = false;
    }
}

