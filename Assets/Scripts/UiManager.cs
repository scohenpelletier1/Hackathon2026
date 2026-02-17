using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour{
    [SerializeField] private SigilDrawer SigilDrawer;
    [SerializeField] private Canvas canvas;

    public GameObject sigilParent;
    //Call this function when you hit the print button
    public void openPrinter(){
        sigilParent.SetActive(true);
        SigilDrawer.StartPrinting();

    }

    //we can move this into the character controller when the time comes
    public void Update(){
        if(Input.GetKeyDown(KeyCode.Q)){
            openPrinter();

        }
         if(Input.GetKeyDown(KeyCode.Escape)){
            SigilDrawer.StopPrinting();

        }
    }

    public void closePrinter(){
        sigilParent.SetActive(false);
    }

}
