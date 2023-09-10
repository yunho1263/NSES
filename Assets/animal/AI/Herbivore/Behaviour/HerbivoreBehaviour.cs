using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivoreBehaviour : BehaviourTreeRunner
{
    public override void SetupTree()
    {
        tree = new BehaviourTree()
        {
            // ���� �ݺ�
            rootNode = new RepeatNode(0, 
            
                // ������� ���� (��� ���࿡ ���� �� �� �������� �Ѿ�� �����ϸ� ����)
                new SequencerNode
                (
                    new ApplyingstaminaConsum(animalStat),

                    // ������� ���� (��� ���࿡ ���� �� �� �������� �Ѿ�� �����ϸ� ����)
                    new NegativeSequencerNode
                    (
                        // ������� 50% ���϶�� ���̸� ã�� �ƴϸ� �������� �Ѿ
                        new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(), true)
                    )
                )
            )
        };
    }
}
