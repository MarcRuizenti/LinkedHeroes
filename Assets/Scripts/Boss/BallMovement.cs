using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        Destroy(gameObject, lifeTime);
    }
}
