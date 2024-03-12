using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Vector2 _knockback;

    private Shaker _shaker;

    private void Start()
    {
        _shaker = FindObjectOfType<Shaker>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Parry":
                collision.gameObject.GetComponentInParent<Parriable>().Parry();
                break;
            case "Enemy":
                collision.gameObject.GetComponentInParent<HealthBar>().TakeDamage(1);
                _shaker.CamShake();
                break;
            case "Boss":
                _shaker.CamShake();
                if (collision.gameObject.GetComponentInParent<BossController>().healthShield <= 0)
                    collision.gameObject.GetComponentInParent<BossController>().TakeDamage(1);
                break;
        }
    }
}