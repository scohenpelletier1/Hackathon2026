using UnityEngine;
using UnityEngine.UI;
public class SigilDrawer: MonoBehaviour
{
    public void StartPrinting(){
        Time.timeScale = 0;
    }
    public void CompareDrawing(){
        //compare the drawing to the preset print codes
    }
    public void CompleteSigil(string objectName){
    if(objectName != null){
        //Paste object creation code here
    }
    foreach (var obj in GameObject.FindGameObjectsWithTag("PrintPrefab"))
    Destroy(obj);
    }
}
