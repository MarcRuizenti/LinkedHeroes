using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Patroll
{
    protected override void PatrollMethod()
    {
        float distance = Vector3.Distance(transform.position, WayPoints[Index].position);
        if (distance < 0.1f)
        {
            Index++;
            if (Index >= WayPoints.Count) Index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[Index].position, Speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
        transform.position = WayPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        PatrollMethod();
    }
}
