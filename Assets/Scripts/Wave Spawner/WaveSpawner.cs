using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    public Wave[] waves;
    public int currentWaveIndex = 0;
    private bool readyToCountDown;
    private float spawnRange = 20f;
    private LoadingBar loadingBar;

    private void Start()
    {
        readyToCountDown = true;
        loadingBar = GetComponent<LoadingBar>();
        loadingBar.ShowLoadingBar(waves[currentWaveIndex].timeToNextWave);
        for (int i = 0; i < waves.Length; i++) waves[i].enemiesLeft = waves[i].enemies.Length;
    }

    private void Update()
    {
        if (currentWaveIndex >= waves.Length) return;

        if (readyToCountDown == true) countdown -= Time.deltaTime;
        
        if (countdown <= 0)
        {
            readyToCountDown = false;

            loadingBar.ShowLoadingBar(waves[currentWaveIndex].timeToNextWave);

            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;

            if (currentWaveIndex < waves.Length) loadingBar.ShowLoadingBar(waves[currentWaveIndex].timeToNextWave);
        }
    }

    private IEnumerator SpawnWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                //GameObject enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
                GameObject enemy = Instantiate(waves[currentWaveIndex].enemies[i], FindSpawnLocation(), Quaternion.identity);
                EnemyScalable enemyHealth = enemy.GetComponent<EnemyScalable>();

                if (enemyHealth != null)
                {
                    enemyHealth.SetWaveReference(waves[currentWaveIndex]);
                }

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    private Vector3 FindSpawnLocation()
    {
        Vector3 spawnPos;
        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = GetGroundHeight(new Vector3(xLoc, 0, zLoc)) + 1f;

        spawnPos= new Vector3(xLoc, yLoc, zLoc);

        int maxSpawnAttempts = 10;
        for (int attempt = 0; attempt < maxSpawnAttempts; attempt++)
        {
            if (Physics.Raycast(spawnPos, Vector3.down, 5)) return spawnPos;
            else
            {
                xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
                zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
                yLoc = GetGroundHeight(new Vector3(xLoc, 0, zLoc)) + 1f;

                spawnPos = new Vector3(xLoc, yLoc, zLoc);
            }
        }
        return spawnPos;
    }

    private float GetGroundHeight(Vector3 position)
    {
        RaycastHit hit;
        
        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity)) return hit.point.y;

        return 0f;
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;
    public float timeToNextEnemy = 1f;
    public float timeToNextWave = 10f;

    [HideInInspector] public int enemiesLeft;
}