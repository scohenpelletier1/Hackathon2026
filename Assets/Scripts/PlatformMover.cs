using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Vector3 PlatformDestination;
    public Vector3 PlatformStart;
    public float moveSpeed;
    public bool isMoving;
    void Start(){
        transform.position = PlatformStart;
    }
    void Update(){
        if(isMoving){
            transform.Translate(Vector3.MoveTowards(PlatformStart, PlatformDestination, moveSpeed * Time.deltaTime));
        }
    }
}
