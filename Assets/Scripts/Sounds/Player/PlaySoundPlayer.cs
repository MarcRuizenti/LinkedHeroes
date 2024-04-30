using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPlayer : MonoBehaviour
{
    public AudioClip[] stepsGrass;
    public AudioClip[] stepsIce;
    public AudioClip[] stepsStone;
    public PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    public void PlaySteps()
    {
        switch (controller.material)
        {
            case PlayerController.Materials.ICE:
                AudioClip stepIce = stepsIce[Random.Range(0, stepsIce.Length)];
                SoundManager.Instance.EjecutarAudio(stepIce, 1, 0.2f);
                break;
            case PlayerController.Materials.STONE:
                AudioClip stepStone = stepsStone[Random.Range(0, stepsStone.Length)];
                SoundManager.Instance.EjecutarAudio(stepStone, 1, 0.1f);
                break;
            case PlayerController.Materials.GRASS:
                AudioClip stepGrass = stepsGrass[Random.Range(0, stepsGrass.Length)];
                SoundManager.Instance.EjecutarAudio(stepGrass, 1, 0.025f);
                break;
            default:
                AudioClip stepGrass1 = stepsGrass[Random.Range(0, stepsGrass.Length)];
                SoundManager.Instance.EjecutarAudio(stepGrass1, 1, 0.1f);
                break;
        }

    }
}
