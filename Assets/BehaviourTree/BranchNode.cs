using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchNode : ConditionNode
{
    BT_Node branchNode;

    public BranchNode(Condition condition, BT_Node branch1, BT_Node branch2) : base(condition, branch1)
    {
        branchNode = branch2;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {

        if (child == null)
        {
            return condition() ? NodeState.Success : NodeState.Failure;
        }

        return condition() ? child.Update() : branchNode.Update();
    }
}
