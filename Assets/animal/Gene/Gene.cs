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
        //�� ���� �ϳ��� �������� ����
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

        // �� �������� recessiveValues�� ����
        recessiveValues.AddRange(gene1.recessiveValues);
        recessiveValues.AddRange(gene2.recessiveValues);
        recessiveValues.Add(recessiveValue);

        // ����
        recessiveValues.Sort();

        //�������� �ȿ� ���� ������ �ִٸ� ���� ����
        List<float> combinedRecessives = new List<float>();

        for (int i = 1; i < recessiveValues.Count; i++)
        {
            if (Mathf.Abs(recessiveValues[i] - recessiveValues[i - 1]) > Tolerance)
            {
                combinedRecessives.Add(recessiveValues[i]);
            }
        }

        // combinedRecessives �� ������ �Ѱ��� ����, �������� recessiveValues���� ����

    }
}
