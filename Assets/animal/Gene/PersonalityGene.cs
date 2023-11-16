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

        // recessivePersonalities를 모두 가져온다
        recessivePersonalities.AddRange(gene1.recessivePersonalities);
        recessivePersonalities.AddRange(gene2.recessivePersonalities);

        // dominantPersonalities를 결합한다
        dominantPersonalities.AddRange(gene1.dominantPersonalities);
        dominantPersonalities.AddRange(gene2.dominantPersonalities);

        // dominantPersonalities의 중복을 제거한다
        dominantPersonalities.Sort();
        for (int i = 0; i < dominantPersonalities.Count - 1; i++)
        {
            if (dominantPersonalities[i] == dominantPersonalities[i + 1])
            {
                dominantPersonalities.RemoveAt(i);
                i--;
            }
        }

        // 확률적으로 돌연변이를 일으켜 가지고 있지 않은 성격을 추가하거나 가지고 있는 성격을 제거한다
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

        // recessivePersonalities에 중복이 있으면 둘 다 제거하고 dominantPersonalities에 추가한다
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

        // 대응되는 성격이 있으면 둘 중 하나를 recessivePersonalities로 옮긴다
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
