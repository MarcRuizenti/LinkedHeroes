using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    //player components
    private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;

    //jump variables
    [SerializeField] private float _jumpForce;
    //[SerializeField] private float _jumpFriction;
    private float _coyoteTime = 0.15f;
    private float _coyoteTimeCounter;
    private bool _jumpInput;

    //movement variables
    [SerializeField] private float _speed;
    [SerializeField] private float _speedFly;
    private float _horizontalInput;

    //detect ground
    private Vector3 _originRight;
    private Vector3 _originLeft;
    public List<Vector3> _origins;
    [SerializeField] private LayerMask _groundLayer;

    //events
    [SerializeField] private UnityEvent _onDamageTaken;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        HandleInputs();

        MoveHorizontal();

        if (IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }


        if (_coyoteTimeCounter >= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

    }

    //Detects ground by using trigger collider and tags
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if (tag == "Damage")
        {
            _onDamageTaken?.Invoke();
        }

    }

    //Handles player inputs and stores them
    private void HandleInputs()
    {
        _jumpInput = Input.GetButtonDown("Jump");

        _horizontalInput = Input.GetAxis("Horizontal");
    }

    //Player movement on the horizontal axis
    private void MoveHorizontal()
    {
        if (IsGrounded())
        {
            transform.position += new Vector3(_horizontalInput, 0, 0) * _speedFly * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(_horizontalInput, 0, 0) * _speed * Time.deltaTime;
        }

    }

    //Jump logic
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }


    private bool IsGrounded()
    {
        _origins.Clear();
        
        _originLeft = _collider.bounds.center - new Vector3(-0.35f, 0.1f, 0);
        _originRight = _collider.bounds.center - new Vector3(0.35f, 0.1f, 0);
        _origins.Add(_originLeft);
        _origins.Add(_originRight);

        int size = _origins.Count;

        for (int i = 0; i < size; i++)
        {
            RaycastHit2D raycast = Physics2D.Raycast(_origins[i], Vector2.down, _collider.bounds.extents.y, _groundLayer);
            Debug.DrawRay(_origins[i], Vector2.down * (_collider.bounds.extents.y), Color.red);
            if (raycast.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}
