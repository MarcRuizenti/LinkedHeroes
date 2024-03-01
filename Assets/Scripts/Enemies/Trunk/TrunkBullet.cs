using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkBullet : Parriable
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;


    private bool _isBeingParried = false;
    private float _parryDirection;

    public override void Parry()
    {
        _isBeingParried = true;
        _parryDirection = GetPlayerScale();
    }


    void Update()
    {
        if (_isBeingParried)
        {
            transform.position -= transform.right * speed * Time.deltaTime * -_parryDirection;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime * transform.localScale.x;
        }

        Destroy(gameObject, lifeTime);
    }
}
