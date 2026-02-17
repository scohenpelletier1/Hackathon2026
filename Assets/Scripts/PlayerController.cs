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
    public KeyCode keyToDetect = KeyCode.Q; // The key you want to detect
    public float requiredHoldTime = 2.0f; // The required hold duration in seconds
    private float holdTimer = 0.0f;
    private bool heldLongEnough = false;
    public GameManager garry;

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
        // only gets to move if playing
        if (GameManager.Gary.currentState == GameState.Playing)
        {
            MovePlayer();
        }
        // Check if the key is currently held down
        if (Input.GetKey(keyToDetect))
        {
            // If it is, increment the timer by the time passed since the last frame
            holdTimer += Time.deltaTime;

            // Check if the timer has reached the required time and the action hasn't already triggered
            if (holdTimer >= requiredHoldTime && !heldLongEnough)
            {
                Debug.Log("Key held for " + requiredHoldTime + " seconds!");
                heldLongEnough = true;
                Laser();
            }
        }
        else
        {
            // If the key is released, reset the timer and the flag
            holdTimer = 0.0f;
            heldLongEnough = false;
        }

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
            animator.SetBool("isGrounded", true);
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
            animator.SetBool("Running", true);
            animator.Play("PlayerRunLeft");
            isLeft = true;

        } else if (rb.linearVelocityX > 0)
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("Running", true);
            animator.Play("PlayerRunRight");
            isLeft = false;

        } else if (rb.linearVelocityX == 0)
        {
            animator.SetBool("Running", false);

        }

        // are they jumping?
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // we are jumping
            rb.linearVelocityY = jumpForce;
            isGrounded = false;
            animator.SetBool("isGrounded", false);

            if (isLeft)
            {
                animator.Play("PlayerJumpLeft");

            } else
            {
                animator.Play("PlayerJumpRight");
            }

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
    public void Laser(){
        Vector2 direction = isLeft ? Vector2.left : Vector2.right;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100f, LayerMask.GetMask("InteractableObjects"));
        
        if(hit.collider != null){
            Debug.Log("Hit object: " + hit.collider.gameObject.name + " on layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            GameObject target = hit.collider.gameObject;
            if (target.CompareTag("Spring")){
                garry.filament += 2;
            }
            if (target.CompareTag("Grapple")){
                garry.filament += 4;
            }
            if (target.CompareTag("Box")){
                garry.filament += 3;
            }
            if (target.CompareTag("Trash")){
                garry.filament += 1;
            }
            garry.UpdateUI();
            Destroy(target);
        } else {
            Debug.Log("Laser hit nothing! Check that objects are on 'InteractableObjects' layer and have Collider2D");
        }
    }

}
