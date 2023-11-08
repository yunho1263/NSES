using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityGene : Gene
{
    public new GeneticTraits geneticTraits = GeneticTraits.Personality;
    public List<Personality> personalities;

    public PersonalityGene(Gene gene1, Gene gene2) : base(gene1, gene2)
    {
    }
}
