using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    List<Gene> genes = new List<Gene>();

    public DNA(DNA dna1, DNA dna2)
    {

    }

    public void Combine(DNA dna1, DNA dna2)
    {
        foreach (Gene gene1 in dna1.genes)
        {
            Gene gene2 = dna2.genes.Find(g => g.geneticTraits == gene1.geneticTraits);
            if (gene2 == null)
            {
                genes.Add(gene1);
            }
            else
            {
                genes.Add(new Gene(gene1, gene2));
            }
        }
    }
}
