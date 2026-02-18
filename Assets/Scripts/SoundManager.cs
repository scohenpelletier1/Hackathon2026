using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource localAudio;

    public static SoundManager Sherry;
    public AudioClip springSound, jumpSound, placeObjectSound, buttonSound, openDoorSound, closeDoorSound, filamentSound, failureSound, successSound;
    public AudioSource backgroundMusic;

    void Awake() {
        // im the singleton
        Sherry = this;

        // set up my audiosources
        localAudio = GetComponent<AudioSource>();
        backgroundMusic.Play();

    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeSpringSound() {
        localAudio.PlayOneShot(springSound);

    }

    public void MakeJumpSound() {
        localAudio.PlayOneShot(jumpSound);

    }

    public void MakePlaceObjectSound() {
        localAudio.PlayOneShot(placeObjectSound);

    }

    public void MakeButtonSound() {
        localAudio.PlayOneShot(buttonSound);
  
    }

    public void MakeOpenDoorSound()
    {
        localAudio.PlayOneShot(openDoorSound);
    }

    public void MakeCloseDoorSound()
    {
        localAudio.PlayOneShot(closeDoorSound);
    }

    public void MakeFilamentSound()
    {
        localAudio.PlayOneShot(filamentSound);
    }

    public void MakeFailureSound()
    {
        localAudio.PlayOneShot(failureSound);
    }

    public void MakeSuccessSound()
    {
        localAudio.PlayOneShot(successSound);
    }

}
