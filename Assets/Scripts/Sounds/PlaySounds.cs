using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public AudioClip audioClip;

    public void PlaySound()
    {
        SoundManager.Instance.EjecutarAudio(audioClip, 1.2f, 0.2f);
    }
}
