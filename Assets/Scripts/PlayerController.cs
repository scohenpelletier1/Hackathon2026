using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody2D rb;

    public float speed, jumpForce, groundCheckRadius;
    public bool isGrounded, isLeft;
    public Vector3 groundCheckPosition;
    public LayerMask groundLayers;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get the rigidbody
        rb = GetComponent<Rigidbody2D>();
        isLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

    }

    void FixedUpdate() {
        // ground check

        // reset grounded
        isGrounded = false;

        // check the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + groundCheckPosition, groundCheckRadius, groundLayers);

        // are there any results
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }

    }

    private void MovePlayer() {
        // get the horizontal input
        float xValue = Input.GetAxis("Horizontal");

        // set the velocity
        rb.linearVelocityX = xValue * speed;

        // change direction of idle based on where the player moved
        if (rb.linearVelocityX < 0)
        {
            animator.SetBool("isLeft", true);
            animator.Play("PlayerLeftIdle");
            isLeft = true;

        } else if (rb.linearVelocityX > 0)
        {
            animator.SetBool("isLeft", false);
            animator.Play("PlayerRightIdle");
            isLeft = false;
        }

        // are they jumping?
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // we are jumping
            rb.linearVelocityY = jumpForce;
            isGrounded = false;
        }

    }

    void OnDrawGizmos() {
        if (isGrounded) 
        {
            Gizmos.color = Color.red;

        } else
        {
            Gizmos.color = Color.cyan;
        }

        // draw the circle
        Gizmos.DrawWireSphere(transform.position + groundCheckPosition, groundCheckRadius);

    }

}
