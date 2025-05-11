using UnityEngine;
using System;

public class Bubble : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceForce = 10f;

    [Tooltip("0 = pure reflection, 1 = fully biased direction")]
    [Range(0f, 1f)]
    [SerializeField] private float squishFactor = 0.35f;

    [Tooltip("How much to favor sideways motion vs. upward motion (0 = pure up, 1 = pure horizontal)")]
    [Range(0f, 1f)]
    [SerializeField] private float horizontalBias = 0.5f;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Get the point of contact
        ContactPoint2D contact = collision.GetContact(0);

        // Raw bounce direction: from bubble center to contact point
        Vector2 rawDirection = (contact.point - (Vector2)transform.position).normalized;

        // Determine side of contact
        float xSign = Mathf.Sign(rawDirection.x);

        // Build dynamic bias based on horizontalBias slider
        float xBias = Mathf.Lerp(0f, 1f, horizontalBias); // 0 = vertical, 1 = horizontal
        float yBias = Mathf.Lerp(1f, 0f, horizontalBias); // inverse

        Vector2 dynamicBias = new Vector2(xSign * xBias, yBias).normalized;

        // Blend raw direction with bias
        Vector2 finalDirection = Vector2.Lerp(rawDirection, dynamicBias, squishFactor).normalized;

        // Apply bounce
        rb.linearVelocity += finalDirection * bounceForce;



        // Optional: tell the player script (for camera, drag, VFX etc.)
        var playerScript = collision.gameObject.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.TriggerBounce(); // Optional cooldown/etc
        }

    }
    public Action onDestroyed; //Other scripts can attatch behaviour to this method call.

    void OnDestroy()
    {
        if (onDestroyed != null) {  //Avoid nullpointerexception
            onDestroyed.Invoke(); //Essentially "Emit signal"
        }
    }
}