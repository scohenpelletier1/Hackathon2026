using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isTouchingPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the rigidbody, set type
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        // didn't delete bc we might put something in here later
        
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // if the collider is a player, change to dynamic
            print("player touched me");
            rb.bodyType = RigidbodyType2D.Dynamic;
            isTouchingPlayer = true;

            // give it the same velocity as the player to reduce jitter
            rb.linearVelocityX = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocityX;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // after player leaves, go back to kinematic
            print("player stopped touching me");
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocityX = 0;
            isTouchingPlayer = false;
        }

    }

}
