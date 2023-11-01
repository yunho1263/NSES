using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.Mathematics;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float maxXposition;
    public float maxYposition;

    public float minGrowTime;
    public float maxGrowTime;
    public float growTime;
    public float time;

    public float growRadius;

    public int remaining;
    public float FullnessPerServing => transform.localScale.x * 10;

    public int maxPlantCount;
    public GameObject plantPrefab;


    private void Start()
    {
        SetRandomGrowTime();
    }

    private void Update()
    {
        //주기적으로 식물을 생성
        time += Time.deltaTime;
        if(time > growTime)
        {
            time = 0;
            Grow();
            SetRandomGrowTime();
        }
    }

    private void SetRandomGrowTime()
    {
        growTime = UnityEngine.Random.Range(minGrowTime, maxGrowTime);
    }

    private void Grow()
    {
        // 범위 내에 다른 식물이 일정 수량 넘게 있는지 확인
        LayerMask layerMask = 1 << LayerMask.NameToLayer("Plants");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, growRadius * 0.5f , layerMask);

        if(colliders.Length > maxPlantCount)
        {
            return;
        }

        Vector3 position = new Vector3(UnityEngine.Random.Range(-growRadius, growRadius), UnityEngine.Random.Range(-growRadius, growRadius), 1);
        position += transform.position;

        if (position.x > maxXposition)
        {
            position.x = maxXposition;
        }
        else if (position.x < -maxXposition)
        {
            position.x = -maxXposition;
        }

        if (position.y > maxYposition)
        {
            position.y = maxYposition;
        }
        else if (position.y < -maxYposition)
        {
            position.y = -maxYposition;
        }

        //생성
        GameObject plant = Instantiate(plantPrefab, position, Quaternion.identity);

        float scale = UnityEngine.Random.Range(0.3f, 1f);
        plant.transform.localScale = new Vector3(scale, scale, scale);
    }
}
