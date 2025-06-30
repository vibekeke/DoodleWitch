using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;
    public int maxInstances = 5;

    [Header("References")]
    public Camera mainCamera; // Optional, can auto-detect

    private List<GameObject> spawnedEnemies = new List<GameObject>();


    public float spawnCooldownMin = 2f;
    public float spawnCooldownMax = 5f;
    public float yOffsetMin = -2f;
    public float yOffsetMax = 2f;
    public float xOffset = 10f;

    private float countdown;
    private float yOffset;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        SetSpawnTimer();
    }

    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 camPos = mainCamera.transform.position;
            transform.position = new Vector3(camPos.x + xOffset, camPos.y, 0);
        }

        // Countdown logic
        countdown -= Time.deltaTime;
        if (countdown <= 0f) {
            if (spawnedEnemies.Count < maxInstances) {
                Vector3 spawnPosition = transform.position + new Vector3(xOffset, yOffset, 0f);
                SpawnEnemy(spawnPosition);
                }
            SetSpawnTimer();
        }
    }
    private void SetSpawnTimer()
        {
            countdown = Random.Range(spawnCooldownMin, spawnCooldownMax);
            yOffset = Random.Range(yOffsetMin, yOffsetMax);
        }

    void SpawnEnemy(Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        spawnedEnemies.Add(enemy);

        // Optional: Auto-remove bubble when destroyed
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            //Essentially - when the bubbleScript invokes onDestroyed, do this:
            enemyScript.onDestroyed += () => spawnedEnemies.Remove(enemy);
        }
    }
}