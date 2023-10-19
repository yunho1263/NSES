using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchNode : ConditionNode
{
    BT_Node branchNode;

    public BranchNode(Condition condition, BT_Node branch1, BT_Node branch2, bool runWhenTrue) : base(condition, branch1, runWhenTrue)
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

        if (runWhenTrue)
        {
            return condition() ? child.Update() : branchNode.Update();
        }
        else
        {
            return condition() ? branchNode.Update() : child.Update();
        }
    }
}
