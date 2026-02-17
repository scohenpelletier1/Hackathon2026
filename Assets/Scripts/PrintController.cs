using UnityEngine;
public class PrintController : MonoBehaviour
{
    [SerializeField] private SigilDrawer sigilDrawer;
    [SerializeField] private GameObject sigilParent;
    public GameObject printPrefab;
    public float gridSize = 1f;
    public float moveInterval = 0.2f; // seconds between moves
    private bool moveLock = false;
    public bool isDrawing = false;
    private Vector2 direction = Vector2.right;
    private Vector3 prefabPosition;
    private float moveTimer;
    private Vector2 offset;
    
    void Update()
    {
        HandleInput();
        
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval && isDrawing)
        {
            moveTimer = 0f;
            Move();
            moveLock = false;
        }
    }
    public void CalculateOffset(){
        offset = (Vector2) transform.position;
        Debug.Log(offset);
    }
    
    void HandleInput()
    {
        // Prevent 180-degree turns
        //TODO: potentially add input buffering
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down && !moveLock){
            moveLock = true;
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up && !moveLock){
            moveLock = true;
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right && !moveLock){
            moveLock = true;
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left && !moveLock){
            moveLock = true;
            direction = Vector2.right;
        }
    }
    //add log position for shape tracking in this method
    void Move()
    {
        prefabPosition = transform.position;
        transform.position += (Vector3)(direction * gridSize);
        Vector2 prefabGridPosition = new Vector2(prefabPosition.x-offset.x, prefabPosition.y-offset.y);
        sigilDrawer.RecordCell(prefabGridPosition);
        Instantiate(printPrefab, prefabPosition, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D other){
        sigilDrawer.CompareDrawing();
    }
    public void resetPrinterHead(){
        isDrawing = false;
        transform.position = sigilParent.transform.position;
        direction = Vector2.right;
    }
}