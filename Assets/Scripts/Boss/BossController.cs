using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossController : Patroll
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool _isFallen;
    [SerializeField] private float _fallDelayCounter = 1;

    [Header("Attack Settings")]
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private Sprite _parryBall;
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private float _krokurAttackCounter;
    [SerializeField] private float _krokurAttackCounterTime;
    private bool _canRechargekrokurAttack = true;
    [SerializeField] private GameObject _frog;


    [Header("Health Settings")]
    [SerializeField] private Transform _fallPosition;

    public int maxHealth;
    public int health;

    public int maxHealthShield;
    public int healthShield;

    public enum States
    {
        IDLE,
        MOVE,
        STOP,
        ATTACK,
        RECHARGE,
        FALL
    }

    public States _currentState = States.IDLE;

    private void Update()
    {
        switch (_currentState)
        {
            case States.IDLE:
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                Index = Random.Range(0, WayPoints.Count);
                transform.GetChild(1).GetComponent<CircleCollider2D>().radius = 0.43f;
                transform.GetChild(2).GetComponent<CircleCollider2D>().radius = 0.53f;
                _currentState = States.MOVE;
                break;
            case States.MOVE:
                PatrollMethod();
                break;
            case States.STOP:
                _currentState = States.ATTACK;
                if (GameManager.Instance._currentCharacter == GameManager.Character.KROKUR) 
                {
                    _krokurAttackCounter = _krokurAttackCounterTime;
                }
                break;
            case States.ATTACK:
                animator.SetBool("isAttacking",true);
                MovementDelayCounter = MovementDelay;
                break;
            case States.RECHARGE:
                Recharge();
                break;
            case States.FALL:
                if (_fallDelayCounter > 0)
                {
                    _fallDelayCounter -= Time.deltaTime;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).GetComponent<CircleCollider2D>().radius = 0.22f;
                    transform.GetChild(2).GetComponent<CircleCollider2D>().radius = 0.35f;
                }
                break;
            default:
                _currentState = States.IDLE;
                break;
        }
    }

    public void FinishAttack()
    {
        Attack();
        if (_canRechargekrokurAttack || GameManager.Instance._currentCharacter == GameManager.Character.AIKE)
        {
            _currentState = States.RECHARGE;
            animator.SetBool("isAttacking", false);
        }
    }

    public void CheckIfFall()
    {
        if (healthShield <= 0)
        {
            animator.SetTrigger("Fall");
            _currentState = States.FALL;
        }
        else
        {
            animator.SetTrigger("NotFall");
            _currentState = States.IDLE;
        }
    }

    public void ActivateCollider()
    {
        _currentState = States.IDLE;
    }

    private void Recharge()
    {
        if (MovementDelayCounter > 0)
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
        _krokurAttackCounter -= 1;
        if (_krokurAttackCounter > 0)
        {
            GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
            if (tempO.transform.position.x > _frog.transform.position.x)
            {
                tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, Vector2.Angle(tempO.transform.position, _frog.transform.position));
                Debug.Log(Vector2.Angle(tempO.transform.position, _frog.transform.position));
            }
            else
            {
                tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, -Vector2.Angle(tempO.transform.position, _frog.transform.position));
                Debug.Log(-Vector2.Angle(tempO.transform.position, _frog.transform.position));

            }
            _canRechargekrokurAttack = false;
        }
        else 
        {
            _canRechargekrokurAttack = true;
        }
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
        _balls[rand1].GetComponent<SpriteRenderer>().sprite = _parryBall;
        _balls[rand2].GetComponent<SpriteRenderer>().sprite = _parryBall;
        _balls[rand1].transform.GetChild(1).gameObject.SetActive(true);
        _balls[rand2].transform.GetChild(1).gameObject.SetActive(true);

        _balls.Clear();

    }

    protected override void PatrollMethod()
    {
        if (transform.position.x < WayPoints[Index].position.x)
        {
            transform.localScale = new Vector3(2.5f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-2.5f, transform.localScale.y, transform.localScale.z);
        }

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
        health = maxHealth;
        healthShield = maxHealthShield;
    }

    public void MoveToPosition(Transform newPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition.position, Speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        if (healthShield > 0)
        {
            healthShield--;

            animator.SetTrigger("ShieldDamage");
        }
        else
        {
            health--;
            healthShield = maxHealthShield;
            gameObject.GetComponent<BossController>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            animator.SetTrigger("Attack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBoss"))
        {
            Debug.Log("Au");
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
