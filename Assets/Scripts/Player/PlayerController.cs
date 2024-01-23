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
    [SerializeField] private GameObject _espada;
    private bool _inputAttack; 

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
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxVelocityFly;

    //detect ground
    private Vector3 _originRight;
    private Vector3 _originLeft;
    public List<Vector3> _origins;
    [SerializeField] private LayerMask _groundLayer;

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

        Attack();

    }

    //Handles player inputs and stores them
    private void HandleInputs()
    {
        _jumpInput = Input.GetButtonDown("Jump");

        _horizontalInput = Input.GetAxis("Horizontal");

        _inputAttack = Input.GetButtonDown("Attack");
    }

    //Player movement on the horizontal axis
    private void MoveHorizontal()
    {
        if (!IsGrounded())
        {
            if (_rb.velocity.x < _maxVelocityFly && _rb.velocity.x > -_maxVelocityFly)
                _rb.velocity += new Vector2(_horizontalInput, 0) * _speedFly * Time.deltaTime;
        }
        else
        {
            if (_rb.velocity.x < _maxVelocity && _rb.velocity.x > -_maxVelocity)
                _rb.velocity += new Vector2(_horizontalInput, 0) * _speed * Time.deltaTime;
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

    private void Attack()
    {
        if (_inputAttack)
        {
            StartCoroutine(AttackDuration());
        }
    }

    private IEnumerator AttackDuration()
    {
        _espada.SetActive(true);
        yield return new WaitForSeconds(1);
        _espada.SetActive(false);
    }
}
