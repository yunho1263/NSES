using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum State
{
    Idle,
    Navigate,
    Eat,
    Drink,
    Sleep,
    Breed,
    Seek,
    Avoid,
}

[Serializable]
public abstract class AnimalBehaviour : MonoBehaviour
{
    public BehaviourTree tree;

    public State state;
    public AnimalStat animalStat;
    public AnimalBreed animalBreed;

    public Transform target;

    private void Start()
    {
        SetupTree();
    }

    public void Initialize()
    {

    }

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

    public bool StaminaCondition()
    {
        return animalStat.stamina * 0.5f <= animalStat.maxStamina;
    }

    public bool HealthCondition()
    {
        return animalStat.health * 0.5f <= animalStat.maxHealth;
    }

    public bool BreedCondition()
    {
        if (HealthCondition() && StaminaCondition() && HungerCondition() && animalBreed.CanBreeding())
        {
            return true;
        }

        return false;
    }
}
