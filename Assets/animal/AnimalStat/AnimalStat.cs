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
    public DNA dna;

    #region 기본 스탯
    public AnimalType animalType;
    public List<Personality> personalitys;

    public float maxHealth;
    public float health;

    public float maxStamina;
    public float stamina;

    public float maxSatiety;
    public float satiety;

    public float maxDamage;
    public float damage;

    public float maxAge;
    public float age;

    #endregion

    #region 이동 관련
    public float MetabolicRate => 1f * (xSize * ySize);
    public float MetMetabolicGein => 1f * (xSize * ySize) * 2;

    public float Speed => isRunning ? maxSpeed : maxSpeed * 0.5f;
    public float maxSpeed;
    public bool isMoving;
    public bool isRunning;
    public float BasicStaminaConsumption => Speed * (xSize * ySize);
    public float RunningStaminaConsumption => BasicStaminaConsumption * 2f;
    public float MinStaminaToRecall => maxStamina * 0.25f;
    #endregion

    #region 번식 관련
    public Sex sex;

    public float maxBreedCount;
    public float defaultbreedCooldown;

    public float minBreedPerTime;
    public float maxBreedPerTime;

    public bool canBreed;

    public float breedCount;
    public float breedcooldown;

    public GameObject babyPrefab;
    #endregion

    #region 크기, 시야관련
    public float xSize;
    public float ySize;

    public float ViewRange;

    public LayerMask NaturalEnemyLayerMask;
    #endregion


    public void initialize()
    {
        health = maxHealth;
        stamina = maxStamina;
        satiety = maxSatiety * 0.7f;

        NaturalEnemyLayerMask = 0;

        switch (animalType)
        {
            case AnimalType.Herbivore:
                NaturalEnemyLayerMask = 1 << LayerMask.NameToLayer("Omnivore") | 1 << LayerMask.NameToLayer("Carnivore");
                break;
            case AnimalType.Omnivore:
                NaturalEnemyLayerMask = 1 << LayerMask.NameToLayer("Carnivore");
                break;
            default:
                break;
        }
    }

    public void Metabolic()
    {
        // 포만감이 0 이하면 체력과 스테미나를 감소시킨다.
        if (satiety <= 0)
        {
            health -= health * 0.05f * Time.deltaTime;
            StaminaConsum(stamina * 0.05f * Time.deltaTime);
            return;
        }

        // 포만감이 0 이상이면 포만감을 소모하여 체력과 스태미너를 회복시킨다.
        if (health < maxHealth)
        {
            satiety -= MetabolicRate * Time.deltaTime;
            health += MetMetabolicGein * Time.deltaTime;

            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        if (stamina < maxStamina)
        {
            satiety -= MetabolicRate * Time.deltaTime;
            stamina += MetMetabolicGein * Time.deltaTime;

            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }

        return;
    }

    public void StaminaConsum()
    {
        float staminaConsumption = BasicStaminaConsumption;
        if (isRunning)
        {
            staminaConsumption = RunningStaminaConsumption;
        }

        if (stamina <= 0)
        {
            health -= staminaConsumption;
            return;
        }

        if (isMoving)
        {
            stamina -= staminaConsumption * Time.deltaTime;

            if(stamina <= 0)
            {
                stamina = 0;
            }
        }
        return;
    }

    public void StaminaConsum(float value)
    {
        if (stamina <= 0)
        {
            health -= value;
            return;
        }

        stamina -= value;

        if (stamina <= 0)
        {
            stamina = 0;
        }
    }
    public void SetMoving(bool value, bool Running)
    {
        if (stamina <= 0)
        {
            stamina = 0;
            isMoving = false;
            isRunning = false;
            return;
        }

        if (value)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Running)
        {
            isRunning = true;
        }

        else
        {
            isRunning = false;
        }
    }
}
