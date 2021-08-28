using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float initialDelay = 4f;
    [SerializeField] float minTimeBetweenSpawns = 1f;
    [SerializeField] float maxTimeBetweenSpawns = 5f;

    [SerializeField] GameObject enemyPrefab;

    [SerializeField] bool hasQuota = false;
    [SerializeField] int quota = 25;

    void Awake()
    {
        Invoke("StartSpawning", initialDelay);
    }

    void StartSpawning()
    {
        if (hasQuota) {
            StartCoroutine("SpawnNumberedEnemies");
        } else {
            StartCoroutine("SpawnEnemies");
        }
        
    }

    public void StopSpawning()
    {
        StopCoroutine("SpawnEnemies");
        StopCoroutine("SpawnNumberedEnemies");
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position + Vector3.up, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
        }
    }

    IEnumerator SpawnNumberedEnemies()
    {
        for (int i = 0; i < quota; i++)
        {
            Instantiate(enemyPrefab, transform.position + Vector3.up, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
        }
    }
}
