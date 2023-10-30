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
        if (behaviour.LowHealthCondition || behaviour.LowStaminaCondition || behaviour.HungryCondition)
        {
            return SearchResult.None;
        }

        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ThisTransform.position, searchRadius, layerMask);

        behaviour.state = State.Breed;

        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        //����Ʈ���� �ڱ� �ڽ��� �����Ѵ�
        animals.Remove(ThisTransform);

        if (animals.Count == 0)
        {
            return SearchResult.None;
        }

        //�Ÿ������� ����
        animals.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, ThisTransform.position).CompareTo(Vector2.Distance(b.position, ThisTransform.position));
        });

        AiPath.destination = animals[0].position;

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
                Stat.SetMoving(false, false);
                return NodeState.Failure;

            case SearchResult.Walking:
                Stat.SetMoving(true, false);
                return NodeState.Success;

            case SearchResult.Stop:
                AiPath.destination = ThisTransform.position;
                Stat.SetMoving(false, false);
                return NodeState.Success;

            default:
                Stat.SetMoving(false, false);
                return NodeState.Failure;
        }
    }
}
