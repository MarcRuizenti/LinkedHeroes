using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    //Sonidos
    [SerializeField] private AudioClip hookStart;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fall;

    //Material Steps
    public enum Materials { GRASS, ICE, STONE, WOOD, STEEL }
    public Materials material;

    //Hanging boost
    private bool _hangedSpeedBoost;
    private float _hangedSpeed;
    [SerializeField] private float _maxHangedSpeed; //estaba a 20
    bool hanged = false, dragging = false;
    public Action MoveDone;
    private void MovePerformed() => MoveDone?.Invoke();
    public Action JumpDone;
    private void JumpPerformed() => JumpDone?.Invoke();
    public Action ActionDone;
    private void ActionPerformed() => ActionDone?.Invoke();
    public Action ParryDone;
    private void ParryPerformed() => ParryDone?.Invoke();
    //animator

    [SerializeField] Animator _animator;
    [SerializeField] RuntimeAnimatorController _animatorKrokur;
    [SerializeField] RuntimeAnimatorController _animatorAike;
    [SerializeField] Sprite _spriteKrokur;
    [SerializeField] Sprite _spriteAike;

    //player components
    private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private GameObject _anchorManager;
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
    [SerializeField] private float _timeSlayDefault;

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
    Transform _target;
    float _distance;
    bool _enemyHooked;
    private bool _canInteractBox = true;

    //sword cooldown
    [SerializeField] private float _swordCooldown;
    [SerializeField] private float _maxSwordCooldown;
    [SerializeField] private Image _cooldownBar;

    //Particulas
    [SerializeField] private ParticleSystem particulasSalto;
    [SerializeField] private ParticleSystem particulasMovement;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _distanceJoint = GetComponent<DistanceJoint2D>();
        _lineRenderer = GetComponent<LineRenderer>();
        _distanceJoint.enabled = false;
        _anchorManager = GetComponentInChildren<AnchorManager>().gameObject;
        _swordCooldown = _maxSwordCooldown;
    }


    private void Update()
    {
        DetectMatirial();

        HandleInputs();

        MoveHorizontal();

        if (IsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
            _activeTimeJump = false;
            _animator.SetBool("air", false);
            _animator.SetFloat("velY", _rb.velocity.y);
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
            _activeTimeJump = true;
            _animator.SetBool("air", true);
            _animator.SetFloat("velY", _rb.velocity.y);
            _animator.SetBool("walking", false);
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

        if(_swordCooldown > 0) 
        {
            _swordCooldown -= Time.deltaTime;
            _cooldownBar.fillAmount = _swordCooldown / _maxSwordCooldown;
        }
        else {
            _cooldownBar.enabled = false;
        }


        Attack();

        Flip();

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        
        if(GameManager.Instance._currentCharacter != GameManager.Character.KROKUR)
        {
            _anchorManager.SetActive(false);
        }
        else
        {
            _anchorManager.SetActive(true);
        }

        if (!_lineRenderer.enabled && GameManager.Instance._currentCharacter == GameManager.Character.KROKUR)
        {
            _animator.SetTrigger("ReleaseTongue");
        }
        else if (_lineRenderer.enabled && GameManager.Instance._currentCharacter == GameManager.Character.KROKUR)
        {
            _animator.ResetTrigger("ReleaseTongue");
        }
    }

    public void UpdateAnimator()
    {
        switch (GameManager.Instance._currentCharacter)
        {
            case GameManager.Character.AIKE:
                _animator.runtimeAnimatorController = _animatorAike;


                //_animator.SetBool("walking_aike", IsGrounded() && _rb.velocity.x != 0);
                //_animator.SetBool("air_aike", !IsGrounded());
                //_animator.SetFloat("velY_aike", _rb.velocity.y);
                //_animator.SetBool("hanging_aike", _distanceJoint.enabled);
                break;
            case GameManager.Character.KROKUR:
                    _animator.runtimeAnimatorController = _animatorKrokur;


                //_animator.SetBool("walking", IsGrounded() && _rb.velocity.x != 0);
                //_animator.SetBool("air", !IsGrounded());
                //_animator.SetFloat("velY", _rb.velocity.y);
                //_animator.SetBool("hanging", _distanceJoint.enabled);
                break;
            default:
                break;
        }

        _animator.SetTrigger("Swap");
    }

    //Handles player inputs and stores them

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_distanceJoint.enabled)
            {
                _animator.SetTrigger("ReleaseTongue");
                SoundManager.Instance.EjecutarAudio(hookStart, 1, 0.2f);
                if (_target.GetComponent<MovingHookPoint>())
                {

                    _target.GetComponent<MovingHookPoint>().ContinueHooking();
                }
            }
            //_hangedSpeedBoost = true;
            _lineRenderer.enabled = false;
            _distanceJoint.enabled = false;
            JumpPerformed();

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            _hangedSpeedBoost = true;
            MovePerformed();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _hangedSpeedBoost = false;
        }
        if (_hangedSpeedBoost)
        {
            _hangedSpeed += 0.2f;
            if (_hangedSpeed > _maxHangedSpeed)
            {
                _hangedSpeed = _maxHangedSpeed;
            }
        }
        else if (_hangedSpeed > 0)
        {
            _hangedSpeed -= 0.2f;
            if (_hangedSpeed < 0)
            {
                _hangedSpeed = 0;
            }
        }

        _horizontalInput = Input.GetAxis("Horizontal");

        _verticalInput = Input.GetAxis("Vertical");

        _inputAttack = Input.GetButtonDown("Attack");

        if (_distanceJoint.enabled)
        {
            _rb.velocity += new Vector2(_horizontalInput, 0) * _hangedSpeed * Time.deltaTime;

        }
    }


    //Player movement on the horizontal axis
    private void MoveHorizontal()
    {
        if (!IsGrounded())
        {
            if (particulasMovement.isPlaying)
                particulasMovement.Stop();

            if (_rb.velocity.x < _maxVelocityFly && _rb.velocity.x > -_maxVelocityFly)
                _rb.velocity += new Vector2(_horizontalInput, 0) * _speedFly * Time.deltaTime;
        }
        else
        {
            if (_rb.velocity.x < _maxVelocity && _rb.velocity.x > -_maxVelocity)
                _rb.velocity += new Vector2(_horizontalInput, 0) * _speed * Time.deltaTime;

            if(_horizontalInput != 0)
                _animator.SetBool("walking", true);
            else
                _animator.SetBool("walking", false);
            if (_rb.velocity.x != 0 && !particulasMovement.isPlaying)
                particulasMovement.Play();

        }

        if (_horizontalInput == 0)
        {
            particulasMovement.Stop();

            if (_rb.velocity.x > 0)
                _rb.velocity = new Vector2(_rb.velocity.x - (Time.deltaTime * _timeSlay), _rb.velocity.y);
            if (_rb.velocity.x < 0)
                _rb.velocity = new Vector2(_rb.velocity.x + (Time.deltaTime * _timeSlay), _rb.velocity.y);

        }
    }

    //Jump logic
    private void Jump()
    {
        SoundManager.Instance.EjecutarAudio(jump, 1, 0.2f);
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        particulasSalto.Play();
    }

    public void DetectMatirial()
    {
        for (int i = 0; i < _origins.Count; i++)
        {
            RaycastHit2D raycast = Physics2D.Raycast(_origins[i], Vector2.down, _collider.bounds.extents.y, _groundLayer);
            if (raycast.collider != null)
            {
                switch (raycast.collider.transform.tag)
                {
                    case "Ice":
                        _timeSlay = 0.001f;
                        _speed = 8;
                        _maxVelocity = 6;
                        material = Materials.ICE;
                        break;
                    case "Stone":
                        _timeSlay = _timeSlayDefault;
                        _speed = 20;
                        _maxVelocity = 4;
                        material = Materials.STONE;
                        break;
                    case "Grass":
                        _timeSlay = _timeSlayDefault;
                        _speed = 20;
                        _maxVelocity = 4;
                        material = Materials.GRASS;
                        break;
                    case "Wood":
                        _timeSlay = _timeSlayDefault;
                        _speed = 20;
                        _maxVelocity = 4;
                        material = Materials.WOOD;
                        break;
                    case "Steel":
                        _timeSlay = _timeSlayDefault;
                        _speed = 20;
                        _maxVelocity = 4;
                        material = Materials.STEEL;
                        break;
                    default:
                        _timeSlay = _timeSlayDefault;
                        _speed = 20;
                        _maxVelocity = 4;
                        material = Materials.WOOD;
                        break;
                }
            }
        }
    }
    public bool IsGrounded()
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
            ActionPerformed();
            switch (GameManager.Instance._currentCharacter)
            {
                case GameManager.Character.AIKE:
                    if (_swordCooldown <= 0)
                    {
                        Aike();
                    }
                    break;
                case GameManager.Character.KROKUR: //Lengua(gancho)
                    Krokur();
                    
                    break;
                default:
                    break;
            }
        
        }
    }
    private void Aike()
    {
        _swordCooldown = _maxSwordCooldown;
        _cooldownBar.enabled = true;
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
        yield return new WaitForSeconds(0.4f);
        _sword.SetActive(false);
    }

    private void Krokur()
    {
        if (_distanceJoint.enabled && _lineRenderer.enabled && _target != null)
        {
            if ( _target.GetComponent<MovingHookPoint>())
            {
        
                _target.GetComponent<MovingHookPoint>().ContinueHooking();
            }
            _animator.SetTrigger("ReleaseTongue");
            _lineRenderer.enabled = false;
            _target = null;
            _distanceJoint.enabled = false;
            SoundManager.Instance.EjecutarAudio(hookStart, 1, 0.2f);

            
            return;
        }

        if (!_distanceJoint.enabled && _canInteractBox)
        {
            _target = this.GetComponentInChildren<AnchorManager>().GetTarget((int)this.transform.localScale.x, IsGrounded());
            if (_target == null) 
            {
                return;
            }
            else if (IsGrounded())
            {
                _target.GetComponentInParent<DrageableObject>().DragMe(this.transform.position + new Vector3(1.25f * (int)this.transform.localScale.x, 0.45f, 0), this);
                SoundManager.Instance.EjecutarAudio(hookStart, 1, 0.2f);
            }
            else
            {
                SoundManager.Instance.EjecutarAudio(hookStart, 1, 0.2f);
            }
            if (_target.GetComponent<MovingHookPoint>())
            {
                _target.GetComponent<MovingHookPoint>().StopHooking();
            }

            Vector2 targetPos = _target.position;
            _lineRenderer.SetPosition(0, targetPos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = targetPos;
            _distanceJoint.enabled = true;
            _lineRenderer.enabled = true;
            _animator.SetTrigger("Tongue");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageBoss"))
        {
            _canInteractBox = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DamageBoss"))
        {
            _canInteractBox = true;
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

    public void UpdateLRFirstPos(Vector3 pos)
    {
        _lineRenderer.SetPosition(0, pos);
    }

    public void UnHook()
    {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }
}