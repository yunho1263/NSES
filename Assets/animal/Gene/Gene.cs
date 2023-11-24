using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    public enum GeneticTraits
    {
        XSize,
        YSize,
        Health,
        Stamina,
        Satiety,
        Speed,
        ViewRange,
        Damage,
        MaxBreedCount,
        BreedCooldown,
        MinBreedPerTime,
        MaxBreedPerTime,
        Personality
    }

    public GeneticTraits geneticTraits;
    public float dominantValue;
    public List<float> recessiveValues = new List<float>();

    public float Tolerance => 0.05f;

    public Gene(GeneticTraits genetic, Gene gene1, Gene gene2)
    {
        if (geneticTraits == GeneticTraits.Personality)
        {
            return;
        }

        geneticTraits = genetic;

        float recessiveValue;
        //두 값중 하나를 무작위로 선택
        if (Random.value < 0.5f)
        {
            dominantValue = gene1.dominantValue;
            recessiveValue = gene2.dominantValue;
        }
        else
        {
            dominantValue = gene2.dominantValue;
            recessiveValue = gene1.dominantValue;
        }

        // 두 유전자의 recessiveValues를 결합
        recessiveValues.AddRange(gene1.recessiveValues);
        recessiveValues.AddRange(gene2.recessiveValues);
        recessiveValues.Add(recessiveValue);

        // 정렬
        recessiveValues.Sort();

        //오차범위 안에 들어온 값들이 있다면 결합 성공
        List<float> combinedRecessives = new List<float>();

        for (int i = 1; i < recessiveValues.Count; i++)
        {
            if (Mathf.Abs(recessiveValues[i] - recessiveValues[i - 1]) > Tolerance)
            {
                combinedRecessives.Add(recessiveValues[i]);
                recessiveValues.RemoveAt(i - 1);
                i--;
            }
        }
        // combinedRecessives 중 무작위 한개를 선택

        int ran = Random.Range(0, combinedRecessives.Count);
        float v = combinedRecessives[ran];

        //확률적으로 우성, 열성, 돌연변이를 결정

        if (Random.value < 0.5f)
        {
            recessiveValues.Remove(v);
            float tmp = dominantValue;
            dominantValue = v;
            v = tmp;
            recessiveValues.Add(v);
        }

        // 돌연변이
        if (Random.value < 0.005f)
        {
            recessiveValues.Add(dominantValue);
            switch (geneticTraits)
            {
                case GeneticTraits.XSize:
                    dominantValue = Random.Range(DNA_Util.SizeMutateRange_min, DNA_Util.SizeMutateRange_max);
                    break;
                case GeneticTraits.YSize:
                    dominantValue = Random.Range(DNA_Util.SizeMutateRange_min, DNA_Util.SizeMutateRange_max);
                    break;
                case GeneticTraits.Health:
                    dominantValue = Random.Range(DNA_Util.HealthMutateRange_min, DNA_Util.HealthMutateRange_max);
                    break;
                case GeneticTraits.Stamina:
                    dominantValue = Random.Range(DNA_Util.StaminaMutateRange_min, DNA_Util.StaminaMutateRange_max);
                    break;
                case GeneticTraits.Satiety:
                    dominantValue = Random.Range(DNA_Util.SatietyMutateRange_min, DNA_Util.SatietyMutateRange_max);
                    break;
                case GeneticTraits.Speed:
                    dominantValue = Random.Range(DNA_Util.SpeedMutateRange_min, DNA_Util.SpeedMutateRange_max);
                    break;
                case GeneticTraits.ViewRange:
                    dominantValue = Random.Range(DNA_Util.ViewRangeMutateRange_min, DNA_Util.ViewRangeMutateRange_max);
                    break;
                case GeneticTraits.Damage:
                    dominantValue = Random.Range(DNA_Util.DamageMutateRange_min, DNA_Util.DamageMutateRange_max);
                    break;
                case GeneticTraits.MaxBreedCount:
                    dominantValue = Random.Range(DNA_Util.MaxBreedCountMutateRange_min, DNA_Util.MaxBreedCountMutateRange_max);
                    break;
                case GeneticTraits.BreedCooldown:
                    dominantValue = Random.Range(DNA_Util.BreedCooldownMutateRange_min, DNA_Util.BreedCooldownMutateRange_max);
                    break;
                case GeneticTraits.MinBreedPerTime:
                    dominantValue = Random.Range(DNA_Util.MinBreedPerTimeMutateRange_min, DNA_Util.MinBreedPerTimeMutateRange_max);
                    break;
                case GeneticTraits.MaxBreedPerTime:
                    dominantValue = Random.Range(DNA_Util.MaxBreedPerTimeMutateRange_min, DNA_Util.MaxBreedPerTimeMutateRange_max);
                    break;
                default:
                    break;
            }
        }
    }

    public Gene(GeneticTraits i)
    {
        geneticTraits = i;
    }
}
