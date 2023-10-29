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

    public Vector2 Position => target.position;
    public Transform target;

    public Transform thisTrans;

    public AnimalStat stat;
    public AnimalBehaviour behaviour;

    protected SearchResult searchResult;

    public bool IsArrival => behaviour.aiPath.isStopped;

    public NavigationNode(AnimalBehaviour behaviour)
    {
        this.target = behaviour.target;
        this.thisTrans = behaviour.transform;
        this.stat = behaviour.animalStat;
        this.behaviour = behaviour;
    }

    public abstract SearchResult Search();
}
