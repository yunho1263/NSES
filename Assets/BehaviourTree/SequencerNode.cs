using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 순서 실행 노드
/// (성공 시 다음 노드 실행)
/// </summary>
public class SequencerNode : CompositeNode
{

    public SequencerNode(params BT_Node[] nodes)
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
                current++;
                break;
            case NodeState.Failure:
                return NodeState.Failure;
        }

        return current == children.Count ? NodeState.Success : NodeState.Running;
    }
}
