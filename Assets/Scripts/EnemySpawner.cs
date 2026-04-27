using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]

    [Header("Enemy Spawns")]
    public GameObject Gobbler;
    public GameObject Scrapion;

    [Header("Spawning Information")]
    // public float minSpawnDistance;
    // public float maxSpawnDistance;
    public float enemySpawnRate;
    public int maxNumEnemies;
    public int numEnemies;
    public bool enableSpawning = true;
    public bool scrapionCanSpawn;
    public int numEnemiesKilled;

    [Header("Gobbler Spawning Area")]
    public float spawnRange;
    float spawnAreaAXPos;
    float spawnAreaAZPos;
    float spawnAreaBXPos;
    float spawnAreaBZPos;
    float spawnAreaCXPos;
    float spawnAreaCZPos;
    float spawnAreaDXPos;
    float spawnAreaDZPos;
    public float timeElapsed;
    float whichSide;
    float whichEnemy;
    bool readyToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ResetSpawning), enemySpawnRate);
        StartCoroutine(IncreaseSpawns());
        numEnemies = 1;
        SpawnEnemyMethod();
        scrapionCanSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        // Debug.Log(timeElapsed);

        if (readyToSpawn && numEnemies <= maxNumEnemies && enableSpawning){
            readyToSpawn = false;
            StartCoroutine(SpawnEnemyMethod());
            Invoke(nameof(ResetSpawning), enemySpawnRate);
        }

        if (timeElapsed >= 10){
            scrapionCanSpawn = true;
            Debug.Log(scrapionCanSpawn);
        }
    }

    IEnumerator IncreaseSpawns(){
        yield return new WaitForSeconds(30);

        while (enableSpawning)
        {
            enemySpawnRate /= 1.5f;
            yield return new WaitForSeconds(30);
        }
    }
    void ResetSpawning(){
        readyToSpawn = true;
    }

    IEnumerator SpawnEnemyMethod(){
        whichSide = UnityEngine.Random.Range(1, 5);
        whichEnemy = UnityEngine.Random.Range(1, 20); 

        float xPos, zPos;

        if (whichSide == 1){
            xPos = UnityEngine.Random.Range(spawnRange, spawnRange * 2);
            zPos = UnityEngine.Random.Range(spawnRange, spawnRange * -1);
        } 
        else if (whichSide == 2){
            xPos = UnityEngine.Random.Range(spawnRange, spawnRange * -1);
            zPos = UnityEngine.Random.Range(spawnRange * -1, spawnRange * -2);
        } 
        else if (whichSide == 3){
            xPos = UnityEngine.Random.Range(spawnRange * -1, spawnRange * -2);
            zPos = UnityEngine.Random.Range(spawnRange * -1, spawnRange);
        } 
        else {
            xPos = UnityEngine.Random.Range(spawnRange * -1, spawnRange);
            zPos = UnityEngine.Random.Range(spawnRange, spawnRange * 2);
        }

        if (whichEnemy < 19){
            Instantiate(Gobbler, new Vector3(xPos, 0, zPos), Quaternion.identity);
        } 
        else if (whichEnemy == 19 && scrapionCanSpawn){
            Instantiate(Scrapion, new Vector3(xPos, 0, zPos), Quaternion.identity);
        }

        numEnemies += 1;
        yield return new WaitForSeconds(enemySpawnRate);
    }
}
