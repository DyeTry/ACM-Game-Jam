using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField] GameObject[] monsterSpawn;

        public GameObject[] GetMonsterSpawn()
        {
            return monsterSpawn;
        }
    }

    [SerializeField] WaveContent[] waves;
    int currentWave = 0;
    float spawnRange = 10;

    void Start()
    {
        SpawnWave();
    }


    void Update()
    {
        
    }

    void SpawnWave()
    {
        for (int i = 0; i < waves[currentWave].GetMonsterSpawn().Length; i++)
        {
            Instantiate(waves[currentWave].GetMonsterSpawn()[i], FindSpawnLoc(), Quaternion.identity);
        }
    }

    Vector3 FindSpawnLoc()
    {
        Vector3 spawnPos;
        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = transform.position.y;

        spawnPos= new Vector3(xLoc, yLoc, zLoc);

        if (Physics.Raycast(spawnPos, Vector3.down, 10))
        {
            return spawnPos;
        }
        else
        {
            return FindSpawnLoc();
        }
    }
}
