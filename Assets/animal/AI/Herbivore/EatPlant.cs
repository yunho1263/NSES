using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPlant : ActionNode
{
    public AnimalBehaviour behaviour;
    Plant crrEatingPlant;

    float time;

    public EatPlant(AnimalBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    protected override void OnStart()
    {
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Plants");
        Ray ray = new Ray(behaviour.transform.position, behaviour.transform.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.1f, layerMask);

        if (hit.collider != null)
        {
            hit.collider.TryGetComponent(out crrEatingPlant);

            time = Time.time;
        }
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (crrEatingPlant == null)
        {
            return NodeState.Failure;
        }

        if (Time.time - time > 1f)
        {
            behaviour.stat.satiety += crrEatingPlant.FullnessPerServing;
            if (behaviour.stat.satiety > behaviour.stat.maxSatiety)
            {
                behaviour.stat.satiety = behaviour.stat.maxSatiety;
            }

            behaviour.stat.stamina += crrEatingPlant.FullnessPerServing;
            if (behaviour.stat.stamina > behaviour.stat.maxStamina)
            {
                behaviour.stat.stamina = behaviour.stat.maxStamina;
            }

            crrEatingPlant.remaining--;

            if (crrEatingPlant.remaining <= 0)
            {
                GameObject.Destroy(crrEatingPlant.gameObject);
            }

            return NodeState.Success;
        }
        else
        {
            return NodeState.Running;
        }
    }
}
