using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBridgeBrick : MonoBehaviour
{
    public Renderer rend;

    public bool Active
    {
        get { return rend.enabled; }
    }

    public Color Color
    {
        get { return rend.material.color; }
    }

    public void OnBuild(Color color)
    {
        rend.enabled = true;
        rend.material.color = color;
    }
}
