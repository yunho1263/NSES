using Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static NavigationNode;

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

    public List<Transform> naturalEnemys;

    private void Start()
    {
        animalStat.initialize();
        SetupTree();
    }

    public void Initialize()
    {

    }

    public abstract void SetupTree();

    private void Update()
    {
        animalStat.Metabolic();
        animalStat.StaminaConsum();
        tree.Update();
    }

    /// <summary>
    /// 배고픔 컨디션 체크
    /// </summary>
    /// <returns>배고픔이 50%이하라면 true를 반환합니다.</returns>
    public bool HungerCondition()
    {
        return animalStat.satiety <= animalStat.maxSatiety * 0.5f ? true : false;
    }

    public bool StaminaCondition()
    {
        return animalStat.stamina <= animalStat.maxStamina * 0.5f ? true : false;
    }

    public bool HealthCondition()
    {
        return animalStat.health <= animalStat.maxHealth * 0.5f ? true : false;
    }

    public bool DetectedNaturalEnemy()
    {
        naturalEnemys.Clear();

        Draw.CircleXY(transform.position, animalStat.ViewRange);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, animalStat.ViewRange, animalStat.NaturalEnemyLayerMask);

        if (colliders == null || colliders.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            naturalEnemys.Add(colliders[i].transform);
        }

        return true;
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
