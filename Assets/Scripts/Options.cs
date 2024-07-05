using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider sfxVolumeSlider;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        musicVolumeSlider.value = GameManager.Instance.options.MusicVolume;
        sfxVolumeSlider.value = GameManager.Instance.options.SfxVolume;
    }

    public void OnMusicVolumeChanged()
    {
        GameManager.Instance.options.MusicVolume = musicVolumeSlider.value;
        GameManager.Instance.SaveOptions();
    }

    public void OnSFXVolumeChanged()
    {
        GameManager.Instance.options.SfxVolume = sfxVolumeSlider.value;
        GameManager.Instance.SaveOptions();
    }
}
