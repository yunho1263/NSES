using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingFemale : Mating
{
    public MatingFemale(FindPartner node) : base(node)
    {
    }

    public bool Accept(MatingMale male)
    {
        if (male == null)
        {
            return false;
        }

        if (behaviour.stat.canBreed)
        {
            partner = male.behaviour;
        }

        return partner != null;
    }

    public void Childbirth()
    {
        
    }

    public IEnumerator Mate()
    {
        partner = null;
        yield return (partner != null);

        // 
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
