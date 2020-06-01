using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMPro.TMP_Dropdown graphicsDropdown;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    public int _resolutionIndex;
    public bool _isFullscreen;
    public int _qualityIndex;
    public float _musicVolume;
    public float _effectsVolume;

    public AudioMixer audioMixer;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();

        bool isFullscreen = Convert.ToBoolean(PlayerPrefs.GetInt("IsFullscreen", 1));
        if (isFullscreen)
        {
            _isFullscreen = true;
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }

        graphicsDropdown.value = PlayerPrefs.GetInt("QualityIndex", 0);
        graphicsDropdown.RefreshShownValue();

        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);

        effectVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0);
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("ResolutionIndex", _resolutionIndex);

        int isFullscreen = Convert.ToInt32(_isFullscreen);
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen);

        PlayerPrefs.SetInt("QualityIndex", _qualityIndex);

        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);

        PlayerPrefs.SetFloat("EffectsVolume", _effectsVolume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        _resolutionIndex = resolutionIndex;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        _isFullscreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _qualityIndex = qualityIndex;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        _musicVolume = volume;
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", volume);
        _effectsVolume = volume;
    }
}