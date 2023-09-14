using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBreedFemale : AnimalBreed
{
    public GameObject prefeb;

    public AnimalBehaviour partner;
    public AnimalStat partnerStat;
    public AnimalBreedMale partnerAnimalBreed;

    public override void Breed()
    {
        base.Breed();
    }

    public bool GetPartner(AnimalBreedMale partner)
    {
        if (CanBreeding())
        {
            partnerAnimalBreed = partner;
            partnerStat = partnerAnimalBreed.animalStat;

            Breed();
            return true;
        }

        return false;
    }

    public void InitializeChilds()
    {
        
    }
}
