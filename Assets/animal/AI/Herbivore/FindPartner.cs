using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPartner : NavigationNode
{
    public float searchRadius;
    public LayerMask layerMask;

    public FindPartner(AnimalBehaviour behaviour, AnimalStat stat, Transform target, AnimalType animalType, Transform p_thisTrans) : base(target, p_thisTrans, stat, behaviour)
    {
        searchRadius = stat.ViewRange;

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

    public override SerchResult Search()
    {
        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(thisTrans.position, searchRadius, layerMask);

        if (colliders.Length == 0)
        {
            return SerchResult.None;
        }

        //거리순으로 정렬
        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        animals.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, thisTrans.position).CompareTo(Vector2.Distance(b.position, thisTrans.position));
        });

        target.position = animals[0].position;
        position = target.position;

        return SerchResult.Walking;
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }
}
