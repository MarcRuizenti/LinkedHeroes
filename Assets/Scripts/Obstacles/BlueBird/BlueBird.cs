using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    [Header("Mobile Platform Settings")]
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _speed;
    private SpriteRenderer _spriteRenderer;
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _wayPoints[0].position;
        _index = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < _wayPoints[_index].position.x)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        float distance = Vector3.Distance(transform.position, _wayPoints[_index].position);
        if (distance < 0.1f)
        {
            _index++;
            if (_index >= _wayPoints.Count) _index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_index].position, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
