using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] spawnBullet;
    [SerializeField] private AudioClip shut;
    public void PlaySpawnBullt(float pich, float volumen) 
    {
        SoundManager.Instance.EjecutarAudio(spawnBullet[Random.Range(0, spawnBullet.Length)], pich, volumen);
    }
    public void PlayShut(float pich, float volumen) 
    {
        SoundManager.Instance.EjecutarAudio(shut, pich, volumen);
    }
}
