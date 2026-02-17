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
    }

    // Update is called once per frame
    void Update()
    {
        // didn't delete bc we might put something in here later
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.CompareTag("Spring") && !collision.gameObject.GetComponent<PlayerController>().isGrounded)
        {
            print("Hiiiiiiiiiiiiiii");
            GameObject player = collision.gameObject;

            // if the collider is a player, the object IS a spring, and the player is standing on top of the spring
            player.GetComponent<Rigidbody2D>().linearVelocityY = player.GetComponent<PlayerController>().jumpForce * 2;

        } else if (collision.gameObject.CompareTag("Player"))
        {
            // if the collider is a player and the object is not a spring
            print("player touched me");
            isTouchingPlayer = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // after player leaves
            print("player stopped touching me");
            rb.linearVelocityX = 0;
            isTouchingPlayer = false;
        }

    }

}
