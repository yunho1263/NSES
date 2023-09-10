using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 지연 노드
/// </summary>
public class WaitNode : ActionNode
{
    public float waitTime = 0;
    public float startTime;

    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (Time.time - startTime > waitTime)
        {
            return NodeState.Success;
        }
        return NodeState.Running;
    }
}
