using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parriable : MonoBehaviour
{
    [Header("Parry Settings")]
    [SerializeField] protected Vector2 _knockback;
    protected GameObject _player;

    protected float GetPlayerScale()
    {
        return _player.transform.localScale.x;
    }

    public virtual void Parry() { }

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

}
