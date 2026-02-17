using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public GameObject teleporter;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Transform>().position = teleporter.GetComponent<Transform>().position;
        }

    }

}
