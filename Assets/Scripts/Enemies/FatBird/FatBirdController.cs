using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBirdController : MonoBehaviour
{
    public enum States { IDLE, FALL, GROUND, FLYUP, HIT }

    public States CurrentState = States.IDLE;

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector3 _startingPosition;
    private bool _canFall = true;
    [SerializeField] private Collider2D _detectPlayer;


    [Header("Raycast Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private List<Vector2> _origins;

    private void Start()
    {
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case States.IDLE:
                break;
            case States.FALL:
                _animator.SetFloat("Velocity_Y", _rb.velocity.y);
                foreach (Vector2 point in _origins)
                {
                    RaycastHit2D raycast = Physics2D.Raycast(transform.position + (Vector3)point, Vector2.down, _collider.bounds.extents.y, _groundLayer);
                    Debug.DrawRay(transform.position + (Vector3)point, Vector2.down * (_collider.bounds.extents.y), Color.red);
                    if (raycast.collider != null)
                    {
                        if (!_animator.GetBool("Grounded")) _animator.SetBool("Grounded", true);
                        CurrentState = States.GROUND;
                        Debug.DrawRay(transform.position + (Vector3)point, Vector2.down * (_collider.bounds.extents.y), Color.blue);
                    }
                }
                break;
            case States.GROUND:
                _animator.SetFloat("Velocity_Y", 0);
                _canFall = false;
                _detectPlayer.enabled = false;
                break;
            case States.FLYUP:
                _animator.SetBool("Grounded", false);
                float distance = Vector3.Distance(transform.position, _startingPosition);
                if (distance > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _startingPosition, 1 * Time.deltaTime);
                }
                else if (distance <= 0)
                {
                    CurrentState = States.IDLE;
                    _canFall = true;
                    _detectPlayer.enabled = true;
                }
                break;
            case States.HIT:
                break;
            default:
                CurrentState = States.IDLE;
                break;

        }


    }

    public void StartFalling()
    {
        if(_canFall) 
        {
            CurrentState = States.FALL;
            _rb.gravityScale = 1.0f;
        }
    }

    public void FlyUp()
    {
        CurrentState = States.FLYUP;
        _rb.gravityScale = 0f;
    }
}
