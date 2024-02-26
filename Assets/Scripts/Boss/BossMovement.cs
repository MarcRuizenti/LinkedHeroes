using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossMovement : Patroll
{
    [SerializeField] private UnityEvent _onPointReached;

    protected override void PatrollMethod()
    {
        bool PointReached = WayPoints[Index].position == transform.position;
        bool CanInvoke = true;

        if (MovementDelayCounter > 0)
        {
            MovementDelayCounter -= Time.deltaTime;
        }

        if (PointReached)
        {
            Index = Random.Range(0, WayPoints.Count);
            if (CanInvoke)
            {
                _onPointReached?.Invoke();
                CanInvoke = false;
            }
        }

        if (MovementDelayCounter <= 0)
        {
            CanInvoke = true;
            transform.position = Vector3.MoveTowards(transform.position, WayPoints[Index].position, Speed * Time.deltaTime);
            if (PointReached)
            {
                MovementDelayCounter = MovementDelay;
            }
        }
    }

    void Start()
    {
        Index = Random.Range(0, WayPoints.Count);
    }

    // Update is called once per frame
    void Update()
    {
        PatrollMethod();
    }
}
