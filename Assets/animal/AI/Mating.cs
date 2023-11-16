using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mating : ActionNode
{
    public float startTime;

    public AnimalBehaviour behaviour => findPartnerNode.behaviour;

    public Mating(FindPartner node)
    {
        findPartnerNode = node;
        behaviour.mating = this;
    }

    public FindPartner findPartnerNode;

    public abstract void Breed();
}
