using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float jumpForce;
    public float jumpForceMax;
    public float speed;

    private void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Jump");
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;

        if (Input.GetButton("Jump"))
        {
            transform.position += new Vector3(0, jumpForce, 0) * speed * Time.deltaTime;
           
            Debug.Log("Salta");
            jumpForce--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
    
}
