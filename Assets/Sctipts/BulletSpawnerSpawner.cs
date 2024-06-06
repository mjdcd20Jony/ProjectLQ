using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerSpawner : MonoBehaviour
{
    public GameObject[] bulletSpawner;
    public float timeElapsed = 0f;
    public float spawnInterval = 1f; 

    private int currentSpawnerIndex = 0;

    private void Start()
    {
        // ��� ������ ��Ȱ��ȭ
        for (int i = 0; i < bulletSpawner.Length; i++)
        {
            bulletSpawner[i].SetActive(false);
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (currentSpawnerIndex < bulletSpawner.Length)
        {
            if (timeElapsed >= currentSpawnerIndex * spawnInterval)
            {
                bulletSpawner[currentSpawnerIndex].SetActive(true);
                currentSpawnerIndex++;
            }
        }
    }
}
