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
        throw new System.NotImplementedException();
    }

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
        throw new System.NotImplementedException();
    }

    protected override NodeState OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
