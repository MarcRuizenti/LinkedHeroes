using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    [SerializeField] private UnityEvent _onDamageTaken;
    [SerializeField] private UnityEvent _onDeath;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        switch (tag)
        {
            case "Damage":
                _onDamageTaken?.Invoke();
                break;
            case "Bullet":
                _onDamageTaken?.Invoke();
                Destroy(collision.gameObject.transform.parent.gameObject);
                break;
            case "Border":
                _onDeath?.Invoke();
                break;
        }
    }
}
