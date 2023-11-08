using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mating : ActionNode
{
    public AnimalBehaviour behaviour => findPartnerNode.behaviour;

    public Mating(FindPartner node)
    {
        findPartnerNode = node;
    }

    public AnimalBehaviour partner;

    public FindPartner findPartnerNode;

    public abstract void Breed();
}