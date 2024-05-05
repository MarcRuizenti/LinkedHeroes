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
    [SerializeField] private bool _isRegening = false;
    [SerializeField] private bool _isRecharging = false;

    [Header("Attack Settings")]
    [SerializeField] private GameObject _damageBall;
    [SerializeField] private Sprite _parryBall;
    [SerializeField] private List<GameObject> _balls;
    [SerializeField] private float _krokurAttackCounter;
    [SerializeField] private float _krokurAttackCounterTime;
    private bool _canRechargekrokurAttack = true;
    [SerializeField] private GameObject _player;
    [SerializeField] public GameManager.Character _character;
    [SerializeField] private int _krokurCycles;
    [SerializeField] private float _krokurRechargeTime;
    [SerializeField] private float _krokurRechargeTimeCounter;


    [Header("Health Settings")]
    public int maxHealth;
    public int health;
    public int maxHealthShield;
    public int healthShield;
    [SerializeField] private UnityEvent _onBossDefeated;

    [Header("Phase Change Settings")]
    [SerializeField] private bool canChangePhase = true;
    [SerializeField] private Transform _changePhasePosition;
    [SerializeField] private GameObject _skull;


    public enum States
    {
        IDLE,
        MOVE,
        STOP,
        ATTACK,
        RECHARGE,
        RECHARGE_KROKUR,
        FALL,
        CHANGE_PHASE
    }

    [Header("States")]
    public States _currentState = States.IDLE;

    //Sonido
    private BossSounds _sound;

    private void Start()
    {
        _character = GameManager.Instance._currentCharacter;
        _sound = gameObject.GetComponent<BossSounds>();
    }

    private void Update()
    {
        switch (_currentState)
        {
            case States.IDLE:
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                Index = Random.Range(0, WayPoints.Count);
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).GetComponent<CircleCollider2D>().radius = 0.94f;
                transform.GetChild(2).GetComponent<CircleCollider2D>().radius = 1.35f;
                _krokurRechargeTimeCounter = _krokurRechargeTime;
                _isRegening = false;
                animator.ResetTrigger("Regen");
                if (_character == GameManager.Character.KROKUR)
                {
                    _krokurAttackCounter = _krokurAttackCounterTime;
                    _isRecharging = false;
                }
                _currentState = States.MOVE;
                break;
            case States.MOVE:
                PatrollMethod();
                break;
            case States.STOP:
                _currentState = States.ATTACK;
                break;
            case States.ATTACK:
                animator.SetBool("isAttacking", true);
                MovementDelayCounter = MovementDelay;
                break;
            case States.RECHARGE:
                if(_character == GameManager.Character.KROKUR)
                {
                    healthShield = 0;
                    RechargeKrokur();
                }
                else
                {
                    Recharge();
                }
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
                    transform.GetChild(1).GetComponent<CircleCollider2D>().radius = 0.61f;
                    transform.GetChild(2).GetComponent<CircleCollider2D>().radius = 0.92f;
                }
                break;
            case States.CHANGE_PHASE:
                canChangePhase = false;
                MoveToPosition(_changePhasePosition);
                break;
            default:
                _currentState = States.IDLE;
                break;
        }
    }



    private void RechargeKrokur()
    {
        if (_krokurCycles <= 0)
        {
            _canRechargekrokurAttack = true;

            if (transform.position != _changePhasePosition.position)
            {
                MoveToPosition(_changePhasePosition);
            }
            else
            {
                if (_krokurRechargeTimeCounter > 0)
                {
                    _krokurRechargeTimeCounter -= Time.deltaTime;
                }
                else if (_krokurRechargeTimeCounter <= 0 && !_isRegening)
                {
                    Debug.Log("hola");
                    _isRegening = true;
                    Debug.Log("Regen2");
                    animator.SetTrigger("Regen");
                }
            }

            if (!_isRecharging)
            {
                Debug.Log("Regen4");
                animator.SetTrigger("RechargeKrokur");
                _isRecharging = true;
            }
        }
        else
        {
            Recharge();
        }

       
    }

    public void FinishAttack()
    {
        Attack();

        if (_canRechargekrokurAttack|| _character == GameManager.Character.AIKE)
        {
            animator.SetBool("isAttacking", false);
            if (_krokurCycles > 0 && _character == GameManager.Character.KROKUR) _krokurCycles--;
            _currentState = States.RECHARGE;
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

    public void ReturnToIdle()
    {
        _isRegening = false;
        _krokurCycles = 2;
        healthShield = maxHealthShield;
        _currentState = States.IDLE;
    }

    public void StartSecondPhase()
    {
        if (!canChangePhase && health <= 0)
        {
            _skull.transform.position = transform.position;
            _skull.SetActive(true);
            UpdateCharacter();
            health = maxHealth;
            _sound.SoundSpawnMiror(1, 0.5f);
        }
    }

    public void UpdateCharacter()
    {
        if (_character == GameManager.Character.AIKE)
        {
            _character = GameManager.Character.KROKUR;
        }
        else
        {
            _character = GameManager.Character.AIKE;
        }
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
        switch (_character)
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
            Vector3 direccion = tempO.transform.position - _player.transform.position;
            tempO.GetComponent<BallMovement>().direccion = -direccion.normalized;
            tempO.GetComponent<BallMovement>()._bossPhase = _character;
            _canRechargekrokurAttack = false;
        }
        else
        {

            _canRechargekrokurAttack = true;
        }


    }

    private IEnumerator InvoceBullets()
    {
        for (int i = 0; i < 360; i += 45)
        {
            Vector3 outdir = new Vector3(Mathf.Cos(i * (Mathf.PI / 180)), Mathf.Sin(i * (Mathf.PI / 180)));

            Vector3 position = transform.position + outdir * 1.6f;

            GameObject tempO = Instantiate(_damageBall, new Vector3(position.x, position.y, -1), transform.rotation);

            int rand1 = Random.Range(0, 2);
            if (rand1 == 0)
            {
                tempO.GetComponent<SpriteRenderer>().sprite = _parryBall;
            }

            tempO.transform.localEulerAngles = new Vector3(tempO.transform.rotation.x, tempO.transform.rotation.y, i - 90);

            tempO.GetComponent<BallMovement>()._bossPhase = _character;
            tempO.GetComponent<BallMovement>().enabled = false;
            tempO.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;

            _balls.Add(tempO);

            _sound.PlaySpawnBullt(1, 0.05f);
            yield return new WaitForSeconds(0.3f);
        }
        
        //int rand1 = Random.Range(0, 2);
        //int rand2 = Random.Range(2, 4);
        //int rand3 = Random.Range(4, 6);
        //int rand4 = Random.Range(6, 8);
        //tempO.GetComponent<SpriteRenderer>().sprite = _parryBall;
        //_balls[rand2].GetComponent<SpriteRenderer>().sprite = _parryBall;
        //_balls[rand3].GetComponent<SpriteRenderer>().sprite = _parryBall;
        //_balls[rand4].GetComponent<SpriteRenderer>().sprite = _parryBall;
        //tempO.transform.GetChild(1).gameObject.SetActive(true);
        //_balls[rand2].transform.GetChild(1).gameObject.SetActive(true);
        //_balls[rand3].transform.GetChild(1).gameObject.SetActive(true);
        //_balls[rand4].transform.GetChild(1).gameObject.SetActive(true);

        for (int i = 0; i < _balls.Count; i++)
        {
            _balls[i].GetComponent<BallMovement>().enabled = true;
            _balls[i].transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
            if (_balls[i].GetComponent<SpriteRenderer>().sprite == _parryBall)
            {
                _balls[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        _balls.Clear();
        _sound.PlayShut(1, 0.5f);
    }

    private void AikePhase()
    {
        StartCoroutine(InvoceBullets());
    }

    protected override void PatrollMethod()
    {
        GameObject shadow = Shadows.Instance.ShadowTrail();

        if (transform.position.x < WayPoints[Index].position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            if (shadow) shadow.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            if (shadow) shadow.GetComponent<SpriteRenderer>().flipX = false;
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
        if (transform.position.x < newPosition.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition.position, Speed * Time.deltaTime);

        if (transform.position == newPosition.position && _currentState == States.CHANGE_PHASE)
        {
            Debug.Log("Regen3");
            animator.SetTrigger("Regen");
        }
    }

    public void TakeDamage(int amount)
    {
        if (healthShield > 0)
        {
            healthShield -= amount;

            animator.SetTrigger("ShieldDamage");
            _sound.SoundHitShild(1, 0.5f);
        }
        else
        {
            _sound.SoundHit(1, 0.2f);
            health--;
            healthShield = maxHealthShield;
            gameObject.GetComponent<BossController>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            animator.SetTrigger("Attack");
            if (health <= 0)
            {
                if (canChangePhase)
                {
                    _currentState = States.CHANGE_PHASE;
                    animator.SetTrigger("ChangePhase");
                }
                else
                {
                    _onBossDefeated?.Invoke();
                }
            }
            else
            {
                Debug.Log("Regen1");
                animator.SetTrigger("Regen");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageBoss"))
        {
            Debug.Log("Au");
            Destroy(collision.gameObject);
            TakeDamage(1);

        }
    }
}
