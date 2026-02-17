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
    
}
