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
            Breed();
        }
        else
        {
            canBreed = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            canBreed = false;

            AnimalBreedFemale target = collision.gameObject.GetComponent<AnimalBreedFemale>();

            if (target != null)
            {
                Courting(target);
            }
        }
    }
}
