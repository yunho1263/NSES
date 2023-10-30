using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNoting : NavigationNode
{
    public DoNoting(AnimalBehaviour behaviour) : base(behaviour)
    {
    }

    public override SearchResult Search()
    {
        return SearchResult.Stop;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        AiPath.destination = ThisTransform.position;

        Stat.SetMoving(false, false);

        return NodeState.Success;
    }
}
