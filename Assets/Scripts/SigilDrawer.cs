using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
public class SigilDrawer: MonoBehaviour
{
    // will add the others once finished
    [SerializeField] private GameObject phantomBox;
    [SerializeField] private GameObject phantomTrash;

    const int gridHeight = 15;
    const int gridWidth = 21;
    bool[,] grid = new bool[gridWidth, gridHeight];
    bool[,] boxGrid = new bool[gridWidth, gridHeight];
    bool[,] springGrid = new bool[gridWidth, gridHeight];
    bool[,] grappleGrid = new bool[gridWidth, gridHeight];
    public UiManager UiManager;
    public PrintController PrintController;
    void Start(){
        //box grid initialization
        for(int i=10; i<gridWidth; i += 10){
            for(int e=7; e<gridHeight; e++){
                boxGrid[i,e] = true;
        }
        }
        for(int i=11; i<gridWidth; i ++){
            boxGrid[i,7] = true;
            boxGrid[i, 14] = true;

        }
    }
    //When the time comes, add the ability to set a starting location for the head
    public void StartPrinting(){
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
        int x =(int) ((5+(gridPosition.x))*2);
        int y =(int) ((3.5 + (gridPosition.y*-1))*2);
        grid[x, y]=true;

    }
    private void CompleteSigil(int objectNumber){
        Debug.Log("objectNumber: " + objectNumber);
    if(objectNumber == 1){
        Debug.Log("Box");
        GameObject box = Instantiate(phantomBox);

    }
    else if(objectNumber == 2){
        Debug.Log("Spring");
    }
    else if(objectNumber == 3){
        Debug.Log("Grapple");
    }
    else{
        Debug.Log("Trash");
        GameObject trash = Instantiate(phantomTrash);
    }
    StopPrinting();
    UiManager.closePrinter();
    }
    public void StopPrinting(){
        foreach (var obj in GameObject.FindGameObjectsWithTag("PrintPrefab")){
        Destroy(obj);
    }
        bool[,] grid = new bool[gridWidth, gridHeight];
        PrintController.resetPrinterHead();
    }
}
