using Drawing;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public AIPath aiPath;

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
        Draw.CircleXY(transform.position, animalStat.ViewRange);
        tree.Update();
        aiPath.maxSpeed = animalStat.Speed;
    }

    public bool HungryCondition => animalStat.satiety <= animalStat.maxSatiety * 0.5f ? true : false;

    public bool LowStaminaCondition => animalStat.stamina <= animalStat.maxStamina * 0.5f ? true : false;

    public bool LowHealthCondition => animalStat.health <= animalStat.maxHealth * 0.5f ? true : false;
}
