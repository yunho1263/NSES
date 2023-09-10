using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBreedMale : AnimalBreed
{
    public override void Breed()
    {
        base.Breed();
    }

    public void Courting(AnimalBreedFemale target)
    {
        if (target.GetPartner(this))
        {
            
        }
    }
}
