using UnityEngine;
using UnityEngine.UI;

public class TurnOnOffSound : MonoBehaviour
{
    public Sprite turnOnSprite;
    public Sprite turnOffSprite;
    public AudioSource clickSound;
    public AudioSource backgroundMusic;

    public Image image;

    void Start()
    {
        AudioListener.pause = false;
        backgroundMusic.Play();
    }

    public void ChangeImage()
    {
        clickSound.Play();

        if (AudioListener.pause)
        {
            image.sprite = turnOnSprite;
            AudioListener.pause = false;
            backgroundMusic.Play();
        }
        else
        {
            image.sprite = turnOffSprite;
            backgroundMusic.Pause();
            AudioListener.pause = true;
        }
    }
}
