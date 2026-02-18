using UnityEngine;
using UnityEngine.UI;
public class SigilDrawer: MonoBehaviour
{
    // will add the others once finished
    [SerializeField] private GameObject phantomBox;
    [SerializeField] private GameObject phantomTrash;
    [SerializeField] private GameObject phantomSpring;
    [SerializeField] private GameObject phantomGrapple;

    const int gridHeight = 15;
    const int gridWidth = 21;
    bool[,] grid = new bool[gridWidth, gridHeight];
    bool[,] boxGrid = new bool[gridWidth, gridHeight];
    bool[,] springGrid = new bool[gridWidth, gridHeight];
    bool[,] grappleGrid = new bool[gridWidth, gridHeight];
    public UiManager UiManager;
    public PrintController PrintController;
    public GameManager garry;
    void Start(){
        // game manager turns movement off
        GameManager.Gary.currentState = GameState.SnakeGame;
        
        //box grid initialization
        for(int i=10; i<gridWidth; i += 10){
            for(int e=7; e<gridHeight; e++){
                boxGrid[i,e] = true;
        }
        }
        for(int i=10; i<gridWidth; i ++){
            boxGrid[i,7] = true;
            boxGrid[i, 14] = true;
        }
        //spring grid initialization
        for(int i = 10; i<gridWidth; i++){
            springGrid[i,7] = true;
            springGrid[i,9] = true;
            springGrid[i,11] = true;
        }
        for(int i=0; i<11; i++){
            springGrid[i,9] = true;
            springGrid[i,11] = true;
        }
        springGrid[0,10] = true;
        springGrid[20,8]=true;
        //grapple grid initialization (GOD WHEN WILL IT END?)
        for(int i = 0; i <7; i++){
            grappleGrid[i,0] = true;
            grappleGrid[i,4] = true;
        }
        grappleGrid[0,1] = true;
        grappleGrid[0,2] = true;
        grappleGrid[0,3] = true;
        grappleGrid[6,1] = true;
        grappleGrid[6,2] = true;
        grappleGrid[6,3] = true;
        for(int i=7; i<10; i++){
            grappleGrid[i,4] = true;
        }
        for(int i=10; i<16; i++){
            grappleGrid[i,4] = true;
            grappleGrid[i,7] = true;
        }
        grappleGrid[15,5] = true;
        grappleGrid[15,6] = true;
    }
    //When the time comes, add the ability to set a starting location for the head
    public void StartPrinting(){
        PrintController.resetPrinterHead();
        PrintController.CalculateOffset();
        PrintController.isDrawing = true;
    }
    public void CompareDrawing(){
        //compare the drawing to the preset print codes
        bool isBox = true;
        bool isSpring = true;
        bool isGrapple = true;
        for(int i=0; i<gridWidth; i ++){
            for(int e=0; e<gridHeight; e++){
                if(boxGrid[i,e] != grid[i,e]){
                    isBox = false;
                    Debug.Log(i+","+e+" is wrong for Box");
                }
                if(springGrid[i,e] != grid[i,e]){
                    isSpring = false;
                }
                if(grappleGrid[i,e] != grid[i,e]){
                    isGrapple = false;
                }
            }
        }
        if(isBox){
            CompleteSigil(1);
        }
        else if(isSpring){
            CompleteSigil(2);
        }
        else if(isGrapple){
            CompleteSigil(3);
        }
        else{
            CompleteSigil(0);
        }
    }
    public void RecordCell(Vector2 gridPosition){
        int x = Mathf.RoundToInt((5.0f + gridPosition.x) * 2.0f);
        int y = Mathf.RoundToInt((3.5f + (gridPosition.y * -1.0f)) * 2.0f);
        grid[x, y]=true;
    }
    private void CompleteSigil(int objectNumber){
        Debug.Log("objectNumber: " + objectNumber);
        StopPrinting();
        UiManager.closePrinter();
        if(objectNumber == 1){
            // create box
            if(garry.filament >= 3){
                GameObject box = Instantiate(phantomBox);
                garry.substractFilament(3);

                // sound
                SoundManager.Sherry.MakeSuccessSound();
            }
            else{
                garry.showErrorMessage();

                // sound
                SoundManager.Sherry.MakeFailureSound();
            }

        }
        else if(objectNumber == 2){
            if(garry.filament >= 2){
                GameObject spring = Instantiate(phantomSpring);
                garry.substractFilament(2);

                // sound
                SoundManager.Sherry.MakeSuccessSound();
            }
            else{
                garry.showErrorMessage();

                // sound
                SoundManager.Sherry.MakeFailureSound();
            }
        }
        else if(objectNumber == 3){
            if(garry.filament >= 4){
                GameObject grapple = Instantiate(phantomGrapple);
                garry.substractFilament(4);

                // sound
                SoundManager.Sherry.MakeSuccessSound();
            }
            else{
                garry.showErrorMessage();

                // sound
                SoundManager.Sherry.MakeFailureSound();
            }
        }
        else{
            // create trash
            if(garry.filament >= 1){
                GameObject trash = Instantiate(phantomTrash);
                garry.substractFilament(1);

                // sound
                SoundManager.Sherry.MakeFailureSound();
            }
            else{
                garry.showErrorMessage();

                // sound
                SoundManager.Sherry.MakeFailureSound();
            }

            garry.UpdateUI();
        }

        // allow player to move again
        GameManager.Gary.currentState = GameState.Playing;

    }
    public void StopPrinting(){
        foreach (var obj in GameObject.FindGameObjectsWithTag("PrintPrefab")){
        Destroy(obj);
        }
        grid = new bool[gridWidth, gridHeight];
        PrintController.resetPrinterHead();
        UiManager.closePrinter();
    }
}

