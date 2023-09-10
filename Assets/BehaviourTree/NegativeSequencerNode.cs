using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ���
/// (���� �� ���� ��� ����)
/// </summary>
public class NegativeSequencerNode : CompositeNode
{
    public NegativeSequencerNode(params BT_Node[] nodes)
    {
        children.AddRange(nodes);
    }

    int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        var child = children[current];

        switch (child.Update())
        {
            case NodeState.Running:
                return NodeState.Running;
            case NodeState.Success:
                return NodeState.Success;
            case NodeState.Failure:
                current++;
                break;
        }

        return current == children.Count ? NodeState.Failure : NodeState.Running;
    }
}
