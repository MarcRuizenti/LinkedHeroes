using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform _krokurPhaseAttackPosition;
    [SerializeField] private float _krokurAttackDelay;

    [Header("Health Settings")]
    [SerializeField] private Transform _fallPosition;
    [SerializeField] private Collider2D _damageCollider;

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
                animator.SetBool("isDamaged", false);
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
                animator.SetBool("isAttacking", true);
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
        _currentState = States.RECHARGE;
        animator.SetBool("isAttacking", false);
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
        _damageCollider.enabled = true;
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
        this.enabled = false;
        GameObject tempO = Instantiate(_damageBall, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        this.enabled = true;
        animator.SetBool("isAttacking", false);
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
        health = maxHealth;
        healthShield = maxHealthShield;
    }

    public void MoveToPosition(Transform newPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition.position, Speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        Debug.Log("Au");
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
            _damageCollider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBoss"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }


}
