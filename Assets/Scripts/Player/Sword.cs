using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Vector2 _knockback;
    private PlaySounds _playSounds;
    public Shaker _shaker;
    public AudioClip audioClip;


    private void Start()
    {
        _playSounds = GetComponent<PlaySounds>();
        _shaker = FindObjectOfType<Shaker>();
    }

    public void PlaySoundSword()
    {
        _playSounds.PlaySoundSoundManager(audioClip, 1.2f, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Parry":
                _shaker.CamShake(0.02f, -0.01f, -0.01f);
                
                collision.gameObject.GetComponentInParent<Parriable>().Parry();
                break;
            case "Enemy":
                collision.gameObject.GetComponentInParent<HealthBar>().TakeDamage(1);
                _shaker.CamShake(0.1f, -0.035f, -0.015f);
                break;
            case "Boss":     
                if (collision.gameObject.GetComponentInParent<BossController>().healthShield <= 0)
                {
                    _shaker.CamShake(0.1f, -0.035f, -0.015f);
                    collision.gameObject.GetComponentInParent<BossController>().TakeDamage(1);
                }   
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Parry":
                _shaker.CamShake(0.02f, -0.01f, -0.01f);

                collision.gameObject.GetComponentInParent<Parriable>().Parry();
                break;
        }
    }
}