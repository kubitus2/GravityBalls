using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI : MonoBehaviour
{
    [SerializeField]
    private Text counter;
    [SerializeField]
    private BallSpawner spawner;
    [SerializeField]
    private AudioMixer am;
    [SerializeField]
    private Text muteButtonText;

    private bool isMuted;
    private float currentVolume;

    void Awake()
    {
        isMuted = false;
        am.GetFloat("MasterVol", out currentVolume);
    }

    void Update()
    {
        DisplayText();
        
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit(0);
        }
    }

    void DisplayText()
    {
        counter.text = string.Format("Balls created so far: {0}", spawner.numOfBalls);
    }

    public void ToggleMute()
    {
        float volume = 0f;

        isMuted = !isMuted;

        if(isMuted)
        {
            volume = -80.0f;
            muteButtonText.text = "Unmute";
        }
        else
        {
            volume = currentVolume;
            muteButtonText.text = "Mute";
        }

        am.SetFloat("MasterVol", volume);
    }
}
