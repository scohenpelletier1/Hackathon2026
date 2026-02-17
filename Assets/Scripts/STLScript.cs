using UnityEngine;

public class STLScript: MonoBehaviour
{
    public bool isBox = false;
    public bool isSpring = false;
    public bool isGrapple = false;
    public GameManager garry;
    void OnTriggerEnter2D(Collider2D other){
        if(isBox){
            garry.foundBox = true;
        }
        else if(isSpring){
            garry.foundSpring = true;
        }
        else if(isGrapple){
            garry.foundGrapple = true;
        }
        gameObject.SetActive(false);
    }
}
