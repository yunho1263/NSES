using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPartner : NavigationNode
{
    public float searchRadius;
    public LayerMask layerMask;

    public Mating mating;
    public Mating partnerMating;

    public AnimalStat stat => behaviour.stat;

    public FindPartner(AnimalBehaviour behaviour) : base(behaviour)
    {
        searchRadius = stat.ViewRange;

        switch (stat.animalType)
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

        if (stat.sex == Sex.Male)
        {
            mating = new MatingMale(this);
        }
        else
        {
            mating = new MatingFemale(this);
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

        //리스트에서 자기 자신을 제거한다
        animals.Remove(ThisTransform);

        if (animals.Count == 0)
        {
            return SearchResult.None;
        }

        List<Transform> removeAnimals = new List<Transform>();

        foreach (Transform transform in animals)
        {
            AnimalStat animalStat;
            transform.TryGetComponent(out animalStat);

            if (animalStat)
            {
                if (stat.sex == stat.sex)
                {
                    removeAnimals.Add(transform);
                }
                else if (!animalStat.canBreed)
                {
                    removeAnimals.Add(transform);
                }
            }
        }

        foreach (Transform transform in removeAnimals)
        {
            animals.Remove(transform);
        }

        //거리순으로 정렬
        animals.Sort(delegate (Transform a, Transform b)
        {
            return Vector2.Distance(a.position, ThisTransform.position).CompareTo(Vector2.Distance(b.position, ThisTransform.position));
        });

        AiPath.destination = animals[0].position;
        animals[0].TryGetComponent(out AnimalBehaviour aniBe);
        partnerMating = aniBe.mating;

        if (IsArrival)
        {
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
                Stat.SetMoving(false, false);
                return NodeState.Failure;

            case SearchResult.Walking:
                Stat.SetMoving(true, false);
                return NodeState.Success;

            case SearchResult.Stop:
                AiPath.destination = ThisTransform.position;
                Stat.SetMoving(false, false);
                return mating.Update();

            default:
                Stat.SetMoving(false, false);
                return NodeState.Failure;
        }
    }
}
