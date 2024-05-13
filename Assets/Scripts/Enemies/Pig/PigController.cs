using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : Patroll
{

    //Particulas
    [SerializeField] private ParticleSystem particulasMovementMushroom;

    [SerializeField] private Animator _animator;
    public enum States
    {
        IDLE,
        WALK,
        RUN,
        HIT
    }

    private SpriteRenderer _spriteRenderer;

    public States CurrentState = States.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case States.IDLE:
                _animator.SetBool("isWalking", false);
                break;
            case States.WALK:
                _animator.SetBool("isWalking", true);
                PatrollMethod();
                break;
            case States.RUN:

                break;
            case States.HIT:
                _animator.SetBool("isDamaged", true);
                break;
        }
    }

    public void SwitchState()
    {
        switch (CurrentState)
        {
            case States.IDLE:
                CurrentState = States.WALK;
                break;
            case States.WALK:
                CurrentState = States.IDLE;
                break;
            case States.HIT:
                break;
        }
    }

    protected override void PatrollMethod()
    {
        if (!particulasMovementMushroom.isPlaying)
            particulasMovementMushroom.Play();

        Vector3 scale = transform.localScale;
        if (transform.position.x < WayPoints[Index].position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        transform.localScale = scale;

        float distance = Vector3.Distance(transform.position, WayPoints[Index].position);
        if (distance < 0.1f)
        {
            Index++;
            if (Index >= WayPoints.Count) Index = 0;

            CurrentState = States.IDLE;
            particulasMovementMushroom.Stop();
        }

        transform.position = Vector3.MoveTowards(transform.position, WayPoints[Index].position, Speed * Time.deltaTime);
    }
}
