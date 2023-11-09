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

        // 각 유전자의 dominantPersonalities 리스트들의 값들을 무작위로 결합
        foreach (Personality personality in gene1.dominantPersonalities)
        {
            if (Random.value < 0.5f)
            {
                dominantPersonalities.Add(personality);
            }
            else
            {
                recessivePersonalities.Add(personality);
            }
        }

        foreach (Personality personality in gene2.dominantPersonalities)
        {
            if (Random.value < 0.5f)
            {
                dominantPersonalities.Add(personality);
            }
            else
            {
                recessivePersonalities.Add(personality);
            }
        }

        //recessivePersonalities에 중복되는 값이 있으면 두 값을 지우고 dominantPersonalities에 추가
        recessivePersonalities.Sort();
        for (int i = 0; i < recessivePersonalities.Count - 1; i++)
        {
            if (recessivePersonalities[i] == recessivePersonalities[i + 1])
            {
                dominantPersonalities.Add(recessivePersonalities[i]);
                recessivePersonalities.RemoveAt(i);
                recessivePersonalities.RemoveAt(i);
                i--;
            }
        }

        // dominantPersonalities에 중복되는 값이 있으면 하나의 값을 지운다
        dominantPersonalities.Sort();
        for (int i = 0; i < dominantPersonalities.Count - 1; i++)
        {
            if (dominantPersonalities[i] == dominantPersonalities[i + 1])
            {
                dominantPersonalities.RemoveAt(i);
                i--;
            }
        }

        if (dominantPersonalities.Contains(Personality.Timid) && dominantPersonalities.Contains(Personality.Aggressive))
        {
            //둘 중 하나를 무작위로 선택
            if (Random.value < 0.5f)
            {
                dominantPersonalities.Remove(Personality.Aggressive);
                if (!recessivePersonalities.Contains(Personality.Aggressive))
                {
                    recessivePersonalities.Add(Personality.Aggressive);
                }
            }
            else
            {
                dominantPersonalities.Remove(Personality.Timid);
                if (!recessivePersonalities.Contains(Personality.Timid))
                {
                    recessivePersonalities.Add(Personality.Timid);
                }
            }
        }

        if (dominantPersonalities.Contains(Personality.Lazy) && dominantPersonalities.Contains(Personality.Active))
        {
            //둘 중 하나를 무작위로 선택
            if (Random.value < 0.5f)
            {
                dominantPersonalities.Remove(Personality.Active);
                if (!recessivePersonalities.Contains(Personality.Active))
                {
                    recessivePersonalities.Add(Personality.Active);
                }
            }
            else
            {
                dominantPersonalities.Remove(Personality.Lazy);
                if (!recessivePersonalities.Contains(Personality.Lazy))
                {
                    recessivePersonalities.Add(Personality.Lazy);
                }
            }
        }

        if (dominantPersonalities.Contains(Personality.Dependent) && dominantPersonalities.Contains(Personality.Independent))
        {
            //둘 중 하나를 무작위로 선택
            if (Random.value < 0.5f)
            {
                dominantPersonalities.Remove(Personality.Independent);
                if (!recessivePersonalities.Contains(Personality.Independent))
                {
                    recessivePersonalities.Add(Personality.Independent);
                }
            }
            else
            {
                dominantPersonalities.Remove(Personality.Dependent);
                if (!recessivePersonalities.Contains(Personality.Dependent))
                {
                    recessivePersonalities.Add(Personality.Dependent);
                }
            }
        }

        // 확률적으로 돌연변이를 일으켜 무작위 성격을 지우거나 가지고 있지 않은 성격을 추가
        if (Random.value < 0.005f)
        {
            int ran = Random.Range(0, dominantPersonalities.Count);
            recessivePersonalities.Add(dominantPersonalities[ran]);
            dominantPersonalities.RemoveAt(ran);
        }
        else if (Random.value < 0.005f)
        {
            while (true)
            {
                int ran = Random.Range(0, Personality.GetValues(typeof(Personality)).Length);
                Personality personality = (Personality)Personality.GetValues(typeof(Personality)).GetValue(ran);
                if (!dominantPersonalities.Contains(personality))
                {
                    if (recessivePersonalities.Contains(personality))
                    {
                        recessivePersonalities.Remove(personality);
                    }
                    dominantPersonalities.Add(personality);

                    break;
                }
            }
        }
    }
}
