using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawn : MonoBehaviour
{
    public float timeToAppear;
    public float timeAFKtoAppear;
    private bool keepChecking = true;
    private bool playerInZone = false;
    public PlayerController player;
    public SpriteRenderer sr;
    private Color spriteColor;
    private float startDissappearTime;
    private bool dissappear = false;
    private float startAppearTime;
    private bool appear = false;
    void Start()
    {
        spriteColor = sr.color;
    }

    void Update()
    {
        if (dissappear)
        {
            float sinceStarted = Time.time - startDissappearTime;
            float percentageComplete = (sinceStarted / timeToAppear);
            sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, Mathf.Clamp(1 - percentageComplete, 0, 1));
            if (sr.color.a <= 0 )
            {
                dissappear = false;
            }
        }
        else if (appear)
        {
            float sinceStarted = Time.time - startDissappearTime;
            float percentageComplete = (sinceStarted / timeToAppear);
            sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, Mathf.Clamp(percentageComplete, 0, 1));
            if (sr.color.a >= 0)
            {
                appear = false;
            }
        }
    }
    protected void StartAppear()
    {
        Debug.Log("StartAppear");
        appear = true;
        startAppearTime = Time.time;
    }

    protected void ActionDetected()
    {
        CancelInvoke();
        keepChecking = false;
        if (sr.color.a > 0)
        {
            dissappear = true;
            startDissappearTime = Time.time;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || !keepChecking)
        {
            return;
        }
        
        playerInZone = true;
        Debug.Log("OnTriggerEnter");

        Invoke(nameof(StartAppear), timeAFKtoAppear);

    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || !keepChecking)
        {
            return;
        }
        Debug.Log("Exit");

        playerInZone = false;

    }
}
