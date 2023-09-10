using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : BT_Node
{
    public List<BT_Node> children = new List<BT_Node>();
}
