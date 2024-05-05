using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkBullet : Parriable
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    


    private float _parryDirection;

    public override void Parry()
    {
        gameObject.tag = "DamageEnemy";
        _isBeingParried = true;
        if (GetComponent<SpriteRenderer>().flipX) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    void Update()
    {
        if (_isBeingParried)
        {
            transform.position -= (transform.right * speed * Time.deltaTime * transform.localScale.x) * -1;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime * transform.localScale.x;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isBeingParried && collision.tag == "Enemy") 
        { 
            collision.gameObject.GetComponentInParent<HealthBar>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    
}
