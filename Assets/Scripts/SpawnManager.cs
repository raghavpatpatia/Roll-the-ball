using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerupPrefabs;
    public float spawnValue = 9.0f;
    public int enemyCount = 0;
    public int waveNumber = 1;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveNumber);
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateRandomPosition(), powerupPrefabs[randomPowerup].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % bossRound == 0)
            {
                spawnBossWave(waveNumber);
            }
            else
            {
                spawnEnemyWave(waveNumber);
            }
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateRandomPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }

    void spawnEnemyWave(int spawnNumber)
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateRandomPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnValue, spawnValue);
        float spawnZ = Random.Range(-spawnValue, spawnValue);

        Vector3 enemySpawnPos = new Vector3(spawnX, 0, spawnZ);
        return enemySpawnPos;
    }

    void spawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn;
        if (bossRound != 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemiesToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateRandomPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemiesToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateRandomPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
}
