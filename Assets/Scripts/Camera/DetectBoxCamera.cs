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
        
        _boxCamera.transform.position = Vector3.Lerp(origin, newPosition, Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Derecha")) {
            _horizontal = 2;
        }
        if (collision.CompareTag("Izquierda"))
        {
            _horizontal = -2;
        }
        if (collision.CompareTag("Arriba"))
        {
            _vertical = 2;
        }
        if (collision.CompareTag("Abajo"))
        {
            _vertical = -2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _horizontal = 0;
        _vertical = 0;
    }
}
