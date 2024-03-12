using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shaker : MonoBehaviour
{
    public Animator _camAnimator;
    Vector3 shakeOffset = Vector3.zero;
    public float timeShake = 0.0f;
    public float minShakeX = 0.0f;
    public float maxShakeX = 0.0f;
    public float minShakeY = 0.0f;
    public float maxShakeY = 0.0f;
    private Vector3 startingPosition;

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if (timeShake > 0.0f)
            {
                timeShake -= Time.deltaTime;

                shakeOffset.x = Random.Range(minShakeX, maxShakeX);
                shakeOffset.y = Random.Range(minShakeY, maxShakeY);
                transform.parent.position += shakeOffset;
                if (timeShake < 0.0f)
                {
                    shakeOffset.x = 0;
                    shakeOffset.y = 0;
                    transform.parent.position = startingPosition;
                }
            }
        }
        
    }

    public void CamShake()
    {
        startingPosition = transform.position;
        timeShake = 0.3f;
        minShakeX = -0.07f; maxShakeX = 0.07f;
        minShakeY = -0.03f; maxShakeY = 0.03f;
    }
}
