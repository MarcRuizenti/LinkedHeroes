using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : Patroll
{

    

    // Start is called before the first frame update
    void Start()
    {
        Index = 0;

        transform.position = WayPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
