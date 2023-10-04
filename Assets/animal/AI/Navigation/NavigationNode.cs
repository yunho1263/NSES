using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NavigationNode : ActionNode
{
    public Vector2 position;
    public Transform target;

    public Transform thisTrans;

    public NavigationNode(Transform target, Transform p_thisTrans)
    {
        this.target = target;
        this.thisTrans = p_thisTrans;
    }

    public abstract bool Serch();
}
