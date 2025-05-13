using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 30f;
    public float turn_threshold = 4.5f;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;


    private float moveX;
    private void Update()
    {
        // Get horizontal input and move (Only for testing purposes!)
        moveX = Input.GetAxisRaw("Horizontal");


        // Flip sprite based on direction & input
        if (rb.linearVelocity.x < -turn_threshold)
            spriteRenderer.flipX = true;
        else if (rb.linearVelocity.x > turn_threshold)
            spriteRenderer.flipX = false;
        else if (moveX < -0.1f)
            spriteRenderer.flipX = true;
        else if (moveX > 0.1f)
            spriteRenderer.flipX = false;


        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //Reset if out of bounds
        if (transform.position.y < -10) {
            transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
            rb.linearVelocity = Vector2.zero;
        }
    }

    [SerializeField] float xDamp = 0.98f; //Descelleration
    [SerializeField] float bounceCooldown = 0.2f;
    private float bounceTimer = 0f;

    void FixedUpdate()
    {
        if (Mathf.Abs(moveX) > 0.01f) // only apply when player gives input
        {
            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        }

        if (bounceTimer > 0f)
        {
            bounceTimer -= Time.fixedDeltaTime;
            return; // skip damping right after bounce
        }

        Vector2 v = rb.linearVelocity;
        v.x *= xDamp;
        rb.linearVelocity = v;
    }

    public void TriggerBounce()
    {
        bounceTimer = bounceCooldown;
    }

}
