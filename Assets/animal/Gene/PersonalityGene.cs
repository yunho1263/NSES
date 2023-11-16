using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalityGene : Gene
{
    public new GeneticTraits geneticTraits = GeneticTraits.Personality;
    public List<Personality> recessivePersonalities;
    public List<Personality> dominantPersonalities;

    public PersonalityGene(PersonalityGene gene1, PersonalityGene gene2): base(GeneticTraits.Personality, gene1, gene2)
    {
        dominantPersonalities = new List<Personality>();
        recessivePersonalities = new List<Personality>();

        // recessivePersonalities�� ��� �����´�
        recessivePersonalities.AddRange(gene1.recessivePersonalities);
        recessivePersonalities.AddRange(gene2.recessivePersonalities);

        // dominantPersonalities�� �����Ѵ�
        dominantPersonalities.AddRange(gene1.dominantPersonalities);
        dominantPersonalities.AddRange(gene2.dominantPersonalities);

        // dominantPersonalities�� �ߺ��� �����Ѵ�
        dominantPersonalities.Sort();
        for (int i = 0; i < dominantPersonalities.Count - 1; i++)
        {
            if (dominantPersonalities[i] == dominantPersonalities[i + 1])
            {
                dominantPersonalities.RemoveAt(i);
                i--;
            }
        }

        // Ȯ�������� �������̸� ������ ������ ���� ���� ������ �߰��ϰų� ������ �ִ� ������ �����Ѵ�
        if (Random.value > 0.05f)
        {
            if (Random.value > 0.5f)
            {
                Personality personality;
                while (true)
                {
                    personality = (Personality)Random.Range(0, 4);
                    if (dominantPersonalities.Contains(personality))
                    {
                        continue;
                    }
                    dominantPersonalities.Add(personality);
                    break;
                }
            }
            else
            {
                if (dominantPersonalities.Count > 0)
                {
                    dominantPersonalities.RemoveAt(Random.Range(0, dominantPersonalities.Count));
                }
            }
        }

        // recessivePersonalities�� �ߺ��� ������ �� �� �����ϰ� dominantPersonalities�� �߰��Ѵ�
        recessivePersonalities.Sort();
        for (int i = 0; i < recessivePersonalities.Count - 1; i++)
        {
            if (recessivePersonalities[i] == recessivePersonalities[i + 1])
            {
                if (!dominantPersonalities.Contains(recessivePersonalities[i]))
                {
                    dominantPersonalities.Add(recessivePersonalities[i]);
                }
                recessivePersonalities.RemoveAt(i);
                recessivePersonalities.RemoveAt(i);
                i--;
            }
        }

        // �����Ǵ� ������ ������ �� �� �ϳ��� recessivePersonalities�� �ű��
        if (dominantPersonalities.Contains(Personality.Timid) && dominantPersonalities.Contains(Personality.Aggressive))
        {
            if (Random.value > 0.5f)
            {
                dominantPersonalities.Remove(Personality.Aggressive);
                recessivePersonalities.Add(Personality.Aggressive);
            }
            else
            {
                dominantPersonalities.Remove(Personality.Timid);
                recessivePersonalities.Add(Personality.Timid);
            }
        }

        if (dominantPersonalities.Contains(Personality.Lazy) && dominantPersonalities.Contains(Personality.Active))
        {
            if (Random.value > 0.5f)
            {
                dominantPersonalities.Remove(Personality.Active);
                recessivePersonalities.Add(Personality.Active);
            }
            else
            {
                dominantPersonalities.Remove(Personality.Lazy);
                recessivePersonalities.Add(Personality.Lazy);
            }
        }

        if (dominantPersonalities.Contains(Personality.Dependent) && dominantPersonalities.Contains(Personality.Independent))
        {
            if (Random.value > 0.5f)
            {
                dominantPersonalities.Remove(Personality.Independent);
                recessivePersonalities.Add(Personality.Independent);
            }
            else
            {
                dominantPersonalities.Remove(Personality.Dependent);
                recessivePersonalities.Add(Personality.Dependent);
            }
        }
    }
}
