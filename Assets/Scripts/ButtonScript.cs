using System.Collections;
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
    public Collider2D effectorCollider;
    public GameObject PlatformMover;
    public GameObject Player;

    private Vector3 GetEffectorBottomEdge(){
        // make sure it's interactable with the button
        if (effector.CompareTag("Box") || effector.CompareTag("Trash"))
        {
            // Get bounds from the sprite renderer or collider to find the bottom edge
            if(effectorSprite != null){
                Bounds bounds = effectorSprite.bounds;
                return new Vector3(bounds.center.x, bounds.min.y, 0);
            }
            else if(effectorCollider != null){
                Bounds bounds = effectorCollider.bounds;
                return new Vector3(bounds.center.x, bounds.min.y, 0);
            }

        }

        // Fallback to object position if no renderer/collider
        return effector.transform.position;        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Box") || other.CompareTag("Trash"))
        {
            isPressed = true;
            if(Rotate){
                print("1");
                Vector3 bottomEdge = GetEffectorBottomEdge();
                effector.transform.RotateAround(bottomEdge, Vector3.back, -90);
            }
            else if(doorOpen){
                print("2");
                if(effectorSprite != null){
                    effectorSprite.color = Color.grey;
                    effectorCollider.enabled = false;
                }
            }
            else if(platformToggle){
                if (effector.gameObject.activeSelf)
                {
                    effector.gameObject.SetActive(false);
                    print("deactivated");
                } else
                {
                    effector.gameObject.SetActive(true);
                    print("activated");
                }
            }
            else if (platformMove){
                if(PlatformMover != null){
                    // PlatformMover.isMoving = true;
                }
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Box") || other.CompareTag("Trash"))
        {
            isPressed = false;
            if(doorOpen){
                if(effectorSprite != null){
                    effectorSprite.color = Color.yellow;
                    effectorCollider.enabled = true;
                }
            }
            else if (platformMove){
                if(PlatformMover != null){
                    Player.GetComponent<Transform>().position = PlatformMover.transform.position;
                }

            }
        }

    }

}
