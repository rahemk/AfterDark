using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        // Load the saved volume level
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        volumeSlider.value = savedVolume;
        
        // Set the initial volume level
        AudioListener.volume = savedVolume;
    }

    public void OnVolumeChanged()
    {
        // Update the volume level
        float volume = volumeSlider.value;
        AudioListener.volume = volume;

        // Save the volume level for future use
        PlayerPrefs.SetFloat("volume", volume);
    }
}
