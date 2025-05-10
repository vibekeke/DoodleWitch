using UnityEngine;
using System;

public class Bubble : MonoBehaviour
{
    public float bounceForce = 10f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Debug.Log("Bubble is born");
         Destroy(gameObject, 2f);
    }


    [Tooltip("0 = pure reflection, 1 = fully biased direction")]
    [Range(0f, 1f)]
    [SerializeField] private float squishFactor = 0.35f;

    [Tooltip("Forward-upward nudge to soften bounce direction")]
    [SerializeField] private Vector2 bounceBias = new Vector2(0.5f, 0.5f);

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Get the point of contact
        ContactPoint2D contact = collision.GetContact(0);

        // Raw bounce direction: from bubble center to contact point
        Vector2 rawDirection = (contact.point - (Vector2)transform.position).normalized;

        // Blend in a little forward-upward squish
        Vector2 biasedDirection = Vector2.Lerp(rawDirection, bounceBias.normalized, squishFactor).normalized;

        // Apply bounce
        rb.linearVelocity = biasedDirection * bounceForce;

        Debug.Log($"SquishBounce → Raw: {rawDirection}, Final: {biasedDirection}");


        // Optional: tell the player script (for camera, drag, VFX etc.)
        var playerScript = collision.gameObject.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            playerScript.TriggerBounce(); // Optional cooldown/etc
        }

        Destroy(gameObject);
    }
    public Action onDestroyed; //Other scripts can attatch behaviour to this method call.

    void OnDestroy()
    {
        if (onDestroyed != null) {  //Avoid nullpointerexception
            onDestroyed.Invoke(); //Essentially "Emit signal"
        }
    }
}