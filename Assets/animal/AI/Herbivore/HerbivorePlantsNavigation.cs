using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivorePlantsNavigation : NavigationNode
{
    public float searchRadius;

    public HerbivorePlantsNavigation(AnimalBehaviour behaviour, AnimalStat stat, Transform target, Transform p_thisTrans) : base(target, p_thisTrans, stat, behaviour)
    {
        searchRadius = stat.ViewRange;
    }
    public override SerchResult Search()
    {
        List<Transform> plants = new List<Transform>();
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Plants");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(thisTrans.position, searchRadius, layerMask);

        if (colliders.Length == 0)
        {
            return SerchResult.None;
        }

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
