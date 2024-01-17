using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //player components
    [SerializeField] Rigidbody2D _rb;

    //jump variables
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _jumpFriction;
    private bool _jumpInput;

    //movement variables
    [SerializeField] private float _speed;
    [SerializeField] private float _speedFly;
    private float _horizontalInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        HandleInputs();

        MoveHorizontal();

        if (_jumpInput && _isGrounded)
        {
            Jump();
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _isGrounded = true;
        }
        
    }

    private void HandleInputs()
    {
        _jumpInput = Input.GetButtonDown("Jump");

        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private void MoveHorizontal()
    {
        if (!_isGrounded)
        {
            transform.position += new Vector3(_horizontalInput, 0, 0) * _speedFly  * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime;
        }
        
    }

    private void Jump()
    {
        _isGrounded = false;
        
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }
}
