using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingFemale : Mating
{
    public MatingFemale(FindPartner node) : base(node)
    {
    }

    public override void Breed()
    {
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        return NodeState.Success;
    }
}
