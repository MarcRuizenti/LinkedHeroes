using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{

    [SerializeField] private UnityEvent _onPlayerReached;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _onPlayerReached?.Invoke();
        }
    }
}
