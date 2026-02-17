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
    void onTriggerEnter2D(Collider2D other){
        isPressed = true;
        if(Rotate){
            effector.transform.Rotate(0, 0, 90);
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
    void onTriggerExit2D(Collider2D other){
        isPressed = false;
        if(Rotate){
            effector.transform.Rotate(0, 0, -90);
        }
        else if(doorOpen){
            effectorSprite.color = Color.yellow;
            effectorCollider.enabled = true;
        }
        else if (platformMove){
            if(PlatformMover != null){
                PlatformMover.isMoving = true;
            }
        }
    }

}
