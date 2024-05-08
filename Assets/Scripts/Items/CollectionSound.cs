using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSound : PlaySounds
{
    [SerializeField] private AudioClip krokurCollection;
    [SerializeField] private AudioClip aikeCollection;
    

    public void PlayKrokurCollection(float pitch, float volume)
    {
        PlaySoundSoundManager(krokurCollection, pitch, volume);
    }

    public void PlayAikeCollection(float pitch, float volume)
    {
        PlaySoundSoundManager(krokurCollection, pitch, volume);
    }
}
