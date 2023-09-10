using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbivoreBehaviour : BehaviourTreeRunner
{
    public override void SetupTree()
    {
        tree = new BehaviourTree()
        {
            // 무한 반복
            rootNode = new RepeatNode(0, 
            
                // 순서대로 실행 (노드 실행에 성공 할 때 다음으로 넘어가고 실패하면 종료)
                new SequencerNode
                (
                    new ApplyingstaminaConsum(animalStat),

                    // 순서대로 실행 (노드 실행에 실패 할 때 다음으로 넘어가고 성공하면 종료)
                    new NegativeSequencerNode
                    (
                        // 배고픔이 50% 이하라면 먹이를 찾고 아니면 다음으로 넘어감
                        new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(), true)
                    )
                )
            )
        };
    }
}
