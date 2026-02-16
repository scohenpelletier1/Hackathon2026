using UnityEngine;
public class UiManager : MonoBehaviour{
    [SerializeField] private GameObject sigilParent;
    //Call this function when you hit the print button
    public void openPrinter(){
        sigilParent.SetActive(true);
    }
    //we can move this into the character controller when the time comes
    public void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            openPrinter();
        }
         if(Input.GetKeyDown(KeyCode.Escape)){
            closePrinter();
        }
    }
    public void closePrinter(){
        sigilParent.SetActive(false);
    }
}