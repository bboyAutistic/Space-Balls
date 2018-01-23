using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    float spawnTimer = 5f;
    float repeatTimer = 20f;

    

    void Start()
    {
        StartSpawning();
    }


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void StartSpawning()
    {
        //Invoke("SpawnEnemy", spawnTimer);
        InvokeRepeating("SpawnEnemy", spawnTimer, repeatTimer);
    }
    void StopSpawning()
    {
        CancelInvoke();
    }
}
