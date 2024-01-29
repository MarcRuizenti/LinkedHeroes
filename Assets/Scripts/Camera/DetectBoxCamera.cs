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
        _boxCamera.transform.position += new Vector3(_horizontal, _vertical, 0) * _cameraSpeed * Time.deltaTime;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Derecha")) {
            _horizontal = 1;
        }
        if (collision.CompareTag("Izquierda"))
        {
            _horizontal = -1;
        }
        if (collision.CompareTag("Arriba"))
        {
            _vertical = 1;
        }
        if (collision.CompareTag("Abajo"))
        {
            _vertical = -1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _horizontal = 0;
        _vertical = 0;
    }
}
