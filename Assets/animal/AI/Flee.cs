using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Flee : NavigationNode
{   
    public float searchRadius;
    public LayerMask layerMask;

    public Flee(AnimalBehaviour behaviour) : base(behaviour)
    {
        this.searchRadius = behaviour.stat.ViewRange;

        layerMask = 0;

        switch (behaviour.stat.animalType)
        {
            case AnimalType.Herbivore:
                layerMask = 1 << LayerMask.NameToLayer("Omnivore") | 1 << LayerMask.NameToLayer("Carnivore");
                break;
            case AnimalType.Omnivore:
                layerMask = 1 << LayerMask.NameToLayer("Carnivore");
                break;
            default:
                break;
        }
    }

    public override SearchResult Search()
    {
        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ThisTransform.position, searchRadius, layerMask);

        if (colliders == null || colliders.Length == 0)
        {
            return SearchResult.None;
        }

        behaviour.state = State.Avoid;

        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        if (animals.Count == 1)
        {
            // 반대방향으로 이동한다
            Vector2 direction = (Vector2)ThisTransform.position - (Vector2)animals[0].position;
            AiPath.destination = (Vector2)ThisTransform.position + direction;
        }
        else
        {
            Vector2 fleePosition = Vector2.zero;

            // 포식자들의 가중치를 계산하여 포식자들이 적은 방향으로 이동한다
            for (int i = 0; i < animals.Count; i++)
            {
                Vector2 direction = (Vector2)ThisTransform.position - (Vector2)animals[i].position;
                float distance = Vector2.Distance(ThisTransform.position, animals[i].position);

                // 포식자와의 거리가 멀수록 가중치가 작아진다
                float weight = 1 / distance * 7;

                // 포식자의 방향으로 가중치를 더한다
                fleePosition += direction * weight;
            }

            AiPath.destination = fleePosition;
        }
        return SearchResult.Running;
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
            case SearchResult.Running:
                Stat.SetMoving(true, true);
                return NodeState.Success;

            case SearchResult.None:
                Stat.SetMoving(false, false);
                return NodeState.Failure;
            default:
                Stat.SetMoving(false, false);
                return NodeState.Failure;
        }
    }
}
