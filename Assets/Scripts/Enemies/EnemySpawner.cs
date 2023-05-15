using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyMeleePrefab;
    public GameObject enemyRangedPrefab;
    public GameObject enemyGrenadePrefab;

    public int enemiesPerWave = 3;
    public float spawnInterval = 1f;
    public float spawnDelay = 2f;

    public GameObject bossPrefab;
    public int bossPerWave = 0;
    public float spawnBossInterval = 3f;
    public float spawnBossDelay = 6f;

    public int enemyCount = 0;

    private int enemiesSpawnedForWave;
    private int bossesSpawnedForWave;

    public bool isSpawningWave = false;

    private int xPos;
    private int yPos;
    private int enemyType;

    public IEnumerator SpawnWave()
    {

        isSpawningWave = true;

        if (bossPerWave == 0)
        {

            yield return new WaitForSeconds(spawnDelay);

            while (enemiesSpawnedForWave < enemiesPerWave)
            {

                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);

            }

        }
        else
        {

            yield return new WaitForSeconds(spawnBossDelay);

            while (bossesSpawnedForWave < bossPerWave)
            {

                SpawnBoss();
                yield return new WaitForSeconds(spawnBossInterval);

            }

        }

        isSpawningWave = false;
        bossPerWave = 0;

    }

    void SpawnEnemy()
    {

        xPos = Random.Range(-20, 21);
        yPos = Random.Range(-20, 21);
        enemyType = Random.Range(1, 4);

        if (enemyType == 1) {

            Instantiate(enemyMeleePrefab, new Vector3(xPos, 2, yPos), Quaternion.identity);

        } else if (enemyType == 2) {

            Instantiate(enemyRangedPrefab, new Vector3(xPos, 2, yPos), Quaternion.identity);

        } else if (enemyType == 3) {

            Instantiate(enemyGrenadePrefab, new Vector3(xPos, 2, yPos), Quaternion.identity);

        }

        enemyCount++;
        enemiesSpawnedForWave++;

    }

    void SpawnBoss()
    {

        yPos = Random.Range(-20, 21);
        xPos = Random.Range(-20, 21);

        Instantiate(bossPrefab, new Vector3(xPos, 2, yPos), Quaternion.identity);

        enemyCount++;
        bossesSpawnedForWave++;

    }

    public void ResetEnemiesSpawnedForWave()
    {

        enemiesSpawnedForWave = 0;
        bossesSpawnedForWave = 0;

    }
}