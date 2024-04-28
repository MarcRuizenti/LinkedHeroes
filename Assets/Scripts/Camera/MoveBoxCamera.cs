using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxCamera : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 newPosition;
    public float speed;
    void Start()
    {
        startPosition = transform.position;
    }


    void FixedUpdate()
    {
        Vector3 distance = transform.position - startPosition;
        newPosition = transform.parent.transform.position + distance;

        if (transform.position != startPosition)
        {
            speed = 3.0f;
        }
        else
        {
            speed = 0.0f;
        }

        transform.parent.transform.position = Vector3.Lerp(transform.parent.transform.position, newPosition, speed * Time.fixedDeltaTime);


    }
}
