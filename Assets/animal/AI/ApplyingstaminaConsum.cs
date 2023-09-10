using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyingstaminaConsum : ActionNode
{
    public ApplyingstaminaConsum(AnimalStat stat)
    {
        this.stat = stat;
    }

    public AnimalStat stat;

    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        if (stat.stamina <= 0)
        {
            stat.health -= stat.health * 0.01f * Time.deltaTime;
            return NodeState.Success;
        }

        if (stat.isMoving)
        {
            float staminaConsumption = stat.staminaConsumption;
            if (stat.isRunning)
            {
                staminaConsumption *= 2;
            }
            stat.stamina -= staminaConsumption * Time.deltaTime;
        }
        return NodeState.Success;
    }
}
