using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    [SerializeField] private Vector2 _knockback;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Parry":
                collision.gameObject.GetComponent<Parriable>().Parry();
                break;
            case "Enemy":
                collision.gameObject.GetComponentInParent<HealthBar>().TakeDamage(1);
                break;
            case "Boss":
                if (collision.gameObject.GetComponent<BossHealth>().healthShield <= 0)
                    collision.gameObject.GetComponent<BossHealth>().TakeDamage();
                break;
        }
    }
}