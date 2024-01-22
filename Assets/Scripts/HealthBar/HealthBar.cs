using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBar : MonoBehaviour 
{
    [SerializeField] private int _maxHealth;
    [SerializeField] int _health;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private Collider2D _damagebleCollider;
    [SerializeField] private Vector2 _knocback;

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
        gameObject.GetComponent<Rigidbody2D>().velocity = _knocback * new Vector2(transform.localScale.x, 1);
        StartCoroutine(InvincibleMode());
    }

    private void Die()
    {
        _onDeath?.Invoke();
    }

    private IEnumerator InvincibleMode()
    {
        _damagebleCollider.enabled = false;

        if(gameObject.GetComponent<PlayerController>() != null )
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            gameObject.GetComponent<PlayerController>().enabled = true;
        }

        Color originalColor = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,.5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        _damagebleCollider.enabled = true;
    }
}
