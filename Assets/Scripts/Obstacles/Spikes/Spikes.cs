using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Parriable
{
    [SerializeField] private GameObject _player;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    public override void Parry()
    {
        if(_player != null && _player.transform.position.y > transform.position.y) 
        {
            Debug.Log("parry");
            _player.GetComponent<Rigidbody2D>().velocity = _knockback;
        }
    }
}
