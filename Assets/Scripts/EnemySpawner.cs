using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float maxTimeLife = 2f;

    private float spawnNext = 0;
    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnNext){
            float rand = Random.Range(-4f, 5f);
            spawnNext = Time.time + 60/spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;
            Vector2 spawnPosition = new Vector2(rand, 8f);
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxTimeLife);
        }
    }
}
