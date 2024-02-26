using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Patroll : MonoBehaviour
{
    [Header("Patroll Settings")]
    [SerializeField] protected List<Transform> WayPoints;
    [SerializeField] protected float Speed;
    [SerializeField] protected int Index;
    [SerializeField] protected float MovementDelay;
    [SerializeField] protected float MovementDelayCounter;

    protected virtual void PatrollMethod() { }
}
