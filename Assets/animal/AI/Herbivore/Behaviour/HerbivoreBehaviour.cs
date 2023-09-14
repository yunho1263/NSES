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

        // 근처에 위험한 동물이 있다면 도망간다
        behaviourSequencer.children.Add(new Flee(animalStat, target, 8f, animalStat.animalType, transform));

        // 허기가 50%이하면 먹이를 찾는다
        behaviourSequencer.children.Add(new ConditionNode(() => HungerCondition(), new HerbivorePlantsNavigation(target, 8f), true));

        // 여유가 있다면 짝짖기를 할 파트너를 찾는다
        if (animalStat.sex == Sex.Male)
        {
            behaviourSequencer.children.Add(new ConditionNode(() => BreedCondition(), new FindPartner(target, 8f, animalStat.animalType), true));
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
