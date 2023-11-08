using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivorePlantsNavigation : NavigationNode
{
    public float searchRadius;

    EatPlant eatPlant;

    public HerbivorePlantsNavigation(AnimalBehaviour behaviour) : base(behaviour)
    {
        searchRadius = behaviour.stat.ViewRange;

        eatPlant = new EatPlant(behaviour);
    }
    public override SearchResult Search()
    {
        if (behaviour.HungryCondition || behaviour.LowStaminaCondition)
        {
            List<Transform> plants = new List<Transform>();
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Plants");

            Collider2D[] colliders = Physics2D.OverlapCircleAll(ThisTransform.position, searchRadius, layerMask);

            if (colliders.Length == 0)
            {
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
                return Vector2.Distance(a.position, ThisTransform.position).CompareTo(Vector2.Distance(b.position, ThisTransform.position));
            });

            AiPath.destination = plants[0].position;

            if (IsArrival)
            {
                return SearchResult.Stop;
            }

            return SearchResult.Walking;

            
        }

        return SearchResult.None;
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
                Stat.SetMoving(false, false);
                return NodeState.Failure;

            case SearchResult.Walking:
                Stat.SetMoving(true, false);
                return NodeState.Success;

            case SearchResult.Stop:
                AiPath.destination = ThisTransform.position;
                Stat.SetMoving(false, false);
                return eatPlant.Update();

            default:
                Stat.SetMoving(false, false);
                return NodeState.Failure;
        }
    }
}
