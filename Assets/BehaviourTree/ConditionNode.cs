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

    // condition�� true�� �� �ڽ� ��带 �������� false�� �� ��������
    public bool runWhenTrue;

    public ConditionNode(Condition condition) : base(null)
    {
        this.condition = condition;
        runWhenTrue = true;
    }

    public ConditionNode(Condition condition, BT_Node child, bool runWhenTrue) : base(child)
    {
        this.condition = condition;
        this.runWhenTrue = runWhenTrue;
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
            return condition() ? child.Update() : NodeState.Success;
        }
        else
        {
            return condition() ? NodeState.Success : child.Update();
        }
    }
}
