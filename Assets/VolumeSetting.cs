using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider masterSlider;
    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("sfxVolume"))
        LoadVolume();
        else
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music",volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx",volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master",volume);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value=PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value=PlayerPrefs.GetFloat("sfxVolume");
        masterSlider.value=PlayerPrefs.GetFloat("masterVolume");
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }
}
