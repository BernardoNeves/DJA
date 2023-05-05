using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public GameObject _player;

    public GameObject chestPrefab;
    public GameObject chestBossPrefab;

    public EnemySpawner enemySpawner;

    public VictoryMenu victoryStart; 

    public int waveNumber = 0;

    private int bossNumber;

    public bool gameWon = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
        InventoryManager.Instance.ListItems();

    }

    public GameObject Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    public PlayerHealth PlayerHealth
    {
        get
        {
            return _player.GetComponent<PlayerHealth>();
        }
    }

    public bool GameWon
    {
        get
        {
            return gameWon;
        }
        set
        {
            gameWon = value;
        }
    }


    void Start() {

        Time.timeScale = 1f;

        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        victoryStart = GameObject.FindObjectOfType<VictoryMenu>();

    }

    void Update() {

        if (enemySpawner.enemyCount == 0 && !enemySpawner.isSpawningWave) {

            if (waveNumber == 11) {

                victoryStart.Victory();

            }

            if (!gameWon) {

                if (waveNumber % 10 == 0 && waveNumber != 0) {
                
                    GameObject chest = Instantiate(chestBossPrefab, new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f)), Quaternion.identity);
                
                } else {

                    GameObject chest = Instantiate(chestPrefab, new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f)), Quaternion.identity);

                }

                waveNumber++;

                if (waveNumber % 10 == 0)
                {

                    bossNumber = (waveNumber / 10) % 5;

                    enemySpawner.bossPerWave = bossNumber;
                    enemySpawner.ResetEnemiesSpawnedForWave();
                    enemySpawner.StartCoroutine(enemySpawner.SpawnWave());

                }
                else
                {

                    enemySpawner.enemiesPerWave = waveNumber * 2;
                    enemySpawner.ResetEnemiesSpawnedForWave();
                    enemySpawner.StartCoroutine(enemySpawner.SpawnWave());

                }

            }

        }

    }

}
