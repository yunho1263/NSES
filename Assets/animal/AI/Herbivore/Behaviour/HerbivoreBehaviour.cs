using System.Collections;
using System.Collections.Generic;
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

        // ��Ⱑ 50%���ϸ� ���̸� ã�´�
        behaviourSequencer.children.Add(new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(), true));

        if (animalStat.sex == Sex.Male)
        {

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
