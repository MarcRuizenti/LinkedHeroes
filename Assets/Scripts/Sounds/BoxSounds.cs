using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSounds : PlaySounds
{
    [SerializeField] private AudioClip[] destroyBoxSounds;
    [SerializeField] private AudioClip[] spawnBoxSounds;
    [SerializeField] private AudioClip[] hitBoxSounds;

    public void PlaySoundDestroyBox(float pitch, float volume)
    {
        PlaySoundSoundManager(destroyBoxSounds[Random.Range(0, destroyBoxSounds.Length)], pitch, volume);
    }

    public void PlaySoundSpawnBox(float pitch, float volume)
    {
        PlaySoundSoundManager(spawnBoxSounds[Random.Range(0, spawnBoxSounds.Length)], pitch, volume);
    }

    public void PlaySoundHitBox(float pitch, float volume)
    {
        PlaySoundSoundManager(hitBoxSounds[Random.Range(0, hitBoxSounds.Length)], pitch, volume);
    }
}
