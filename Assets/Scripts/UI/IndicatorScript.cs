using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    public Transform Target { get; private set; }
    private Camera _camera;
    private float _offset;

    public void Initialize(Transform target, Camera camera, float indicatorOffset)
    {
        Target = target;
        _camera = camera;
        _offset = indicatorOffset;
    }

    public void UpdateTargetPosition()
    {
        if (Target != null)
        {
            Vector3 targetPosition = _camera.WorldToScreenPoint(Target.position);


            if (targetPosition.z > 0 && !IsInCameraView(targetPosition))
            {
                transform.position = targetPosition;
                ClampToScreen();
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void ClampToScreen()
    {
        Vector3 clampedPosition = transform.position;

        

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _offset, Screen.width - _offset);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, _offset, Screen.height - _offset);


        Debug.Log(clampedPosition);
        transform.position = clampedPosition;
    }

    private bool IsInCameraView(Vector3 screenPosition)
    {
        return screenPosition.x > 0 && screenPosition.x < Screen.width &&
            screenPosition.y >  0 && screenPosition.y < Screen.height;
    }

   
}
