using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour{
    [SerializeField] private SigilDrawer SigilDrawer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject Player;
    public GameManager garry;

    public GameObject sigilParent;
    //Call this function when you hit the print button
    public void openPrinter(){
        garry.currentState = GameState.SnakeGame;
        SigilDrawer.transform.position = Player.transform.position;
        sigilParent.SetActive(true);
        SigilDrawer.StartPrinting();

    }

    //we can move this into the character controller when the time comes
    public void Update(){
        // if(Input.GetKeyDown(KeyCode.Q)){
        //     openPrinter();

        // }
         if(Input.GetKeyDown(KeyCode.Escape)){
            SigilDrawer.StopPrinting();

        }
    }

    public void closePrinter(){
        sigilParent.SetActive(false);
        garry.currentState = GameState.Playing;

    }

}
