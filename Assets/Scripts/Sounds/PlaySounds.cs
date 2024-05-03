using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    protected AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySoundSoundManager(AudioClip audioClip, float pitch, float volume)
    {
        SoundManager.Instance.EjecutarAudio(audioClip, pitch, volume);
    }

    public void PlaySoundLocalAudioSource(AudioClip audioClip, float pitch, float volume)
    {
        audioSource.PlayOneShot(audioClip);
        audioSource.pitch = pitch;
        audioSource.volume = volume;
    }
}
