using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private bool timerStart;
    [SerializeField] private float _boxTimer;
    [SerializeField] private float _boxLife;
    [SerializeField] private float _boxTimerCount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Vector3 _startingPosition;
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider;
    [SerializeField] private LayerMask _groundLayer;

    private BoxSounds _sound;
    private void Start()
    {
        _sound = GetComponent<BoxSounds>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    { 
        _boxTimerCount = _boxTimer;
        timerStart = false;
        transform.position = _startingPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = _collider.bounds.center - new Vector3(0, 0.1f, 0);
        RaycastHit2D raycast = Physics2D.Raycast(origin, Vector2.down, _collider.bounds.extents.y, _groundLayer);
        Debug.DrawRay(origin, Vector2.down * (_collider.bounds.extents.y), Color.red);
        if (raycast.collider == null)
        {
            Debug.DrawRay(origin, Vector2.down * (_collider.bounds.extents.y), Color.green);
            gameObject.tag = "DamageBoss";
        }
        else
        {
            gameObject.tag = "Untagged";
        }

        if (timerStart)
        {
            _boxTimerCount -= Time.deltaTime;

            if(_boxTimerCount < 0)
            {
                SetActiveFalse();
                enabled = false;
                timerStart = false;
            }
        }

        if (_boxLife <= 0)
        {
            SetActiveFalse();
            enabled = false;
            timerStart = false;
        }
    }

    private void OnDisable()
    {
        Invoke("RespawnBox", _spawnDelay);
    }

    private void RespawnBox()
    {
        enabled = true;
        _boxLife = 5;
        SetActiveTrue();
    }

    public void StartTimer()
    {
        timerStart = true;
    }

    public void TakeDamge()
    {
        _boxLife--;
        _sound.PlaySoundHitBox(1, 0.5f);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
        _sound.PlaySoundDestroyBox(1, 0.2f);
    }

    public void SetActiveTrue()
    {
        gameObject.SetActive(true);
        _sound.PlaySoundSpawnBox(1, 0.5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            SetActiveFalse();
        }
    }
}
