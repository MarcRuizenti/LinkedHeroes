using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDamageTaken;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private float timeRespawn;

    private Shaker _shaker;

    private void Start()
    {
        _shaker = FindObjectOfType<Shaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        switch (tag)
        {
            case "Damage":
                _onDamageTaken?.Invoke();
                _shaker.CamShake(0.3f, -0.07f, -0.03f);
                break;
            case "Bullet":
                _onDamageTaken?.Invoke();
                _shaker.CamShake(0.3f, -0.07f, -0.03f);
                Destroy(collision.gameObject.transform.parent.gameObject);
                break;
            case "Border":
                Invoke("invokeDeath", timeRespawn);
                GameManager.Instance.SetFollowCamera(false);
                break;
        }
    }

    private void invokeDeath()
    {
        _onDeath?.Invoke();
    }
}
