using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator anim;

    public bool Opened { get { return anim.enabled; } }

    public void OpenDoor()
    {
        anim.enabled = true;
    }

}