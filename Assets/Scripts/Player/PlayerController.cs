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
    private bool _inputAttack;
    private float _direction;

    //jump variables
    [SerializeField] private float _jumpForce;
    //[SerializeField] private float _jumpFriction;
    private float _coyoteTime = 0.15f;
    private float _coyoteTimeCounter;
    [SerializeField] private float _sacaleGravity;
    [SerializeField] private float _timeJump;
    [SerializeField] private bool _activeTimeJump = false;
    [SerializeField] private float _timeJumpCurent;
    [SerializeField] private float _timeSlay;

    //movement variables
    [SerializeField] private float _speed;
    [SerializeField] private float _speedFly;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxVelocityFly;

    //detect ground
    private Vector3 _originRight;
    private Vector3 _originLeft;
    public List<Vector3> _origins;
    [SerializeField] private LayerMask _groundLayer;
    
    LineRenderer _lineRenderer;
    DistanceJoint2D _distanceJoint;

    //hook variables
    Transform _anchor;
    float _distance;
    bool _enemyHooked;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _distanceJoint = GetComponent<DistanceJoint2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _distanceJoint.enabled = false;
    }


    private void Update()
    {
        HandleInputs();

        MoveHorizontal();

        if (IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
            _activeTimeJump = false;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            _activeTimeJump = true;
        }

        if (_coyoteTimeCounter >= 0 && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

           


        if (!_activeTimeJump)
        {
            _timeJumpCurent = _timeJump; 
        }
        else
        {
            _timeJumpCurent -= Time.deltaTime;
        }

        if (_timeJumpCurent <= 0)
        {
            _rb.gravityScale = 2f;
        }
        else
        {
            _rb.gravityScale = 1;
        }

        Attack();

        Flip();

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        //if (_enemyHooked)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _anchor.position, 3 * Time.deltaTime);

        //    //if(transform.position == _anchor.position)
        //    //{
        //    //    _enemyHooked = false;
        //    //}
        //}

        if (_distanceJoint.enabled && IsGrounded())
        {
            _lineRenderer.enabled = false;
            _distanceJoint.enabled = false;

        }
    }

    //Handles player inputs and stores them
    private void HandleInputs()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        _verticalInput = Input.GetAxis("Vertical");

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

        if (_horizontalInput == 0)
        {
            if (_rb.velocity.x > 0)
                _rb.velocity = new Vector2(_rb.velocity.x - (Time.deltaTime * _timeSlay), _rb.velocity.y);
            if (_rb.velocity.x < 0)
                _rb.velocity = new Vector2(_rb.velocity.x + (Time.deltaTime * _timeSlay), _rb.velocity.y);
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
            switch (GameManager.Instance._currentCharacter)
            {
                case GameManager.Character.AIKE:
                    Aike();
                    break;
                case GameManager.Character.KROKUR:
                    Krokur();
                    break;
                default:
                    break;
            }
        
        }
    }
    private void Aike()
    {
        if(_verticalInput > 0)
        {
            StartCoroutine(AttackDuration(gameObject.transform.GetChild(1).gameObject));
            return;
        }

        if(_verticalInput < 0)
        {
            StartCoroutine(AttackDuration(gameObject.transform.GetChild(2).gameObject));
            return;
        }

        
        StartCoroutine(AttackDuration(gameObject.transform.GetChild(0).gameObject));
    }

    private IEnumerator AttackDuration(GameObject _sword)
    {
        _sword.SetActive(true);
        _sword.GetComponent<Animator>().Play("Slash", 0);
        yield return new WaitForSeconds(0.4f);
        _sword.SetActive(false);
    }

    private void Krokur()
    {
        if (_distanceJoint.enabled)
        {
            _lineRenderer.enabled = false;
            _distanceJoint.enabled = false;
        }
        else
        {
            _anchor = this.GetComponentInChildren<AnchorManager>().GetTargetAnchor((int)this.transform.localScale.x);
            if (_anchor == null)
                return;
            
            Vector2 targetPos = _anchor.position;
            _lineRenderer.SetPosition(0, targetPos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = targetPos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;

            

            //if (_anchor.parent != null && _anchor.parent.tag == "Enemy")
            //{
            //    Debug.Log("hit");
            //    _anchor.parent.GetComponent<Patroller>().enabled = false;
            //    _distance = Vector3.Distance(transform.position, _anchor.position);
            //    _enemyHooked = true;
            //}
        }
    }

    private void Flip()
    {
        if (_horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (_horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}