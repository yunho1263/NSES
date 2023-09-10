using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public override void SetupTree()
    {
        // 스텟 연산 시퀀스
        // 순서대로 실행 (노드 실행에 성공 할 때 다음으로 넘어가고 실패하면 종료)
        SequencerNode statSequencer = new SequencerNode();

        // 스테미나 소모
        statSequencer.children.Add(new ApplyingstaminaConsum(animalStat));


        // 행동 시퀀스
        // 순서대로 실행 (노드 실행에 실패 할 때 다음으로 넘어가고 성공하면 종료)
        NegativeSequencerNode behaviourSequencer = new NegativeSequencerNode();

        // 허기가 50%이하면 먹이를 찾는다
        behaviourSequencer.children.Add(new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(), true));

        if (animalStat.sex == Sex.Male)
        {

        }


        tree = new BehaviourTree()
        {
            // 무한 반복
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
