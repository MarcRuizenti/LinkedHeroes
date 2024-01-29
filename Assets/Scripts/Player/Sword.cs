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
                Debug.Log("Parry");
                gameObject.GetComponentInParent<Rigidbody2D>().velocity = _knockback;
                break;
            case "Enemy":
                Debug.Log("Damage");
                collision.gameObject.GetComponentInParent<HealthBar>().TakeDamage(1);
                break;
        }
    }
}