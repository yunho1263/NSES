using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPartner : NavigationNode
{
    public float searchRadius;
    public LayerMask layerMask;

    public FindPartner(AnimalBehaviour behaviour) : base(behaviour)
    {
        searchRadius = behaviour.animalStat.ViewRange;

        switch (behaviour.animalStat.animalType)
        {
            case AnimalType.Herbivore:
                layerMask = 1 << LayerMask.NameToLayer("Herbivore");
                break;
            case AnimalType.Carnivore:
                layerMask = 1 << LayerMask.NameToLayer("Carnivore");
                break;
            case AnimalType.Omnivore:
                layerMask = 1 << LayerMask.NameToLayer("Omnivore");
                break;
            default:
                break;
        }
    }

    public override SearchResult Search()
    {
        if (behaviour.LowHealthCondition && behaviour.LowStaminaCondition && behaviour.HungryCondition)
        {
            return SearchResult.None;
        }

        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(thisTrans.position, searchRadius, layerMask);

        behaviour.state = State.Breed;

        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        //리스트에서 자기 자신을 제거한다
        animals.Remove(thisTrans);

        if (animals.Count == 0)
        {
            return SearchResult.None;
        }

        //거리순으로 정렬
        animals.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, thisTrans.position).CompareTo(Vector2.Distance(b.position, thisTrans.position));
        });

        target.position = animals[0].position;

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
                stat.SetRunning(true);
                return NodeState.Running;
            case SearchResult.Stop:
                stat.SetMoving(false);
                stat.SetRunning(false);
                return NodeState.Running;
            default:
                return NodeState.Failure;
        }
    }
}
