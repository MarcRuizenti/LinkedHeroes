using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parriable : MonoBehaviour
{
    [Header("Parry Settings")]
    [SerializeField] protected Vector2 _knockback;

    protected float GetPlayerScale()
    {
        return GameObject.FindWithTag("Player").transform.localScale.x;
    }

    public virtual void Parry() { }

}
