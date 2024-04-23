using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : Patroll
{

    public Transform _firstIndex;
    public Transform _lastIndex;

    int _direction;
    // Start is called before the first frame update
    void Start()
    {
        _firstIndex = WayPoints[0];
        _lastIndex = WayPoints[WayPoints.Count - 1];
        Index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PatrollMethod();
    }

    protected override void PatrollMethod()
    {
        float distanceToFirst = Vector3.Distance(transform.position, _firstIndex.position);
        float distanceToLast = Vector3.Distance(transform.position, _lastIndex.position);
        if (distanceToFirst < 0.1f) _direction = 1;
        if (distanceToLast < 0.1f)
        {
            _direction = -1;
        }

        
        float distance = Vector3.Distance(transform.position, WayPoints[Index].position);
        if (distance < 0.1f)
        {

            Index += _direction;

        }

        transform.position = Vector3.MoveTowards(transform.position, WayPoints[Index].position, Speed * Time.deltaTime);
    }
}
