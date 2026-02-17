using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float pullSpeed = 25f;
    public float arrivalDistance = 0.5f;
    public LayerMask groundLayer;
    private Transform player;
    private Rigidbody2D playerRb;
    private bool isPulling = false;

    void Start()
    {
        // Find the player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerRb = playerObj.GetComponent<Rigidbody2D>();
            
            // Check line of sight and start pulling if visible
            if (HasLineOfSight())
            {
                StartPulling();
            }
        }
        else
        {
            Debug.LogWarning("GrappleHook: No player found with tag 'Player'");
        }
    }

    void Update()
    {
        if (isPulling && player != null)
        {
            PullPlayer();
        }
    }

    bool HasLineOfSight()
    {
        if (player == null) return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Cast a ray from the grapple hook to the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, groundLayer);

        // If we didn't hit any obstacles, we have line of sight
        // Or if we hit the player directly
        if (hit.collider == null || hit.collider.CompareTag("Player"))
        {
            Debug.Log("GrappleHook: Line of sight confirmed!");
            return true;
        }

        Debug.Log("GrappleHook: Line of sight blocked by " + hit.collider.name);
        return false;
    }

    void StartPulling()
    {
        isPulling = true;
        Debug.Log("GrappleHook: Starting to pull player!");
    }

    void PullPlayer()
    {
        if (playerRb == null) return;

        Vector2 directionToHook = (transform.position - player.position).normalized;
        float distanceToHook = Vector2.Distance(transform.position, player.position);

        // Check if player has arrived
        if (distanceToHook <= arrivalDistance)
        {
            StopPulling();
            return;
        }

        // Rapidly pull the player towards the grapple hook
        playerRb.linearVelocity = directionToHook * pullSpeed;
    }

    void StopPulling()
    {
        isPulling = false;
        if (playerRb != null)
        {
            // Stop the player's momentum when they arrive
            playerRb.linearVelocity = Vector2.zero;
        }
        Debug.Log("GrappleHook: Player arrived!");
    }

}
