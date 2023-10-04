using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public override void SetupTree()
    {
        // ���� ���� ������
        // ������� ���� (��� ���࿡ ���� �� �� �������� �Ѿ�� �����ϸ� ����)
        SequencerNode statSequencer = new SequencerNode();

        // ���׹̳� �Ҹ�
        statSequencer.children.Add(new ApplyingstaminaConsum(animalStat));


        // �ൿ ������
        // ������� ���� (��� ���࿡ ���� �� �� �������� �Ѿ�� �����ϸ� ����)
        NegativeSequencerNode behaviourSequencer = new NegativeSequencerNode();

        // ��ó�� ������ ������ �ִٸ� ��������
        behaviourSequencer.children.Add(new Flee(animalStat, target, 8f, animalStat.animalType, transform));

        // ��Ⱑ 50%���ϸ� ���̸� ã�´�
        behaviourSequencer.children.Add(new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(target, 8f, transform), true));

        // ������ �ִٸ� ¦¢�⸦ �� ��Ʈ�ʸ� ã�´�
        if (animalStat.sex == Sex.Male)
        {
            behaviourSequencer.children.Add(new ConditionNode(() => BreedCondition(), new FindPartner(target, 8f, animalStat.animalType, transform), true));
        }


        tree = new BehaviourTree()
        {
            // ���� �ݺ�
            rootNode = new RepeatNode(0,

                new SequencerNode
                (
                    statSequencer,

                    behaviourSequencer
                )
            )
        };
    }
}
