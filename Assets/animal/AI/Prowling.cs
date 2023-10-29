using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prowling : NavigationNode
{
    float interval;
    float startTime;

    public Prowling(AnimalBehaviour behaviour, float interval) : base(behaviour)
    {
        this.interval = interval;
    }

    public override SearchResult Search()
    {
        Vector2 randomPosition = Random.insideUnitCircle * behaviour.animalStat.ViewRange;
        target.position = (Vector2)thisTrans.position + randomPosition;

        return SearchResult.Walking;
    }

    protected override void OnStart()
    {
        startTime = Time.time;
        Search();
    }

    protected override void OnStop()
    {
        startTime = 0;
    }

    protected override NodeState OnUpdate()
    {
        behaviour.state = State.Idle;

        if (Time.time - startTime > interval)
        {
            return NodeState.Success;
        }

        return NodeState.Running;
    }
}
