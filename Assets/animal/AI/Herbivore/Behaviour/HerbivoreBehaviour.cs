using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public override void SetupTree()
    {
        SequencerNode rootBehaviour = new SequencerNode
        (
            new Resting(this),

            new SelectorNode
            (
                new Flee(this),

                new SelectorNode
                (
                    new HerbivorePlantsNavigation(this),

                    new SelectorNode
                    (
                        new FindPartner(this),
                        new Prowling(this)
                    )
                )
            )
        );

        tree = new BehaviourTree()
        {
            // 무한 반복
            rootNode = new RepeatNode(0, rootBehaviour)
        };
    }
}
