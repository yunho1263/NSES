using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing;

public class Flee : NavigationNode
{
    public AnimalStat stat;
    
    public float searchRadius;
    public LayerMask layerMask;

    public Flee(AnimalStat stat, Transform target, float searchRadius, AnimalType animalType, Transform p_thisTrans) : base(target, p_thisTrans)
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
    }

    public override bool Serch()
    {
        List<Transform> animals = new List<Transform>();

        Draw.CircleXY(thisTrans.position, searchRadius);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(thisTrans.position, searchRadius, layerMask);

        if (colliders == null || colliders.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            animals.Add(colliders[i].transform);
        }

        if (animals.Count == 1)
        {
            // �ݴ�������� �̵��Ѵ�
            Vector2 direction = (Vector2)thisTrans.position - (Vector2)animals[0].position;
            target.position = (Vector2)thisTrans.position + direction;
        }
        else
        {
            Vector2 fleePosition = Vector2.zero;

            // �����ڵ��� ����ġ�� ����Ͽ� �����ڵ��� ���� �������� �̵��Ѵ�
            for (int i = 0; i < animals.Count; i++)
            {
                Vector2 direction = (Vector2)thisTrans.position - (Vector2)animals[i].position;
                float distance = Vector2.Distance(thisTrans.position, animals[i].position);

                // �����ڿ��� �Ÿ��� �ּ��� ����ġ�� �۾�����
                float weight = 1 / distance * 7;

                // �������� �������� ����ġ�� ���Ѵ�
                fleePosition += direction * weight;
            }

            target.position = fleePosition;
        }
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
