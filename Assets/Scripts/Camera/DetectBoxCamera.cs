using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBoxCamera : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    [SerializeField] private GameObject _boxCamera;
    [SerializeField] private float _cameraSpeed;


    private void Update()
    {
        Vector3 origin = _boxCamera.transform.position;
        Vector3 newPosition = _boxCamera.transform.position + new Vector3(_horizontal, _vertical, 0);

        if (Vector3.Distance(_boxCamera.transform.position, transform.position) < -9 || Vector3.Distance(_boxCamera.transform.position, transform.position) > 9)
        {
            newPosition = transform.position;
        }

        _boxCamera.transform.position = Vector3.Lerp(origin, newPosition, Time.deltaTime * _cameraSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Derecha"))
        {
            _horizontal = 0.5f;
        }
        if (collision.CompareTag("Izquierda"))
        {
            _horizontal = -0.5f;
        }
        if (collision.CompareTag("Arriba"))
        {
            _vertical = 0.5f;
        }
        if (collision.CompareTag("Abajo"))
        {
            _vertical = -0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _horizontal = 0;
        _vertical = 0;
    }
}
