using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ghost : MonoBehaviour
{
    [Header("Ghost Settings")]
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private List<Transform> _wayPointsAike;
    [SerializeField] private List<Transform> _wayPointsKrokur;
    [SerializeField] private float _speed;
    [SerializeField] private UnityEvent _onGoalReached;
    public Transform _goal;
    private Transform _mirror;
    private SpriteRenderer _spriteRenderer;
    public int _index;
    public List<Transform> points;
    private bool _mirrorReached;
    private bool _canChange;

    void Start()
    {
        transform.position = _wayPoints[0].position;
        _index = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mirror = _wayPoints[_wayPoints.Count - 1].transform;
        _canChange = true;
    }
    
    void Update()
    {
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
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        if (transform.position == points[_index].position)
        {
            _index++;
            if (_index >= points.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[_index].position, _speed * Time.deltaTime);
    }
}
