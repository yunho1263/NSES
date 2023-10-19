using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public override void SetupTree()
    {
        BranchNode rootBehaviour = new BranchNode
        (
            // 근처에 위험한 동물이 있다면 도망간다
            () => DetectedNaturalEnemy(), new Flee(this, animalStat, target , animalStat.animalType, transform),

            new BranchNode
            (
                // 허기가 50%이하면 먹이를 찾는다
                () => HungerCondition(), new HerbivorePlantsNavigation(this, animalStat, target, transform),

                new BranchNode
                (
                    // 여유가 있다면 짝짖기를 할 파트너를 찾는다
                    () => BreedCondition(), new FindPartner(this, animalStat, target, animalStat.animalType, transform),

                    new DoNoting(target, transform, animalStat, this), true
                ), true
            ), true
        ) ;

        tree = new BehaviourTree()
        {
            // 무한 반복
            rootNode = new RepeatNode(0, rootBehaviour)
        };
    }
}
