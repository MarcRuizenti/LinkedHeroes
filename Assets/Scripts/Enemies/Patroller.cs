using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _speed = 2f;
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _wayPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _wayPoints[_index].position);
        if (distance < 0.1f)
        {
            _index++;
            if (_index >= _wayPoints.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_index].position, _speed * Time.deltaTime);
    }
}
