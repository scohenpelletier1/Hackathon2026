using UnityEngine;

public class STLScript: MonoBehaviour
{
    public bool isBox, isSpring, isGrapple;

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player"))
        {            
            if(isBox){
                GameManager.Gary.GetComponent<GameManager>().foundBox = true;
            }
            else if(isSpring){
                GameManager.Gary.GetComponent<GameManager>().foundSpring = true;
            }
            else if(isGrapple){
                GameManager.Gary.GetComponent<GameManager>().foundGrapple = true;
            }

            gameObject.SetActive(false);
            
        }

    }

}
