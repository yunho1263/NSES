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
        searchResult = Search();

        target.position = thisTrans.position;

        stat.SetMoving(false);
        stat.SetRunning(false);

        return NodeState.Success;
    }
}
