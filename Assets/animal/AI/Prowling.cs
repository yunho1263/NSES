using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prowling : NavigationNode
{
    public Prowling(AnimalBehaviour behaviour) : base(behaviour)
    {
    }

    public override SearchResult Search()
    {
        Vector2 randomPosition = Random.insideUnitCircle * behaviour.animalStat.ViewRange;
        target.position = (Vector2)thisTrans.position + randomPosition;

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
        stat.SetMoving(true);
        stat.SetRunning(false);

        if (stat.isResting)
        {
            target.position = thisTrans.position;
            return NodeState.Failure;
        }

        if (IsArrival)
        {
            Search();
        }

        behaviour.state = State.Idle;

        return NodeState.Running;
    }
}
