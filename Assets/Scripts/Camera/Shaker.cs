using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shaker : MonoBehaviour
{
    Vector3 shakeOffset = Vector3.zero;
    public float timeShake = 0.0f;
    public float minShakeX = 0.0f;
    public float maxShakeX = 0.0f;
    public float minShakeY = 0.0f;
    public float maxShakeY = 0.0f;
    public Vector3 startingPosition;

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if (timeShake > 0.0f)
            {
                timeShake -= Time.deltaTime;

                shakeOffset.x = Random.Range(minShakeX, maxShakeX);
                shakeOffset.y = Random.Range(minShakeY, maxShakeY);
                transform.position += shakeOffset;
                if (timeShake < 0.0f)
                {
                    shakeOffset.x = 0;
                    shakeOffset.y = 0;
                    transform.position = startingPosition;
                }
            }
        }
        
    }

    public void CamShake(float _time, float _minX, float _minY)
    {
        startingPosition = transform.position;
        timeShake = _time;
        minShakeX = _minX; maxShakeX = _minX * -1;
        minShakeY = _minY; maxShakeY = _minY * -1;
    }
}
