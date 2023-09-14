using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPartner : NavigationNode
{
    public float searchRadius;
    public LayerMask layerMask;

    public FindPartner(Transform target, float searchRadius, AnimalType animalType) : base(target)
    {
        this.searchRadius = searchRadius;

        switch (animalType)
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

    public override bool Serch()
    {
        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, searchRadius, layerMask);

        if (colliders.Length == 0)
        {
            return false;
        }

        //거리순으로 정렬
        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        animals.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, position).CompareTo(Vector2.Distance(b.position, position));
        });

        target.position = animals[0].position;
        position = target.position;

        return true;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (Serch())
        {
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
    }
}
