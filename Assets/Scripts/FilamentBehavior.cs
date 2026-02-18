using UnityEngine;

public class FilamentBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            // add to number of filaments
            GameManager.Gary.addFilament(1);

            // sound
            SoundManager.Sherry.MakeFilamentSound();

            // destroy the filament
            Destroy(this.gameObject);

            // hide filament
            this.gameObject.SetActive(false);
        }
        
    }
}
