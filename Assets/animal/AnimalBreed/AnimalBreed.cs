using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBreed : MonoBehaviour
{
    public AnimalBehaviour behaviour;
    public AnimalStat animalStat;

    public bool canBreed;
    public bool breedIsReady;

    public float breedCount;
    public float breedcooldown;

    public bool CanBreeding()
    {
        return canBreed && breedIsReady;
    }

    public virtual void Breed()
    {
        canBreed = false;
        breedIsReady = false;
        behaviour.state = State.Breed;

        StartCoroutine(Breeding());
    }

    public virtual IEnumerator Breeding()
    {
        yield return new WaitForSeconds(1f);
        breedCount++;
        behaviour.state = State.Idle;

        StartCoroutine(BreedCoolTime());
    }

    public IEnumerator BreedCoolTime()
    {
        yield return new WaitForSeconds(breedcooldown);
        breedIsReady = true;
    }
}
