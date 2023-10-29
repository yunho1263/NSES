using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivorePlantsNavigation : NavigationNode
{
    public float searchRadius;

    public HerbivorePlantsNavigation(AnimalBehaviour behaviour) : base(behaviour)
    {
        searchRadius = behaviour.animalStat.ViewRange;
    }
    public override SearchResult Search()
    {
        if (behaviour.HungryCondition == false)
        {
            target.position = thisTrans.position;
            return SearchResult.None;
        }

        List<Transform> plants = new List<Transform>();
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Plants");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(thisTrans.position, searchRadius, layerMask);

        if (colliders.Length == 0)
        {
            target.position = thisTrans.position;
            return SearchResult.None;
        }

        behaviour.state = State.Seek;

        //거리순으로 정렬
        for (int i = 0; i < colliders.Length; i++)
        {
            plants.Add(colliders[i].transform);
        }

        plants.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, thisTrans.position).CompareTo(Vector2.Distance(b.position, thisTrans.position));
        });

        target.position = plants[0].position;

        if (Vector3.Distance(thisTrans.position, Position) <= 0.1f)
        {
            target.position = thisTrans.position;
            return SearchResult.Stop;
        }

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
        searchResult = Search();

        switch (searchResult)
        {
            case SearchResult.None:
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Failure;

            case SearchResult.Walking:
                stat.SetMoving(true);
                stat.SetRunning(false);
                return NodeState.Running;

            case SearchResult.Stop:
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Success;

            default:
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Failure;
        }
    }
}
