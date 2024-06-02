using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Ghost : MonoBehaviour
{
    [Header("Ghost Settings")]
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private List<Transform> _wayPointsAike;
    [SerializeField] private List<Transform> _wayPointsKrokur;
    [SerializeField] private float _speedMax;
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _onGoalReached;
    public Transform _goal;
    private Transform _mirror;
    private SpriteRenderer _spriteRenderer;
    public int _index;
    public List<Transform> points;
    private bool _mirrorReached;
    private bool _canChange;
    private float _startCounter = 3f;

    [Header("Sounds")]
    private PlaySounds _playSound;
    [SerializeField] private AudioClip run;
    [SerializeField, Range(0, 3)] private float picth;
    [SerializeField, Range(0, 1)] private float volume;
    private bool canPlay;

    void Start()
    {
        canPlay = true;
        _playSound = GetComponent<PlaySounds>();
        transform.position = _wayPoints[0].position;
        _index = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mirror = _wayPoints[_wayPoints.Count - 1].transform;
        _canChange = true;
    }
    
    void Update()
    {
        GameObject shadow = Shadows.Instance.ShadowTrail();

        if(_startCounter > 0)
        {
            _startCounter -= Time.deltaTime;
            _speed = 0;
        }
        else
        {
            _speed = _speedMax;
            if (canPlay)
            {
                _playSound.PlaySoundSoundManager(run, picth, volume);
                canPlay = false;
            }
        }

        if (transform.position == _mirror.position)
        {
            _mirrorReached = true;
            _index = 0;
        }

        if (transform.position != _mirror.position && !_mirrorReached)
        {
            points = _wayPoints;
        }
        else if (_canChange && transform.position == _mirror.position && _mirrorReached)
        {
            _canChange = false;
            if (GameManager.Instance._currentCharacter == GameManager.Character.AIKE)
            {
                points = _wayPointsAike;
            }
            else
            {
                points = _wayPointsKrokur;
            }
        }


        if (transform.position == _goal.position)
        {
            _onGoalReached.Invoke();
            _speed = 0;
        }

        if (transform.position.x < points[_index].position.x)
        {
            _spriteRenderer.flipX = true;
            if(shadow) shadow.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            if (shadow) shadow.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (transform.position == points[_index].position)
        {
            _index++;
            if (_index >= points.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[_index].position, _speed * Time.deltaTime);
    }
}
