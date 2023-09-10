using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;

    public Animal animal;
    public AnimalStat animalStat;
    public AnimalBreed animalBreed;

    public abstract void SetupTree();

    private void Update()
    {
        tree.Update();
    }

    /// <summary>
    /// 배고픔 컨디션 체크
    /// </summary>
    /// <returns>배고픔이 50%이하라면 true를 반환합니다.</returns>
    public bool HungerCondition()
    {
        return animalStat.hunger * 0.5f <= animalStat.maxHunger;
    }
}
