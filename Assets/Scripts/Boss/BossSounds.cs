using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] spawnBullet;
    [SerializeField] private AudioClip shut;
    private BossController bossController;
    private AudioSource audioSource;
    private void Start()
    {
        bossController = GetComponent<BossController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpawnBullt(float pitch, float volume) 
    {
        audioSource.PlayOneShot(spawnBullet[Random.Range(0, spawnBullet.Length)]);
        audioSource.pitch = pitch;
        audioSource.volume = volume;
    }
    public void PlayShut(float pitch, float volume) 
    {
        audioSource.PlayOneShot(shut);
        audioSource.pitch = pitch;
        audioSource.volume = volume;
    }

    public void SoundAttackKrokur(float volume)
    {
        if (bossController._character == GameManager.Character.KROKUR)
        {
            PlayShut(1, volume);
        }
    }
}
