using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Controller : Patroll
{

    //Particulas
    [SerializeField] private ParticleSystem particulasMovementMushroom;

    [SerializeField] private Animator _animator;
    public enum States
    {
        IDLE,
        WALK,
        HIT
    }

    public States CurrentState = States.IDLE;

    private void Start()
    {
        Index = 0;
    }

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

    public void StopDamage()
    {
        _animator.SetBool("isDamaged", false);
    }
    
    protected override void PatrollMethod()
    {
        if(!particulasMovementMushroom.isPlaying)
            particulasMovementMushroom.Play();

        Vector3 scale = transform.localScale;
        if (transform.position.x < WayPoints[Index].position.x)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
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
