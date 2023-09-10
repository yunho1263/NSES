using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType
{
    Herbivore,
    Carnivore,
    Omnivore
}

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

public class Animal : MonoBehaviour
{
    public State state;
    public AnimalType animalType;
    [SerializeField]
    public AnimalStat animalStat;
    public AnimalBreed animalBreed;


    public void Initialize()
    {

    }
}
