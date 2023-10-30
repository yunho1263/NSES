using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resting : ActionNode
{
    AnimalBehaviour behaviour;

    public Resting(AnimalBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    protected override void OnStart()
    {
        if (behaviour.animalStat.stamina <= 0)
        {
            behaviour.animalStat.stamina = 0;
            behaviour.animalStat.SetMoving(false, false);
            behaviour.state = State.Resting;
        }
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (behaviour.state == State.Resting)
        {
            if (behaviour.animalStat.stamina > behaviour.animalStat.MinStaminaToRecall)
            {
                return NodeState.Success;
            }
        
            behaviour.animalStat.SetMoving(false, false);
        
            return NodeState.Running;
        }

        return NodeState.Success;
    }
}
