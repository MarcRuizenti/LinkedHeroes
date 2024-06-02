using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireContoller : MonoBehaviour
{
    [Header("Taimers")]
    private Animator _animator;
    public bool _canFire;
    [SerializeField] private float _timeToFireTotal;
    [SerializeField] private float _timeToFire;
    [SerializeField] private float _fireTimeTotal;
    [SerializeField] private float _fireTime;
    private Collider2D _collider;

    [Header("Sounds")]
    private PlaySounds _playSounds;
    [SerializeField] private AudioClip chispa;
    [SerializeField, Range(0, 3)] private float picth;
    [SerializeField, Range(0, 1)] private float volume;
    void Start()
    {
        _playSounds = GetComponent<PlaySounds>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _timeToFire = _timeToFireTotal;
        _fireTime = _fireTimeTotal;
        _canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_timeToFire > 0 && _canFire)
        {
            _timeToFire -= Time.deltaTime;
            _fireTime = _fireTimeTotal;
        }
        else if(_timeToFire <= 0 && _canFire)
        {
            _playSounds.PlaySoundLocalAudioSource(chispa, picth, volume);
            _animator.SetTrigger("Fire");
            _timeToFire = _timeToFireTotal;
            _canFire = false;
        }

        if (!_canFire)
        {
            if (_fireTime > 0)
            {
                _fireTime -= Time.deltaTime;
            }
            else
            {
                _animator.SetTrigger("Stop");
                _canFire = true;
                _collider.enabled = false;
            }
        }
    }

    public void StartFire()
    {
        _collider.enabled = true;
    }
}
