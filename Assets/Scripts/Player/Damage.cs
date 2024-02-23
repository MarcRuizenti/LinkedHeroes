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

        if (tag == "Enemy" || tag == "Damage")
        {
            Debug.Log(collision.name);
            _onDamageTaken?.Invoke();
        }
        else if(tag == "Border")
        {
            _onDeath?.Invoke();
        }
    }
}
