using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider musicVolumeSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            musicVolumeSlider.value = musicSource.volume;
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
            musicVolumeSlider.value = 0.5f;
        }
    }

    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
    }
}
