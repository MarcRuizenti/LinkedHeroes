using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera _camara;

    // Update is called once per frame
    void Update()
    {
        _camara.transform.position = transform.position;
    }
}
