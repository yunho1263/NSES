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
    /// ����� ����� üũ
    /// </summary>
    /// <returns>������� 50%���϶�� true�� ��ȯ�մϴ�.</returns>
    public bool HungerCondition()
    {
        return animalStat.hunger * 0.5f <= animalStat.maxHunger;
    }
}
