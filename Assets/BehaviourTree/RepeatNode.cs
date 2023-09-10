using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 반복 노드
/// </summary>
public class RepeatNode : DecoratorNode
{
    public int count;
    public int current;

    public RepeatNode(int count, BT_Node child) : base(child)
    {
        this.count = count;
    }

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        child.Update();
        current++;
        if (current == count)
        {
            return NodeState.Success;
        }
        return NodeState.Running;
    }
}
