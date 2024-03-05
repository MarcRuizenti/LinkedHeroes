using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLines : MonoBehaviour
{

    [SerializeField] float velocityY;

    float spawnPosY = 6f; //6.24f


    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + velocityY * Time.deltaTime, this.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("BackgroundTrigger"))
            return;
        this.transform.position = new Vector3(this.transform.position.x, spawnPosY, this.transform.position.z);

    }
}
