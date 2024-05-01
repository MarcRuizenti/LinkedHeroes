using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : Parriable
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    public GameManager.Character _bossPhase;
    public Vector3 direccion;


    private bool _isBeingParried = false;
    private float _parryDirection;



    public override void Parry()
    {
        _isBeingParried = true;
        _parryDirection = GetPlayerScale();
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
            transform.position += direccion * speed * Time.deltaTime * _parryDirection;
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
            Destroy(gameObject);
        }
    }
}
