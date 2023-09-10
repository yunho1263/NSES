using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BT_Node
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public NodeState nodeState = NodeState.Running;
    public bool started = false;

    public NodeState Update()
    {
        if(!started)
        {
            OnStart();
            started = true;
        }

        nodeState = OnUpdate();

        if(nodeState == NodeState.Failure || nodeState == NodeState.Success)
        {
            OnStop();
            started = false;
        }

        return nodeState;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeState OnUpdate();
}
