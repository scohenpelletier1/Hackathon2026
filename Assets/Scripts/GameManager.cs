using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { None, Playing, SnakeGame }

public class GameManager : MonoBehaviour
{
    public static GameManager Gary;
    public GameState currentState = GameState.None;
    public int filament;
    public Image filamentImage;
    public Canvas canvas;
    public GameObject errorMessage;
    public Image STLScreen;
    public Image BoxScreen;
    public Image SpringScreen;
    public Image GrappleScreen;
    public bool foundBox = false;
    public bool foundSpring = false;
    public bool foundGrapple = false;

    void Awake() {
        // check for the singleton
        if (Gary) {
            // kill the fake gary
            Destroy(this.gameObject);

        } else {
            // i am gary
            Gary = this;

            // don't destroy gary please
            DontDestroyOnLoad(this.gameObject);

            // set my initial gamestate
            currentState = GameState.Playing;

        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // set the starting UI values
        filament = 6;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addFilament(int addAmount) {
        // update score amount
        filament += addAmount;
        UpdateUI();

    }

    public void UpdateUI() {
        // update all UI values
        int filamentImageCount = GameObject.FindGameObjectsWithTag("FilamentImage").Length;
        // for filament
        if(filamentImageCount < filament){
            for (int i = 1; i < filament; i++)
            {
                filamentImage = GameObject.Instantiate(filamentImage);
                filamentImage.transform.SetParent(canvas.transform, false);
                filamentImage.rectTransform.anchoredPosition = new Vector3(-370 + (30 * i), 190, 0);
            }
        }
        else{
            for (int i = filamentImageCount; i > filamentImageCount - (filamentImageCount - filament); i--)
            {
                Debug.Log(filamentImageCount-filament);
                Destroy(GameObject.FindGameObjectsWithTag("FilamentImage")[i-1]);
            }
        }
    }
    public void showErrorMessage(){
        StartCoroutine(ShowErrorCoroutine());
    }

    private IEnumerator ShowErrorCoroutine(){
        errorMessage.SetActive(true);
        yield return new WaitForSeconds(3f);
        errorMessage.SetActive(false);
    }
    public void showSTLScreen(){
        // Delegate to Gary singleton if this is not Gary
        if (this != Gary && Gary != null) {
            Gary.showSTLScreen();
            return;
        }
        STLScreen.gameObject.SetActive(true);
        if(foundBox){
            BoxScreen.gameObject.SetActive(true);
        }
        else{
            BoxScreen.gameObject.SetActive(false);
        }
        if(foundSpring){
            SpringScreen.gameObject.SetActive(true);
        }
        else{
            SpringScreen.gameObject.SetActive(false);
        }
        if(foundGrapple){
            GrappleScreen.gameObject.SetActive(true);
        }
        else{
            GrappleScreen.gameObject.SetActive(false);
        }
        
    }
    public void hideSTLScreen(){
        // Delegate to Gary singleton if this is not Gary
        if (this != Gary && Gary != null) {
            Gary.hideSTLScreen();
            return;
        }
        STLScreen.gameObject.SetActive(false);
        BoxScreen.gameObject.SetActive(false);
        SpringScreen.gameObject.SetActive(false);
        GrappleScreen.gameObject.SetActive(false);
    }

    
}
