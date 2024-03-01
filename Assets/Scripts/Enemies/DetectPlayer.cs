using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerDetected;
    [SerializeField] private UnityEvent _onPlayerExit;
    [SerializeField] private bool flip;
    private Collider2D _playerCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerCollider = collision;
            _onPlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerCollider = null;
            _onPlayerExit?.Invoke();
        }
    }

    private void Update()
    {
        if (transform.parent.localScale.x > 0)
        {
            flip = false;
        }
        else
        {
            flip = true;
        }

        Vector3 scale = transform.parent.localScale;

        if (_playerCollider != null)
        {
            if (_playerCollider.transform.position.x > transform.position.x)
            {
                //int multiplier;
                //if (flip)
                //{
                //    multiplier = 1;
                //}
                //else
                //{
                //    multiplier = -1;
                //}
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.parent.localScale = scale;
        }

    }
}
