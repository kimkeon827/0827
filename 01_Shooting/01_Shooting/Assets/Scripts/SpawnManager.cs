using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    Vector3[] positions = new Vector3[5];

    public GameObject enemy;

    public bool isSpawn = false;

    public float spawnDelay = 1.5f;
    float spawnTimer = 0f;

    private void Awake()
    {
        if (SpawnManager.instance == null)
        {
            SpawnManager.instance = this;
        }
    }

    private void Start()
    {
        CreatePositions();
    }

    void CreatePositions()  // 적이 나오는 지점을 카메라의 월드좌표로 정의
    {
        float viewPosY = 1.2f;
        float gapX = 1f / 6f;
        float viewPosX = 0f;

        for(int i = 0; i < positions.Length; i++)
        {
            viewPosX = gapX + gapX * i;
            Vector3 viewPos = new Vector3(viewPosX, viewPosY, -6);
            Vector3 WorldPos = Camera.main.ViewportToWorldPoint(viewPos);
            WorldPos.z = -6f;
            positions[i] = WorldPos;
        }
    }

    void SpawnEnemy()   // isSpawn이 true일 때 적을 랜덤하게 생성
    {
        if (isSpawn == true)
        {
            if (spawnTimer > spawnDelay)
            {
                int rand = Random.Range(0, positions.Length);
                Instantiate(enemy, positions[rand], Quaternion.identity);
                spawnTimer = 0f;
            }

            spawnTimer += Time.deltaTime;
        }
    }

    private void Update()
    {
        SpawnEnemy();
    }
}
