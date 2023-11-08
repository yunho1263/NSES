using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public abstract class NavigationNode : ActionNode
{
    public enum SearchResult
    {
        Walking,
        Running,
        Waiting,
        Stop,
        None
    }

    public AnimalBehaviour behaviour;
    public Transform ThisTransform => behaviour.transform;
    public AnimalStat Stat => behaviour.stat;

    protected SearchResult searchResult;

    public AIPath AiPath => behaviour.aiPath;

    public bool IsArrival => !AiPath.pathPending && (AiPath.reachedEndOfPath || !AiPath.hasPath);
    public bool IsOutOfView => Vector2.Distance(ThisTransform.position, AiPath.destination) > Stat.ViewRange;

    public NavigationNode(AnimalBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public abstract SearchResult Search();
}
