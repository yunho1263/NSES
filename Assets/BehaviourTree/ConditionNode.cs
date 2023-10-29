using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���¸� üũ�ϴ� ���
/// </summary>
public class ConditionNode : DecoratorNode
{
    public delegate bool Condition();
    public Condition condition;
    public object value;

    public ConditionNode(Condition condition) : base(null)
    {
        this.condition = condition;
    }

    public ConditionNode(Condition condition, BT_Node child) : base(child)
    {
        this.condition = condition;
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

        return condition() ? child.Update() : NodeState.Failure;
    }
}
