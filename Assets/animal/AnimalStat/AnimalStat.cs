using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public enum AnimalType
{
    Herbivore,
    Carnivore,
    Omnivore
}

public enum Personality
{
    Timid, // 소심함
    Aggressive, // 공격적

    Lazy, //게으름
    Active, //활동적

    Dependent, //의존적
    Independent, //독립적
}

public enum Sex
{
    Male,
    Female
}

public class AnimalStat : MonoBehaviour
{
    public AnimalType animalType;
    public List<Personality> personalitys;
    public Sex sex;

    public float maxHealth;
    public float health;

    public float maxStamina;
    public float stamina;

    public float maxHunger;
    public float hunger;

    public float maxDamage;
    public float damage;

    public float maxSpeed;
    public float speed;
    public bool isMoving;
    public bool isRunning;
    public bool isResting;
    public float BasicStaminaConsumption;
    public float RunningStaminaConsumption;

    public float maxAge;
    public float age;

    public float maxBreedCouunt;
    public float defaultbreedCooldown;

    public float minBreedPerTime;
    public float maxBreedPerTime;

    public float xSize;
    public float ySize;

    public void SetMoving(bool value)
    {
        isMoving = value;
    }

    public void SetRunning(bool value)
    {
        if (value)
        {
            if (stamina < BasicStaminaConsumption)
            {
                isRunning = false;
                isResting = true;
                return;
            }

            isRunning = true;
            isResting = false;
        }

        else
        {
            if (stamina < BasicStaminaConsumption)
            {
                isResting = true;
            }
            isRunning = false;
        }
    }
}
