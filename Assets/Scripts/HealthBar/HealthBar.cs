using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBar : MonoBehaviour 
{
    [SerializeField] private int _maxHealth;
    [SerializeField] int _health;
    [SerializeField] private UnityEvent _onDeath;

    private void Start()
    {
        _health = _maxHealth;
    }


    private void Update()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
    }

    private void Die()
    {
        _onDeath?.Invoke();
    }
}
