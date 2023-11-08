using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prowling : NavigationNode
{
    Vector3 savedPosition;

    public Prowling(AnimalBehaviour behaviour) : base(behaviour)
    {
    }

    public override SearchResult Search()
    {
        Vector3 randomPosition = Random.insideUnitCircle * behaviour.stat.ViewRange;
        randomPosition.z = 0;
        randomPosition += ThisTransform.position;
        AiPath.destination = randomPosition;
        savedPosition = randomPosition;

        return SearchResult.Walking;
    }

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        Stat.SetMoving(true, false);
        behaviour.state = State.Idle;

        if (IsArrival || AiPath.destination != savedPosition)
        {
            Search();
            return NodeState.Success;
        }

        AiPath.destination = savedPosition;

        return NodeState.Success;
    }
}
