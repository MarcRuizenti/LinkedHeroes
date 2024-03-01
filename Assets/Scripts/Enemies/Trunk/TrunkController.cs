using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkController : MonoBehaviour
{
    [Header("Trunk Settings")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Animator _animator;

    public enum States
    {
        IDLE,
        ATTACK
    }

    public States _currentState = States.IDLE;
    void Update()
    {
        switch (_currentState)
        {
            case States.IDLE:
                _animator.SetBool("isAttacking", false);
                break;
            case States.ATTACK:
                _animator.SetBool("isAttacking", true);
                break;
        }
    }

    public void SwitchState()
    {
        switch (_currentState)
        {
            case States.IDLE:
                _currentState = States.ATTACK;
                break;
            case States.ATTACK:
                _currentState = States.IDLE;
                break;
        }
    }

    public void Shoot()
    {
        GameObject temp = Instantiate(_bulletPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        temp.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
