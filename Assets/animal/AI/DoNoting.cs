using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNoting : NavigationNode
{
    public DoNoting(Transform target, Transform p_thisTrans, AnimalStat stat, AnimalBehaviour behaviour) : base(target, p_thisTrans, stat, behaviour)
    {
    }

    public override SerchResult Search()
    {
        return SerchResult.Stop;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }
}
