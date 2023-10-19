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
            // ��ó�� ������ ������ �ִٸ� ��������
            () => DetectedNaturalEnemy(), new Flee(this, animalStat, target , animalStat.animalType, transform),

            new BranchNode
            (
                // ��Ⱑ 50%���ϸ� ���̸� ã�´�
                () => HungerCondition(), new HerbivorePlantsNavigation(this, animalStat, target, transform),

                new BranchNode
                (
                    // ������ �ִٸ� ¦¢�⸦ �� ��Ʈ�ʸ� ã�´�
                    () => BreedCondition(), new FindPartner(this, animalStat, target, animalStat.animalType, transform),

                    new DoNoting(target, transform, animalStat, this), true
                ), true
            ), true
        ) ;

        tree = new BehaviourTree()
        {
            // ���� �ݺ�
            rootNode = new RepeatNode(0, rootBehaviour)
        };
    }
}
