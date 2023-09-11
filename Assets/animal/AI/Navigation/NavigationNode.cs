using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationNode : ActionNode
{
    public Vector2 position;
    public Transform target;

    public NavigationNode(Transform target)
    {
        this.target = target;
    }

    public abstract bool Serch();
}
