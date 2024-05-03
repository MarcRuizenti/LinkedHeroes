using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundPlayer : PlaySounds
{
    public AudioClip[] stepsGrass;
    public AudioClip[] stepsIce;
    public AudioClip[] stepsStone;
    public AudioClip[] stepsWood;
    public AudioClip[] stepsSteel;
    public PlayerController controller;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<PlayerController>();
    }
    public void PlaySteps()
    {
        switch (controller.material)
        {
            case PlayerController.Materials.ICE:
                AudioClip stepIce = stepsIce[Random.Range(0, stepsIce.Length)];
                PlaySoundLocalAudioSource(stepIce, 1, 0.2f);
                break;
            case PlayerController.Materials.STONE:
                AudioClip stepStone = stepsStone[Random.Range(0, stepsStone.Length)];
                PlaySoundLocalAudioSource(stepStone, 1, 0.1f);
                break;
            case PlayerController.Materials.GRASS:
                AudioClip stepGrass = stepsGrass[Random.Range(0, stepsGrass.Length)];
                PlaySoundLocalAudioSource(stepGrass, 1, 0.025f);
                break;
            case PlayerController.Materials.WOOD:
                AudioClip stepWood = stepsWood[Random.Range(0, stepsWood.Length)];
                PlaySoundLocalAudioSource(stepWood, 1, 0.1f);
                break;
            case PlayerController.Materials.STEEL:
                AudioClip stepSteel = stepsSteel[Random.Range(0, stepsSteel.Length)];
                PlaySoundLocalAudioSource(stepSteel, 1, 0.1f);
                break;
            default:
                AudioClip stepWood1 = stepsWood[Random.Range(0, stepsWood.Length)];
                PlaySoundLocalAudioSource(stepWood1, 1, 0.1f);
                break;
        }

    }

    
}
