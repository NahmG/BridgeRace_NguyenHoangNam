using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    public void OnEnter(Bot bot);
    public void OnExecute(Bot bot);
    public void OnExit(Bot bot);
}
