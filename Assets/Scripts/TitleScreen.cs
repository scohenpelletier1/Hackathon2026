using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScne : MonoBehaviour
{
    public AudioSource backgroundMusic;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
