using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DNA_Util
{
    public static float SizeMutateRange_min = 0.1f;
    public static float SizeMutateRange_max = 10f;

    public static float HealthMutateRange_min = 10f;
    public static float HealthMutateRange_max = 300f;

    public static float StaminaMutateRange_min = 10f;
    public static float StaminaMutateRange_max = 300f;

    public static float SatietyMutateRange_min = 10f;
    public static float SatietyMutateRange_max = 300f;

    public static float SpeedMutateRange_min = 0.1f;
    public static float SpeedMutateRange_max = 20f;

    public static float ViewRangeMutateRange_min = 0.1f;
    public static float ViewRangeMutateRange_max = 20f;

    public static float DamageMutateRange_min = 0.1f;
    public static float DamageMutateRange_max = 30f;

    public static float MaxBreedCountMutateRange_min = 0f;
    public static float MaxBreedCountMutateRange_max = 10f;

    public static float BreedCooldownMutateRange_min = 0f;
    public static float BreedCooldownMutateRange_max = 30f;

    public static float MinBreedPerTimeMutateRange_min = 0f;
    public static float MinBreedPerTimeMutateRange_max = 10f;

    public static float MaxBreedPerTimeMutateRange_min = 0f;
    public static float MaxBreedPerTimeMutateRange_max = 10f;
}

public class DNA
{
    List<Gene> genes = new List<Gene>();

    public DNA(AnimalStat stat)
    {
        //모든 유전자를 생성한다.
        for (int i = 0; i < 10; i++)
        {
            Gene nG;
            if (i == (int)Gene.GeneticTraits.Personality)
            {
                nG = new PersonalityGene((Gene.GeneticTraits)i);
            }
            else nG = new Gene((Gene.GeneticTraits)i);

            switch (nG.geneticTraits)
            {
                case Gene.GeneticTraits.XSize:
                    nG.dominantValue = stat.xSize;
                    break;
                case Gene.GeneticTraits.YSize:
                    nG.dominantValue = stat.ySize;
                    break;
                case Gene.GeneticTraits.Health:
                    nG.dominantValue = stat.maxHealth;
                    break;
                case Gene.GeneticTraits.Stamina:
                    nG.dominantValue = stat.maxStamina;
                    break;
                case Gene.GeneticTraits.Satiety:
                    nG.dominantValue = stat.maxSatiety;
                    break;
                case Gene.GeneticTraits.Speed:
                    nG.dominantValue = stat.maxSpeed;
                    break;
                case Gene.GeneticTraits.ViewRange:
                    nG.dominantValue = stat.ViewRange;
                    break;
                case Gene.GeneticTraits.Damage:
                    nG.dominantValue = stat.maxDamage;
                    break;
                case Gene.GeneticTraits.MaxBreedCount:
                    nG.dominantValue = stat.maxBreedCount;
                    break;
                case Gene.GeneticTraits.BreedCooldown:
                    nG.dominantValue = stat.defaultbreedCooldown;
                    break;
                case Gene.GeneticTraits.MinBreedPerTime:
                    nG.dominantValue = stat.minBreedPerTime;
                    break;
                case Gene.GeneticTraits.MaxBreedPerTime:
                    nG.dominantValue = stat.maxBreedPerTime;
                    break;
                case Gene.GeneticTraits.Personality:
                    break;
                default:
                    break;
            }

            genes.Add(nG);
        }
    }

    public DNA(DNA dna1, DNA dna2)
    {
        Combine(dna1, dna2);
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
                genes.Add(new Gene(gene1.geneticTraits, gene1, gene2));
            }
        }
    }
}
