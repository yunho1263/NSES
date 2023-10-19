using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class NavigationNode : ActionNode
{
    public enum SerchResult
    {
        Walking,
        Running,
        Stop,
        None
    }

    public Vector2 position;
    public Transform target;

    public Transform thisTrans;

    public AnimalStat stat;
    public AnimalBehaviour behaviour;

    public NavigationNode(Transform target, Transform p_thisTrans, AnimalStat stat, AnimalBehaviour behaviour)
    {
        this.target = target;
        this.thisTrans = p_thisTrans;
        this.stat = stat;
        this.behaviour = behaviour;
    }

    public abstract SerchResult Search();

    protected override NodeState OnUpdate()
    {
        switch (Search())
        {
            case SerchResult.Running:
                stat.SetMoving(true);
                stat.SetRunning(true);
                return NodeState.Running;
            case SerchResult.Walking:
                stat.SetMoving(true);
                stat.SetRunning(false);
                return NodeState.Running;

            case SerchResult.Stop:
                target.position = thisTrans.position;
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Success;
            case SerchResult.None:
                target.position = thisTrans.position;
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Success;
        }

        return NodeState.Failure;
    }
}
