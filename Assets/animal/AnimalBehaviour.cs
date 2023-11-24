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
    Resting,
}

[Serializable]
public abstract class AnimalBehaviour : MonoBehaviour
{
    public AIPath aiPath;

    public BehaviourTree tree;

    public State state;
    public AnimalStat stat;

    public Transform target;

    public List<Transform> naturalEnemys;

    public Mating mating;
    public AnimalBehaviour partner;

    private void Start()
    {
        if (stat.dna == null)
        {
            stat.dna = new DNA(stat);
            Initialize();
        }
    }

    public void Initialize()
    {
        stat.initialize();
        SetupTree();
    }

    public abstract void SetupTree();

    private void Update()
    {
        stat.Metabolic();
        stat.StaminaConsum();
        Draw.CircleXY(transform.position, stat.ViewRange);
        tree.Update();
        aiPath.maxSpeed = stat.Speed;
        aiPath.SearchPath();
    }

    #region 컨디션 메소드
    public bool HungryCondition => stat.satiety <= stat.maxSatiety * 0.5f ? true : false;

    public bool LowStaminaCondition => stat.stamina <= stat.maxStamina * 0.5f ? true : false;

    public bool LowHealthCondition => stat.health <= stat.maxHealth * 0.5f ? true : false;
    #endregion
}
