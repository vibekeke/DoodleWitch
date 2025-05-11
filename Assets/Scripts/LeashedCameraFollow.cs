using UnityEngine;

public class LeashedCameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Scroll Settings")]
    public float y_offset = 2f;
    public float baseScrollSpeed = 2f;          // constant forward scroll
    public float catchUpSpeed = 5f;             // how fast camera reacts when player gets too far
    public float leashDistance = 2f;            // how far ahead player can get before camera follows
    public float verticalFollowSpeed = 2f;      // optional: smooth follow for Y movement

    private float targetX;

    void Start()
    {
        targetX = transform.position.x;
    }

    void LateUpdate()
    {
        float playerX = player.position.x;

        // Move forward at base scroll speed
        targetX += baseScrollSpeed * Time.deltaTime;

        // If player goes too far ahead, catch up
        float maxAllowedX = targetX + leashDistance;
        if (playerX > maxAllowedX)
        {
            targetX = Mathf.Lerp(targetX, playerX - leashDistance, Time.deltaTime * catchUpSpeed);
        }

        // Smooth vertical follow (optional)
        float targetY = Mathf.Lerp(transform.position.y, player.position.y - y_offset, Time.deltaTime * verticalFollowSpeed);

        // Apply new camera position
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}

