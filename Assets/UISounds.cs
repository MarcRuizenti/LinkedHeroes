using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] private AudioClip click;

    public void SoundClick()
    {
        SoundManager.Instance.EjecutarAudio(click, 1, 0.2f);
    }
}
