using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NavigationNode
{
    public AnimalStat stat;
    public Transform thisTrans;
    public float searchRadius;
    public LayerMask layerMask;

    public Flee(AnimalStat stat, Transform target, float searchRadius, AnimalType animalType, Transform thisTrans) : base(target)
    {
        this.stat = stat;
        this.searchRadius = searchRadius;

        layerMask = 0;

        switch (animalType)
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

        this.thisTrans = thisTrans;
    }

    public override bool Serch()
    {
        List<Transform> animals = new List<Transform>();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, searchRadius, layerMask);

        if (colliders.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        // 모든 포식자들을 피할 수 있는 위치를 찾는다
        Vector2 fleePosition = Vector2.zero;

        for (int i = 0; i < animals.Count; i++)
        {
            Vector2 dir = (Vector2)thisTrans.position - (Vector2)animals[i].position;
            dir.Normalize();

            fleePosition += (Vector2)thisTrans.position + dir * 2f;
        }

        fleePosition /= animals.Count;
        target.position = fleePosition;

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
