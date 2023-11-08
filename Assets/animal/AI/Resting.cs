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
        if (behaviour.stat.stamina <= 0)
        {
            behaviour.stat.stamina = 0;
            behaviour.stat.SetMoving(false, false);
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
            if (behaviour.stat.stamina > behaviour.stat.MinStaminaToRecall)
            {
                return NodeState.Success;
            }
        
            behaviour.stat.SetMoving(false, false);
        
            return NodeState.Failure;
        }

        return NodeState.Success;
    }
}
