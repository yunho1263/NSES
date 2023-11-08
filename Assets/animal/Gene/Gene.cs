using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    public enum Expression
    {
        Dominant,
        Recessive,
        Mutated
    }

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

    public float Tolerance => 0.1f;

    public Gene(Gene gene1, Gene gene2)
    {
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
            }
        }

        // combinedRecessives 중 무작위 한개를 선택, 나머지는 recessiveValues에서 삭제

    }
}
