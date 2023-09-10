using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorNode : BT_Node
{
    public DecoratorNode(BT_Node child)
    {
        this.child = child;
    }

    public BT_Node child = null;
}
