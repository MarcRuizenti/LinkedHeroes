using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPlayer : MonoBehaviour
{
    public AudioClip[] stepsGrass;

    public void PlaySteps()
    {
        AudioClip stepGrass = stepsGrass[Random.Range(0, stepsGrass.Length)];
        SoundManager.Instance.EjecutarAudio(stepGrass, 1, 0.1f);
    }
}
