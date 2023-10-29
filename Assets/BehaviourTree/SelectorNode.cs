using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BT_Node
{
    BT_Node doNode;
    BT_Node elseNode;

    public SelectorNode(BT_Node doNode, BT_Node elseNode)
    {
        this.doNode = doNode;
        this.elseNode = elseNode;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        switch (doNode.Update())
        {
            case NodeState.Running:
                return NodeState.Running;
            case NodeState.Success:
                return NodeState.Success;
            case NodeState.Failure:
                return elseNode.Update();
        }

        return NodeState.Failure;
    }
}
