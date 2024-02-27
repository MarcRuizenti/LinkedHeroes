using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : Patroll
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private Transform _krokurPhaseAttackPosition;
    [SerializeField] private float _krokurAttackDelay;


    public enum States
    {
        IDLE,
        MOVE,
        STOP,
        ATTACK,
        RECHARGE
    }

    public States _currentState = States.IDLE;

    private void Update()
    {
        switch (_currentState)
        {
            case States.IDLE:
                Index = Random.Range(0, WayPoints.Count);
                _currentState = States.MOVE;
                break;
            case States.MOVE:
                PatrollMethod();
                break;
            case States.STOP:
                _currentState = States.ATTACK;
                break;
            case States.ATTACK:
                MovementDelayCounter = MovementDelay;
                Attack();
                _currentState = States.RECHARGE;
                break;
            case States.RECHARGE:
                Recharge();
                break;
            default:
                _currentState = States.IDLE;
                break;
        }
    }

    private void Recharge()
    {
        if(MovementDelayCounter > 0)
        {
            MovementDelayCounter -= Time.deltaTime;
        }
        else
        {
            _currentState = States.IDLE;
        }
    }

    private void Attack()
    {
        switch (GameManager.Instance._currentCharacter)
        {
            case GameManager.Character.AIKE:
                AikePhase();
                break;
            case GameManager.Character.KROKUR:
                KrokurPhase();
                break;
        }
    }

    private void KrokurPhase()
    {
        this.enabled = false;
        GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        this.enabled = true;
    }

    private void AikePhase()
    {
        for (int i = 0; i < 360; i += 45)
        {
            GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);

            tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, i);

            _balls.Add(tempO);
        }

        int rand1 = Random.Range(0, 5);
        int rand2 = Random.Range(5, 8);
        _balls[rand1].GetComponent<SpriteRenderer>().color = Color.blue;
        _balls[rand2].GetComponent<SpriteRenderer>().color = Color.blue;
        _balls[rand1].tag = "Parry";
        _balls[rand2].tag = "Parry";

        _balls.Clear();
    }

    protected override void PatrollMethod()
    {
        bool PointReached = WayPoints[Index].position == transform.position;


        transform.position = Vector3.MoveTowards(transform.position, WayPoints[Index].position, Speed * Time.deltaTime);

        if (MovementDelayCounter > 0)
        {
            MovementDelayCounter -= Time.deltaTime;
        }

        if (PointReached)
        {
            _currentState = States.STOP;
        }


    }

    private void OnEnable()
    {
        Index = Random.Range(0, WayPoints.Count);
    }

    public void MoveToPosition(Transform newPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition.position, Speed * Time.deltaTime);
    }



}
