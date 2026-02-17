using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isPressed = false;
    public bool Rotate;
    public bool doorOpen;
    public bool platformToggle;
    public bool platformMove;
    public GameObject effector;
    public SpriteRenderer effectorSprite;
    public BoxCollider2D effectorCollider;
    public PlatformMover PlatformMover;

    private Vector3 GetEffectorBottomEdge(){
        // Get bounds from the sprite renderer or collider to find the bottom edge
        if(effectorSprite != null){
            Bounds bounds = effectorSprite.bounds;
            return new Vector3(bounds.center.x, bounds.min.y, 0);
        }
        else if(effectorCollider != null){
            Bounds bounds = effectorCollider.bounds;
            return new Vector3(bounds.center.x, bounds.min.y, 0);
        }
        // Fallback to object position if no renderer/collider
        return effector.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other){
        isPressed = true;
        if(Rotate){
            Vector3 bottomEdge = GetEffectorBottomEdge();
            effector.transform.RotateAround(bottomEdge, Vector3.back, -90);
        }
        else if(doorOpen){
            if(effectorSprite != null){
                effectorSprite.color = Color.grey;
                effectorCollider.enabled = false;
            }
        }
        else if(platformToggle){
            if(effectorCollider != null){
                effectorCollider.enabled = !effectorCollider.enabled;
            }
        }
        else if (platformMove){
            if(PlatformMover != null){
                PlatformMover.isMoving = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        isPressed = false;
        if(doorOpen){
            if(effectorSprite != null){
                effectorSprite.color = Color.yellow;
                effectorCollider.enabled = true;
            }
        }
        else if (platformMove){
            if(PlatformMover != null){
                PlatformMover.isMoving = true;
            }
        }
    }

}
