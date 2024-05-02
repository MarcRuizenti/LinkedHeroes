using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLisenerMusic : AudioSlider
{
    void Start()
    {
        SetVolumeMusic(PlayerPrefs.GetFloat("musicVolume", 100));
    }

    private void SetVolumeMusic(float value)
    {
        if (value < 1) value = 0.001f;

        RefreshSlider(value);
        PlayerPrefs.SetFloat("musicVolume", value);
        _masterMixer.SetFloat("musica", Mathf.Log10(value / 100) * 20f);
    }

    public void SetValueFromSliderMusic()
    {
        SetVolumeMusic(_soundSlider.value);
    }
}
