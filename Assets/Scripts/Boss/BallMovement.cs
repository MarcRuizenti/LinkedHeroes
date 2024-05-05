using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : Parriable
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    public GameManager.Character _bossPhase;
    public Vector3 direccion;





    public override void Parry()
    {
        if (!((direccion.x < 0 && GetPlayerScale() < 0) || (direccion.x > 0 && GetPlayerScale() > 0)))
            _isBeingParried = true;
        //_parryDirection = GetPlayerScale();
        gameObject.tag = "DamageBoss";


    }


    void Update()
    {

        if (GameManager.Character.AIKE == _bossPhase)
        {
            direccion = transform.up;
        }

        if (_isBeingParried) 
        { 
            transform.position += (direccion * speed * Time.deltaTime) * -1;
        }
        else
        {
            transform.position += direccion * speed * Time.deltaTime;
        }
        
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Box")
        {
            BoxController box = collision.gameObject.GetComponent<BoxController>();
            if (box)
            {
                box.TakeDamge();
                Destroy(gameObject);
            }
        }
    }
}
