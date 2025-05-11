using UnityEngine;
using System.Collections.Generic;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject bubblePrefab;
    public int maxBubbles = 5;
    public float noSpawnRadius = 0.2f;
    public LayerMask playerLayerMask;

    [Header("References")]
    public Camera mainCamera; // Optional, can auto-detect

    private List<GameObject> spawnedBubbles = new List<GameObject>();

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (spawnedBubbles.Count < maxBubbles &&
                Physics2D.OverlapCircle(mousePosition, noSpawnRadius, playerLayerMask) == null)
                    {
                        SpawnBubble(mousePosition);
                    }
            }
        }

    void SpawnBubble(Vector3 position)
    {
        GameObject bubble = Instantiate(bubblePrefab, position, Quaternion.identity);
        spawnedBubbles.Add(bubble);

        // Optional: Auto-remove bubble when destroyed
        Bubble bubbleScript = bubble.GetComponent<Bubble>();
        if (bubbleScript != null)
        {
            //Essentially - when the bubbleScript invokes onDestroyed, do this:
            bubbleScript.onDestroyed += () => spawnedBubbles.Remove(bubble);
        }
    }
}