using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingFemale : Mating
{
    public MatingFemale(FindPartner node) : base(node)
    {
    }


    public Queue<MatingMale> maleList = new();

    public bool Accept(MatingMale male)
    {
        if (male == null)
        {
            return false;
        }

        if (behaviour.stat.canBreed)
        {
            behaviour.partner = male.behaviour;
        }

        return behaviour.partner != null;
    }

    public void Childbirth()
    {
        
    }

    public override void Breed()
    {
        startTime = Time.time;
        DNA dna = new DNA(behaviour.stat.dna, behaviour.partner.stat.dna);
        AnimalBehaviour chB;
        GameObject child = Object.Instantiate(behaviour.stat.babyPrefab, behaviour.transform.position, Quaternion.identity);
        child.TryGetComponent(out chB);
        chB.stat.dna = dna; 
        chB.Initialize();
        behaviour.stat.canBreed = false;
    }

    protected override void OnStart()
    {
        behaviour.partner = null;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (!behaviour.stat.canBreed)
        {
            if (Time.time - startTime > behaviour.stat.breedcooldown)
            {
                behaviour.stat.canBreed = true;
            }
            else
            {
                return NodeState.Running;
            }
        }

        if (behaviour.partner == null && maleList.Count > 0)
        {
            AnimalBehaviour b;
            while (true)
            {
                b = maleList.Dequeue().behaviour;
                if (b.partner == null && b.stat.canBreed)
                {
                    break;
                }
            }

            if (maleList.Count == 0)
            {
                return NodeState.Success;
            }

            behaviour.partner = b;
            behaviour.partner.partner = behaviour;
            Breed();
            return NodeState.Success;
        }

        maleList.Clear();

        return NodeState.Success;
    }
}
