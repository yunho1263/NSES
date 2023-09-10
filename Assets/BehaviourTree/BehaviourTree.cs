using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree
{
    public BT_Node rootNode;
    public BT_Node.NodeState treeState = BT_Node.NodeState.Running;

    public BT_Node.NodeState Update()
    {
        if(rootNode.nodeState == BT_Node.NodeState.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }
}
