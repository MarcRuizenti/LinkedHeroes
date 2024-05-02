using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLisenerSFX : AudioSlider
{
    void Start()
    {
        SetVolumeSFX(PlayerPrefs.GetFloat("sfxVolume", 100));
    }

    private void SetVolumeSFX(float value)
    {
        if (value < 1) value = 0.001f;

        RefreshSlider(value);
        PlayerPrefs.SetFloat("sfxVolume", value);
        _masterMixer.SetFloat("sfx", Mathf.Log10(value / 100) * 20f);
    }

    public void SetValueFromSliderSFX()
    {
        SetVolumeSFX(_soundSlider.value);
    }
}
